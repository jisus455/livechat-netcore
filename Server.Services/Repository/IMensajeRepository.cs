using Server.Models.DTO;

namespace Server.Repository
{
    public interface IMensajeRepository
    {
        public Task<string> ObtenerMensajes(int idEmisor, int idReceptor);
        public Task<bool> AgregarMensaje(MensajeDTO mensaje);
    }
}
