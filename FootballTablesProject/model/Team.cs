public class Team
{
    public string? abbreviation { get; set; }
    public string? full_club_name { get; set; }
    public string? special_ranking { get; set; }
    public int games_played { get; set; }
    public int games_won { get; set; }
    public int games_drawn { get; set; }
    public int games_lost { get; set; }
    public int goals_for { get; set; }
    public int goals_against { get; set; }
    public int goal_difference { get; set; }
    public int points { get; set; }
    public int current_streak { get; set; }


    public override string ToString()
    {
        if (string.IsNullOrEmpty(special_ranking)) {
            return $"Team: {abbreviation}, {full_club_name}";
        } else {
            return $"Team: {abbreviation}, {full_club_name} ({special_ranking})";
        }
    }
}
