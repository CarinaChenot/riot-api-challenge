namespace LolAPI.DTO
{
    public class LeaguePositionDTO
    {
        public string queueType { get; set; }
        public bool hotStreak { get; set; }
        public MiniSeriesDTO miniSeries { get; set; }
        public int wins { get; set; }
        public bool veteran { get; set; }
        public int losses { get; set; }
        public string playerOrTeamId { get; set; }
        public string tier { get; set; }
        public string playerOrTeamName { get; set; }
        public bool inactive { get; set; }
        public string rank { get; set; }
        public bool freshBlood { get; set; }
        public string leagueName { get; set; }
        public int leaguePoints { get; set; }
    }
}