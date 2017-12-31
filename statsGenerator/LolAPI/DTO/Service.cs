namespace LolAPI.DTO
{
    public class Service
    {
        public string status { get; set; }
        public Incident[] incidents { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
    }
}