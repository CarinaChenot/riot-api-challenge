namespace LolAPI.DTO
{
    public class MatchReferenceDto
    {
        public string lane { get; set; }//OK
        public long gameId { get; set; }
        public int champion { get; set; }//OK
        public string platformId { get; set; }
        public long timestamp { get; set; }
        public int queue { get; set; }//OK
        public string role { get; set; }
        public int season { get; set; }
    }
}