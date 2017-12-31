namespace LolAPI.DTO
{
    internal class ErreurDTO
    {
        public ErreurStatusDTO status { get; set; }
    }

    internal class ErreurStatusDTO
    {
        public int status_code { get; set; }
        public string message { get; set; }
    }
}
