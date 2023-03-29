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
            //if team share position, first should show number while others show dash
        }
    }

}