using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Core
{
    public class Controller : IController
    {
        private IRepository<IPlayer> players;
        private IRepository<ITeam> teams;
        public Controller()
        {
            players = new PlayerRepository();
            teams = new TeamRepository();
        }
        public string LeagueStandings()
        {
            StringBuilder sb = new();

            sb.AppendLine("***League Standings***");

            foreach (var team in teams.Models
                .OrderByDescending(t => t.PointsEarned)
                .ThenByDescending(t => t.OverallRating)
                .ThenBy(t => t.Name))
            {
                sb.AppendLine(team.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string NewContract(string playerName, string teamName)
        {
            if (!players.ExistsModel(playerName))
            {
                return $"Player with the name {playerName} does not exist in the {nameof(PlayerRepository)}.";
            }
            if (!teams.ExistsModel(teamName))
            {
                return $"Team with the name {teamName} does not exist in the {nameof(TeamRepository)}.";
            }

            IPlayer player = players.GetModel(playerName);
            ITeam team = teams.GetModel(teamName);

            if (player.Team != default)
            {
                return $"Player {playerName} has already signed with {player.Team}.";
            }

            player.JoinTeam(teamName);
            team.SignContract(player);

            return $"Player {playerName} signed a contract with {teamName}.";
        }

        public string NewGame(string firstTeamName, string secondTeamName)
        {
            ITeam firstTeam = teams.GetModel(firstTeamName);
            ITeam secondTeam = teams.GetModel(secondTeamName);

            if (firstTeam.OverallRating < secondTeam.OverallRating)
            {
                firstTeam.Lose();
                secondTeam.Win();

                return $"Team {secondTeamName} wins the game over {firstTeamName}!";
            }
            else if (secondTeam.OverallRating < firstTeam.OverallRating)
            {
                firstTeam.Win();
                secondTeam.Lose();

                return $"Team {firstTeamName} wins the game over {secondTeamName}!";
            }
            else
            {
                firstTeam.Draw();
                secondTeam.Draw();

                return $"The game between {firstTeamName} and {secondTeamName} ends in a draw!";
            }
        }

        public string NewPlayer(string typeName, string name)
        {
            if (typeName != nameof(Goalkeeper)
                  && typeName != nameof(ForwardWing)
                  && typeName != nameof(CenterBack))
            {
                return $"{typeName} is invalid position for the application.";
            }
            if (players.ExistsModel(name))
            {
                string position = players.GetModel(name).GetType().Name;

                return $"{name} is already added to the {nameof(PlayerRepository)} as {position}.";
            }

            IPlayer player;
            if (typeName == nameof(Goalkeeper))
            {
                player = new Goalkeeper(name);
                players.AddModel(player);
            }
            else if (typeName == nameof(ForwardWing))
            {
                player = new ForwardWing(name);
                players.AddModel(player);
            }
            else if (typeName == nameof(CenterBack))
            {
                player = new CenterBack(name);
                players.AddModel(player);
            }

            return $"{name} is filed for the handball league.";
        }

        public string NewTeam(string name)
        {
            if (teams.ExistsModel(name))
            {
                return $"{name} is already added to the {typeof(TeamRepository)}.";
            }

            teams.AddModel(new Team(name));

            return $"{name} is successfully added to the {nameof(TeamRepository)}.";
        }

        public string PlayerStatistics(string teamName)
        {
            StringBuilder sb = new();

            ITeam team = teams.GetModel(teamName);

            sb.AppendLine($"***{team.Name}***");

            var teamPlayers = team.Players.OrderByDescending(p => p.Rating).ThenBy(p => p.Name);

            foreach (var player in teamPlayers)
            {
                sb.AppendLine(player.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
