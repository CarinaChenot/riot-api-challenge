namespace LolAPI.DTO
{
    public class Incident
    {
        public bool active { get; set; }
        public string created_at { get; set; }
        public long id { get; set; }
        public Message[] updates { get; set; }
    }
}