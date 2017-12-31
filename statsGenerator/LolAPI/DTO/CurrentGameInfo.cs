namespace LolAPI.DTO
{
    public class CurrentGameInfo
    {
        public long gameId { get; set; }
        public long gameStartTime { get; set; }
        public string platformId { get; set; }
        public string gameMode { get; set; }
        public int mapId { get; set; }
        public string gameType { get; set; }
        public int gameQueueConfigId { get; set; }
        public Observers observers { get; set; }
        public Participant[] participants { get; set; }
        public int gameLength { get; set; }
        public Bannedchampion[] bannedChampions { get; set; }
    }
}