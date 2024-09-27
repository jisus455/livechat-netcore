namespace Server.Model
{
    public class Usuario
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? email { get; set; }
        public string? clave { get; set; }
        public string? rol { get; set; }
        public string? estado { get; set;}
    }
}
