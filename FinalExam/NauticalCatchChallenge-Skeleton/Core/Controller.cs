using NauticalCatchChallenge.Core.Contracts;
using NauticalCatchChallenge.Models;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Core
{
    public class Controller : IController
    {
        private DiverRepository divers;
        private FishRepository fishes;
        public Controller()
        {
            divers = new DiverRepository();
            fishes = new FishRepository();
        }
        public string ChaseFish(string diverName, string fishName, bool isLucky)
        {
            IDiver diver = divers.GetModel(diverName);
            IFish fish = fishes.GetModel(fishName);

            if (diver is null)
            {
                return $"{divers.GetType().Name} has no {diverName} registered for the competition.";
            }

            if (fish is null)
            {
                return $"{fishName} is not allowed to be caught in this competition.";
            }

            if ( diver.HasHealthIssues == true)
            {
                return $"{diverName} will not be allowed to dive, due to health issues.";
            }

            if (diver.OxygenLevel < fish.TimeToCatch)
            {
                diver.Miss(fish.TimeToCatch);
                return $"{diverName} missed a good {fish.Name}.";
            }

            else if (diver.OxygenLevel == fish.TimeToCatch)
            {
                if (isLucky == true)
                {
                    diver.Hit(fish);
                    diver.UpdateHealthStatus(); 
                    return $"{diverName} hits a {Math.Round(fish.Points, 1)}pt. {fish.Name}.";
                }
                else
                {
                    diver.Miss(fish.TimeToCatch);
                    if (diver.OxygenLevel == 0)
                    {
                        diver.UpdateHealthStatus();
                    }
                    return $"{diverName} missed a good {fishName}.";
                }
            }


            diver.Hit(fish);

            return $"{diverName} hits a {Math.Round(fish.Points, 1)}pt. {fishName}.";


        }

        public string CompetitionStatistics()
        {
            StringBuilder sb = new StringBuilder();

            var sorted = divers.Models.OrderByDescending(d => d.CompetitionPoints)
                .ThenByDescending(d => d.Catch.Count)
                .ThenBy(d => d.Name);

            foreach ( var diver in sorted)
            {
                sb.AppendLine(diver.ToString());
            }

            return sb.ToString().TrimEnd();

        }

        public string DiveIntoCompetition(string diverType, string diverName)
        {
            if (diverType != nameof(FreeDiver) && diverType != nameof(ScubaDiver))
            {
                return $"{diverType} is not allowed in our competition.";
            }

            IDiver diver = divers.GetModel(diverName);

            if (diver != null)
            {
                return $"{diverName} is already a participant -> {divers.GetType().Name}.";
            }

            if (diverType == nameof(FreeDiver))
            {
                diver = new FreeDiver(diverName);
            }
            else
            {
                diver = new ScubaDiver(diverName);
            }

            divers.AddModel(diver);

            return $"{diverName} is successfully registered for the competition -> {divers.GetType().Name}.";
        }

        public string DiverCatchReport(string diverName)
        {
            StringBuilder sb = new StringBuilder();
            IDiver diver = divers.GetModel(diverName);
            if (diver != null)
            {
                sb.AppendLine(diver.ToString());

                foreach (var fish in diver.Catch)
                {
                    sb.AppendLine(fish.ToString());
                }
            }

            return sb.ToString().TrimEnd();
        }

        public string HealthRecovery()
        {
            int count = 0;

            foreach (var diver in divers.Models)
            {
                if (diver.HasHealthIssues == true)
                {
                    diver.UpdateHealthStatus();
                    diver.RenewOxy();
                    count++;
                }

            }
                return $"Divers recovered: {count}";
        }

        public string SwimIntoCompetition(string fishType, string fishName, double points)
        {
            if (fishType != nameof(ReefFish) && fishType != nameof(DeepSeaFish) && fishType != nameof(PredatoryFish))
            {
                return $"{fishType} is forbidden for chasing in our competition.";
            }

            IFish fish = fishes.GetModel(fishName);

            if (fish != null)
            {
                return $"{fishName} is already allowed -> {fishes.GetType().Name}.";
            }

            if (fishType == nameof(ReefFish))
            {
                fish = new ReefFish(fishName, points);
            }
            else if (fishType == nameof(DeepSeaFish))
            {
                fish = new DeepSeaFish(fishName, points);
            }
            else
            {
                fish = new PredatoryFish(fishName, points);
            }

            fishes.AddModel(fish);

            return $"{fishName} is allowed for chasing.";
        }
    }
}
