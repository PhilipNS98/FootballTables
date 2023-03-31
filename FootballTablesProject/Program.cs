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
            var rounds = new List<Round>();

            initializeData(leagues, teams, rounds);
            //print(leagues, teams, rounds);
            print(leagues);
            print(teams);
            print(rounds);

            CreateTable(leagues, teams, rounds);
        }

        /* public static void print(List<League> leagues, List<Team> teams, List<Round> rounds)
        {
            foreach (League l in leagues)
            {
                Console.WriteLine(l.ToString());
            }
            foreach (Team team in teams)
            {
                Console.WriteLine(team.ToString());
            }
            foreach (Round round in rounds)
            {
                Console.WriteLine(round.ToString());
            }
        } */

        // Print method using generics to allow multiple data types.
        public static void print<T>(List<T> items)
        {
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public static void initializeData(List<League> leagues, List<Team> teams, List<Round> rounds)
        {
            initializeSetupCSV(leagues);
            initializeTeamCSV(teams);
            initializeAllRoundsCSV(rounds);
        }

        public static void initializeSetupCSV(List<League> leagues)
        {
            string filePath = "./csv/setup.csv";
            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    string[] values = line?.Split(',') ?? new string[0];

                    var league = new League
                    {
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

        public static void initializeTeamCSV(List<Team> teams)
        {
            string filePath = "./csv/teams.csv";
            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    string[] values = line?.Split(',') ?? new string[0];
                    /* string specialRankingTemp = "";

                    if (values.Length == 3)
                    {
                        specialRankingTemp = values[2].Trim();
                    } */

                    //Pattern matching, using is operator, changed if expression to is statement
                    string specialRankingTemp = values.Length == 3 && values[2].Trim() is string sr ? sr : "";


                    var team = new Team(values[0], values[1], specialRankingTemp);
                    teams.Add(team);
                }
            }
        }

        public static void initializeAllRoundsCSV(List<Round> rounds)
        {
            for (int i = 1; i <= 32; i++)
            {
                string filePath = $"./csv/rounds/round-{i}.csv";
                initializeRoundCSV(rounds, filePath);
            }
        }

        public static void initializeRoundCSV(List<Round> rounds, string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    string[] values = line?.Split(',') ?? new string[0];

                    var round = new Round
                    {
                        homeTeamAbbreviation = values[0],
                        awayTeamAbbreviation = values[1],
                        score = values[2],
                        homeTeamGoals = Convert.ToInt32(values[3]),
                        awayTeamGoals = Convert.ToInt32(values[4]),
                    };

                    rounds.Add(round);
                }
            }
        }

        public static void CreateTable(List<League> leagues, List<Team> teams, List<Round> rounds)
        {
            Dictionary<string, Team> standings = new Dictionary<string, Team>();
            foreach (var t in teams)
            {
                string? abbreviation = t.Abbreviation;
                string? fullClubName = t.FullClubName;
                string? specialRanking = string.IsNullOrEmpty(t.SpecialRanking) ? "" : t.SpecialRanking;
                standings.Add(abbreviation ?? "", new Team(abbreviation ?? "", fullClubName ?? "", specialRanking ?? ""));
            }

            foreach (var round in rounds)
            {
                string? homeTeamAbbreviation = round.homeTeamAbbreviation;
                string? awayTeamAbbreviation = round.awayTeamAbbreviation;
                int homeTeamGoals = round.homeTeamGoals;
                int awayTeamGoals = round.awayTeamGoals;

                // update home team stats
                standings[homeTeamAbbreviation.Trim()].UpdateStats(homeTeamGoals, awayTeamGoals);

                // update away team stats
                standings[awayTeamAbbreviation.Trim()].UpdateStats(awayTeamGoals, homeTeamGoals);

            }
            var position = 1;
            // print table
            System.Console.WriteLine("┏━━━━━┯━━━━━━━━━━━━━━━━━━┯━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            System.Console.WriteLine("┃ Pos │ Team             │    M  W  D  L  GF GA GD P  Streak ┃");
            System.Console.WriteLine("┠─────┼──────────────────┼───────────────────────────────────┨");
            foreach (var standing in standings.OrderByDescending(s => s.Value.Points)
            
                .ThenByDescending(s => s.Value.GoalDifference)
                .ThenByDescending(s => s.Value.GoalsFor)
                .ThenBy(s => s.Value.GoalsAgainst)
                .ThenBy(s => s.Key))
            {
                var team = standing.Value;
                Console.WriteLine("┃ {0, -4}│{1, -17} │ {2, 4} {3, 2} {4, 2} {5, 2} {6, 2} {7, 2} {8, 2} {9, 2} {10, 6}  ┃", 
                    position,  // Position in table
                    //team.SpecialRanking,  // Special marking in parentheses
                    team.FullClubName,   // Full club name
                    team.GamesPlayed,     // Games played M
                    team.GamesWon,        // Number of games won W
                    team.GamesDrawn,      // Number of games drawn D
                    team.GamesLost,       // Number of games lost L
                    team.GoalsFor,        // Goals for
                    team.GoalsAgainst,    // Goals against
                    team.GoalDifference,  // Goal difference
                    team.Points,          // Points achieved
                    team.CurrentWinningStreak    // Current winning streak
                );
                position++;
            }
            System.Console.WriteLine("┗━━━━━┷━━━━━━━━━━━━━━━━━━┷━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

            // NEW
            System.Console.WriteLine("\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ NEW ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            var pos = 1;
            System.Console.WriteLine("┏━━━━━┯━━━━━━━━━━━━━━━━━━┯━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            System.Console.WriteLine("┃ Pos │ Team             │    M    W    D    L   GF   GA   GD    P   Streak     ┃");
            System.Console.WriteLine("┠─────┼──────────────────┼──────────────────────────────────────────────────────┨");
            foreach (var standing in standings.OrderByDescending(s => s.Value.Points)
                .ThenByDescending(s => s.Value.GoalDifference)
                .ThenByDescending(s => s.Value.GoalsFor)
                .ThenBy(s => s.Value.GoalsAgainst)
                .ThenBy(s => s.Key))
            {
                var team = standing.Value;
                Console.Write("┃ {0, -4}│{1, -17} │ {2, 4} {3, 4} {4, 4} {5, 4} {6, 4} {7, 4} {8, 4} {9, 4}  ", 
                    pos,                    // Position in table
                    //team.SpecialRanking,  // Special marking in parentheses
                    team.FullClubName,      // Full club name
                    team.GamesPlayed,       // Games played "M"
                    team.GamesWon,          // Number of games won "W"
                    team.GamesDrawn,        // Number of games drawn "D"
                    team.GamesLost,         // Number of games lost "L"
                    team.GoalsFor,          // Goals for "GF"
                    team.GoalsAgainst,      // Goals against "GA"
                    team.GoalDifference,    // Goal difference "P"
                    team.Points             // Points achieved "Streak"
                );

                // Get the last 5 games from the CurrentWinningStreak list
                var lastFiveGames = team.CurrentWinningStreak.TakeLast(5);
                // Loop through the last 5 games and add the colored text
                //
                foreach (var game in lastFiveGames)
                {
                    Console.ForegroundColor = game switch
                    {
                        'W' => ConsoleColor.Green,
                        'D' => ConsoleColor.Yellow,
                        'L' => ConsoleColor.Red,
                        _ => Console.ForegroundColor // set default color if game is null or unknown
                    };

                    Console.Write(game + " ");
                }
                Console.ResetColor();       // reset color to default
                Console.WriteLine("  ┃");   // finish the table row
                pos++;
            }
            System.Console.WriteLine("┗━━━━━┷━━━━━━━━━━━━━━━━━━┷━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
        }
    }

}