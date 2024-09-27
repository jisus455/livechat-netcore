

using Server.Handler;

namespace Server.Services.Service
{
    public class ChatService2
    {
        public async Task<string> ObtenerConnection(int id)
        {
            string query = $"SELECT connection FROM conexiones WHERE id = '{id}'";
            string resultado = SQLiteHandler.GetJson(query);
            return await Task.Run(() => resultado);
        }

        public async Task<string> ObtenerConexiones()
        {
            string query = $"SELECT * FROM conexiones";
            string resultado = SQLiteHandler.GetJson(query);
            return await Task.Run(() => resultado);
        }

        public async Task<bool> AgregarConexion(int id, string connection, string estado)
        {
            string query = $"INSERT INTO conexiones VALUES('{id}', '{connection}', '{estado}')";
            bool resultado = SQLiteHandler.Exec(query);
            return await Task.Run(() => resultado);
        }

        public async Task<bool> ModificarConexion(string connection, string estado)
        {
            string query = $"UPDATE conexiones SET estado = '{estado}' WHERE connection = '{connection}'";
            bool resultado = SQLiteHandler.Exec(query);
            return await Task.Run(() => resultado);
        }

        public async Task<bool> BorrarConexion(string connection)
        {
            string query = $"DELETE FROM conexiones WHERE connection = '{connection}'";
            bool resultado = SQLiteHandler.Exec(query);
            return await Task.Run(() => resultado);
        }


    }
}
