namespace LolAPI.DTO
{
    public class ParticipantDto
    {
        public ParticipantStatsDto stats { get; set; }
        public int spell1Id { get; set; }
        public int participantId { get; set; }
        public RuneDto[] runes { get; set; }
        public string highestAchievedSeasonTier { get; set; }
        public MatchMasteryDto[] masteries { get; set; }
        public int spell2Id { get; set; }
        public int teamId { get; set; }
        public ParticipantTimelineDto timeline { get; set; }
        public int championId { get; set; }
    }
}