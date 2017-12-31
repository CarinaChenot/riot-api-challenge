namespace LolAPI.DTO
{
    public class MatchDto
    {
        public int seasonId { get; set; }
        public int queueId { get; set; }
        public long gameId { get; set; }
        public ParticipantIdentityDto[] participantIdentities { get; set; }
        public string gameVersion { get; set; }
        public string platformId { get; set; }
        public string gameMode { get; set; }
        public int mapId { get; set; }
        public string gameType { get; set; }
        public TeamStatsDto[] teams { get; set; }
        public ParticipantDto[] participants { get; set; }
        public int gameDuration { get; set; }
        public long gameCreation { get; set; }
    }
}