public class League
{
    public string? Name { get; set; }
    public int positions_to_champions_league_qualification { get; set; }
    public int positions_to_europa_league_qualification { get; set; }
    public int positions_to_conference_league { get; set; }
    public int positions_to_upper_league { get; set; }
    public int positions_to_lower_league { get; set; }
    public int positions_to_relegation_group { get; set; }
    public int positions_to_championship_playoff { get; set; }

    public (string?, int, int, int, int, int, int, int) GetLeagueInfo()
    {
        return (
            Name, 
            positions_to_champions_league_qualification,
            positions_to_europa_league_qualification,
            positions_to_conference_league,
            positions_to_upper_league,
            positions_to_lower_league,
            positions_to_relegation_group,
            positions_to_championship_playoff
        );
    }

    public override string ToString()
    {
        return $"League: {Name}, {positions_to_champions_league_qualification}, {positions_to_europa_league_qualification}, {positions_to_conference_league}, {positions_to_upper_league}, {positions_to_lower_league}, {positions_to_relegation_group}, {positions_to_championship_playoff}";
    }
}
