namespace Server.Model
{
    public class Mensaje
    {
        public int id { get; set; }
        public int emisor { get; set; }
        public int receptor { get; set; }
        public string? mensaje { get; set; }
        public string? estado {  get; set; }
        public string? fecha { get; set; }

    }
}
