using System.ComponentModel.DataAnnotations;
public class Team
{
    [StringLength(3, ErrorMessage = "Abbreviation must be no more than 3 characters long.")]
    public string? Abbreviation { get; set; }
    public string? FullClubName { get; set; }
    public string? SpecialRanking { get; set; }

    public int GamesPlayed { get; set; }
    public int GamesWon { get; set; }
    public int GamesDrawn { get; set; }
    public int GamesLost { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int GoalDifference { get; set; }
    public int Points { get; set; }
    public string? CurrentWinningStreak { get; set; }

    public Team(string abbreviation, string fullClubName, string specialRanking)
    {
        Abbreviation = abbreviation;
        FullClubName = fullClubName;
        SpecialRanking = specialRanking;
    }

    public void UpdateStats(int goalsFor, int goalsAgainst)
    {
        GamesPlayed++;
        GoalsFor += goalsFor;
        GoalsAgainst += goalsAgainst;
        GoalDifference = GoalsFor - GoalsAgainst;
        if (goalsFor > goalsAgainst)
        {
            GamesWon++;
            CurrentWinningStreak += "W";
           
        }
        else if (goalsFor == goalsAgainst)
        {
            GamesDrawn++;
            CurrentWinningStreak += "D";
        }
        else
        {
            GamesLost++;
            CurrentWinningStreak += "L";
          
        }
        Points = GamesWon * 3 + GamesDrawn;
    }
public override string ToString()
    {
        // The null-coalescing operator '??' is used to provide a default value if Abbreviation & FullClubName are null or empty
        string abbreviation = Abbreviation ?? "Unknown";
        string fullClubName = FullClubName ?? "Unknown";

        if (string.IsNullOrEmpty(SpecialRanking))
        {
            // String interpolation by using $ before the string, and inserting expressions (Abbreviation & FullClubName) into the string encapsulated with {}
            return $"Team: {Abbreviation}, {FullClubName}";
        }
        else
        {
            return $"Team: {Abbreviation}, {FullClubName} ({SpecialRanking})";
        }
    }
}


