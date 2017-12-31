namespace LolAPI.DTO
{
    public class Message
    {
        public string severity { get; set; }
        public string author { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string content { get; set; }
        public string id { get; set; }
        public Translation[] translations { get; set; }
    }
}