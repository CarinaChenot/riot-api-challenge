namespace LolAPI.DTO
{
    public class ChampionMasteryDTO
    {
        public int championLevel { get; set; }
        public bool chestGranted { get; set; }
        public int championPoints { get; set; }
        public int championPointsSinceLastLevel { get; set; }
        public int playerId { get; set; }
        public int championPointsUntilNextLevel { get; set; }
        public int tokensEarned { get; set; }
        public int championId { get; set; }
        public long lastPlayTime { get; set; }
    }
}