namespace LolAPI.DTO
{
    public class RunePageDto
    {
        public bool current { get; set; }
        public RuneSlotDto[] slots { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }
}