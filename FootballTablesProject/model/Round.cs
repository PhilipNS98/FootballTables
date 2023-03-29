public class Round
{
    public string? home_team_abbreviated { get; set; }
    public string? away_team_abbreviated { get; set; }
    public string? score { get; set; }
    public int? goals_home_team { get; set; }
    public int? goals_away_team { get; set; }

    public override string ToString()
    {
        return $"Round: {home_team_abbreviated}, {away_team_abbreviated}, {score}, {goals_home_team}, {goals_away_team}";
    }
}
