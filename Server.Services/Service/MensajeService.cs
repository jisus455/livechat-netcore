using Server.Handler;
using Server.Models.DTO;
using Server.Repository;

namespace Server.Service
{
    public class MensajeService : IMensajeRepository
    {
        public async Task<string> ObtenerMensajes(int idEmisor, int idReceptor)
        {
            string query = $"SELECT m.id, u.nombre as emisor, m.mensaje, m.estado, m.fecha FROM mensajes m INNER JOIN usuarios u on (m.emisor = u.id) WHERE m.emisor = {idEmisor} and m.receptor = {idReceptor} or m.emisor = {idReceptor} and m.receptor = {idEmisor}";
            string resultado = SQLiteHandler.GetJson(query);
            return await Task.Run(() => resultado);
        }

        public async Task<bool> AgregarMensaje(MensajeDTO mensaje)
        {
            string query = $"INSERT INTO mensajes VALUES(null, {mensaje.emisor}, {mensaje.receptor}, '{mensaje.mensaje}', 'ENVIADO', '{DateTime.Now}')";
            bool resultado = SQLiteHandler.Exec(query);
            return await Task.Run(() => resultado);
        }
    }
}
