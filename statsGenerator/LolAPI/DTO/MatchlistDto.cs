namespace LolAPI.DTO
{
    public class MatchlistDto
    {
        public MatchReferenceDto[] matches { get; set; }
        public int endIndex { get; set; }
        public int startIndex { get; set; }
        public int totalGames { get; set; }
    }
}