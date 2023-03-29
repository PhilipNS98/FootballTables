// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

namespace FootballTableSpace
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("First C# Football stats!\n");
            var leagues = new List<League>();
            var teams = new List<Team>();

            InitializeData(leagues, teams);         
            Print(leagues, teams);         
            CurrentStandings(teams);
        }

        public static void Print(List<League> leagues, List<Team> teams) {
            foreach(League l in leagues)
            {
                Console.WriteLine(l.ToString());
            }
            foreach(Team team in teams)
            {
                Console.WriteLine(team.ToString());
            }
        }

        public static void InitializeData(List<League> leagues, List<Team> teams)
        {
            InitializeSetupCSV(leagues);
            InitializeTeamCSV(teams);
        }

        public static void InitializeSetupCSV(List<League> leagues)
        {
            string filePath = "./csv/setup.csv";
            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine();

                while(!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    var league = new League {
                        Name = values[0],
                        positions_to_champions_league_qualification = Convert.ToInt32(values[1]),
                        positions_to_europa_league_qualification = Convert.ToInt32(values[2]),
                        positions_to_conference_league = Convert.ToInt32(values[3]),
                        positions_to_upper_league = Convert.ToInt32(values[4]),
                        positions_to_lower_league = Convert.ToInt32(values[5]),
                        positions_to_relegation_group = Convert.ToInt32(values[6]),
                        positions_to_championship_playoff = Convert.ToInt32(values[7]),
                    };

                    leagues.Add(league);
                }                
            }
        }

        public static void InitializeTeamCSV(List<Team> teams)
        {
            string filePath = "./csv/teams.csv";
            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine();

                while(!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    string specialRanking = "";

                    if (values.Length == 3)
                    {
                        specialRanking = values[2].Trim();
                    }

                    var team = new Team {
                        abbreviation = values[0],
                        full_club_name = values[1],
                        special_ranking = specialRanking
                    };

                    teams.Add(team);
                }                
            }
        }

        public static void CurrentStandings(List<Team> teams)
        {
            //present each club, sorted by points, goal dif, goal for, goal against, alphabetical
            //club position in list is calculated based on above sorting
            //if clubs share position, first should show number while others show dash
        
            var sortedTeams = teams.OrderByDescending(t => t.games_won*3)
                          .ThenByDescending(t => t.goals_for-t.goals_against)
                          .ThenByDescending(t => t.goals_for)
                          .ThenBy(t => t.goals_against)
                          .ThenBy(t => t.full_club_name)
                          .ToList();

            /* for (int i = 1; i < sortedTeams.Count+1; i++)
            {
                Console.WriteLine("{0,-2} {1,-25} {2,-2} {3,-2} {4,-2} {5,-2} {6,-2} {7,-2} {8,-2}",
                i,
                sortedTeams[i-1].Name,
                sortedTeams[i-1].GamesPlayed,
                sortedTeams[i-1].GamesWon,
                sortedTeams[i-1].GamesDrawn,
                sortedTeams[i-1].GamesLost,
                sortedTeams[i-1].GoalsFor,
                sortedTeams[i-1].GoalsAgainst,
                sortedTeams[i-1].GoalDifference,
                sortedTeams[i-1].Points);
            } */
            //System.Console.WriteLine("┎─────┬──────────────────┬───────────────────────────────────┒");
            System.Console.WriteLine("┏━━━━━┯━━━━━━━━━━━━━━━━━━┯━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            System.Console.WriteLine("┃ Pos │ Team             │    M  W  D  L  GF GA GD P  Streak ┃");
            System.Console.WriteLine("┠─────┼──────────────────┼───────────────────────────────────┨");
            foreach (var team in sortedTeams)
            {
                Console.WriteLine("┃ {0, -4}│{1, -17} │ {2, 4} {3, 2} {4, 2} {5, 2} {6, 2} {7, 2} {8, 2} {9, 2} {10, 2}      ┃", 
                    sortedTeams.IndexOf(team) + 1,  // Position in table
                    //team.special_marking,  // Special marking in parentheses
                    team.full_club_name,   // Full club name
                    team.games_played,     // Games played M
                    team.games_won,        // Number of games won W
                    team.games_drawn,      // Number of games drawn D
                    team.games_lost,       // Number of games lost L
                    team.goals_for,        // Goals for
                    team.goals_against,    // Goals against
                    team.goal_difference,  // Goal difference
                    team.points,           // Points achieved
                    team.current_streak    // Current winning streak
                );
            }
            System.Console.WriteLine("┗━━━━━┷━━━━━━━━━━━━━━━━━━┷━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
        }
    }

}