using NauticalCatchChallenge.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public abstract class Diver : IDiver
    {
        private string name;
        private int oxygenLevel;
        private List<string> catchedFishes;
        private double competitionPoints;
        private bool hasHealthIssues;

        protected Diver(string name, int oxygenLevel)
        {
            Name = name;
            OxygenLevel = oxygenLevel;

            catchedFishes = new List<string>();
            competitionPoints = 0;
            hasHealthIssues = false;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Diver's name cannot be null or empty.");
                }
                name = value;
            }
        }

        public int OxygenLevel
        {
            get => oxygenLevel;
            protected set
            {
                if (value < 0)
                {
                    value = 0;
                }
                oxygenLevel = value;
            }
        }

        public IReadOnlyCollection<string> Catch => catchedFishes;

        public double CompetitionPoints => competitionPoints;

        public bool HasHealthIssues => hasHealthIssues;

        public void Hit(IFish fish)
        {
            oxygenLevel -= fish.TimeToCatch;
            catchedFishes.Add(fish.Name);
            competitionPoints += fish.Points;
        }

        public abstract void Miss(int TimeToCatch);


        public abstract void RenewOxy();


        public void UpdateHealthStatus()
        {
            if (hasHealthIssues == false)
            {
                hasHealthIssues = true;
            }
            else
            {
                hasHealthIssues = false;
            }
        }

        public override string ToString()
        {
            return $"Diver [ Name: {Name}, Oxygen left: {OxygenLevel}, Fish caught: {catchedFishes.Count}, Points earned: {Math.Round(CompetitionPoints, 1)} ]";
        }

    }
}
