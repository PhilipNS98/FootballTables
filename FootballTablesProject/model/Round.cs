public class Round
{
    public string? homeTeamAbbreviation { get; set; }
    public string? awayTeamAbbreviation { get; set; }
    public string? score { get; set; }
    public int homeTeamGoals { get; set; }
    public int awayTeamGoals { get; set; }

    public override string ToString()
    {
        return $"Round: {homeTeamAbbreviation}, {awayTeamAbbreviation}, {score}, {homeTeamGoals}, {awayTeamGoals}";
    }
}
