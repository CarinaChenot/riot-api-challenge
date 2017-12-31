namespace LolAPI.DTO
{
    public class LeagueListDTO
    {
        public string tier { get; set; }
        public string queue { get; set; }
        public string name { get; set; }
        public LeagueItemDTO[] entries { get; set; }
    }
}