public class Team
{
    public string? abbreviation { get; set; }
    public string? full_club_name { get; set; }
    public string? special_ranking { get; set; }

    public override string ToString()
    {
        if (string.IsNullOrEmpty(special_ranking)) {
            return $"Team: {abbreviation}, {full_club_name}";
        } else {
            return $"Team: {abbreviation}, {full_club_name} ({special_ranking})";
        }
    }
}
