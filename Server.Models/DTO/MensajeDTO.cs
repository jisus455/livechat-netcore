namespace Server.Models.DTO
{
    public class MensajeDTO
    {
        public int emisor { get; set; }
        public int receptor { get; set; }
        public string? mensaje { get; set; }
    }
}
