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

            initializeData(leagues, teams);         
            print(leagues, teams);         
        }

        public static void print(List<League> leagues, List<Team> teams) {
            foreach(League l in leagues)
            {
                Console.WriteLine(l.ToString());
            }
            foreach(Team team in teams)
            {
                Console.WriteLine(team.ToString());
            }
        }

        public static void initializeData(List<League> leagues, List<Team> teams)
        {
            initializeSetupCSV(leagues);
            initializeTeamCSV(teams);
        }

        public static void initializeSetupCSV(List<League> leagues)
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

        public static void initializeTeamCSV(List<Team> teams)
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
    }

}