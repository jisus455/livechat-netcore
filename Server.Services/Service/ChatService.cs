using Server.Handler;
using Server.Services.Handler;

namespace Server.Services.Service
{
    public class ChatService
    {
        RedisHandler redisHandler = new RedisHandler();

        public async Task<string> ObtenerConnection(string idUsuario)
        {
            string resultado = redisHandler.Get(idUsuario);
            return await Task.Run(() => resultado);
        }

        public async Task<bool> ModificarConnection(string idUsuario, string connection)
        {
            redisHandler.Set(idUsuario, connection);
            return await Task.Run(() => true);
        }
    }
}
