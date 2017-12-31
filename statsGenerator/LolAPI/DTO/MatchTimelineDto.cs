namespace LolAPI.DTO
{
    public class MatchTimelineDto
    {
        public MatchFrameDto[] frames { get; set; }
        public int frameInterval { get; set; }
    }
}