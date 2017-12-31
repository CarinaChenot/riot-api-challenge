using System.Collections.Generic;

namespace LolAPI.DTO
{
    public class MatchFrameDto
    {
        public int timestamp { get; set; }
        public Dictionary<int, Participantframe> participantFrames { get; set; }
        public MatchEventDto[] events { get; set; }
    }
}