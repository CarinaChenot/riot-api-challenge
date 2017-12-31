namespace LolAPI.DTO
{
    public class ChampionDto
    {
        public bool rankedPlayEnabled { get; set; }
        public bool botEnabled { get; set; }
        public bool botMmEnabled { get; set; }
        public bool active { get; set; }
        public bool freeToPlay { get; set; }
        public int id { get; set; }
    }
}