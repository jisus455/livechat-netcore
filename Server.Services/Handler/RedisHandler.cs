using ServiceStack.Redis;

namespace Server.Services.Handler
{
    public class RedisHandler
    {
        private RedisClient cliente;

        public RedisHandler()
        {
            this.cliente = new RedisClient();
        }

        public void Set(string clave, string valor)
        {
            this.cliente.Set<string>(clave, valor);
        }

        public string Get(string clave)
        {
            return this.cliente.Get<string>(clave);
        }

        public void Del(string clave)
        {
            this.cliente.Del(clave);
        }
    }
}
