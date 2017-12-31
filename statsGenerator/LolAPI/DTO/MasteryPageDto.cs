namespace LolAPI.DTO
{
    public class MasteryPageDto
    {
        public bool current { get; set; }
        public MasteryDto[] masteries { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }
}