namespace LolAPI.DTO
{
    public class Participant
    {
        public int profileIconId { get; set; }
        public int championId { get; set; }
        public string summonerName { get; set; }
        public Rune[] runes { get; set; }
        public bool bot { get; set; }
        public Mastery[] masteries { get; set; }
        public int spell2Id { get; set; }
        public int teamId { get; set; }
        public int spell1Id { get; set; }
        public int summonerId { get; set; }
    }
}