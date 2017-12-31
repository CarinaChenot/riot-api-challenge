namespace LolAPI.DTO
{
    public class ShardStatus
    {
        public string name { get; set; }
        public string region_tag { get; set; }
        public string hostname { get; set; }
        public Service[] services { get; set; }
        public string slug { get; set; }
        public string[] locales { get; set; }
    }
}