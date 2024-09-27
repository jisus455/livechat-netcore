using Microsoft.AspNetCore.Mvc;
using Server.Model;
using Server.Models.DTO;
using Server.Repository;

namespace Server.Controller
{
    [Route("/mensajes")]
    [ApiController]
    public class MensajeController : ControllerBase
    {
        private readonly IMensajeRepository mensajeRepository;

        public MensajeController(IMensajeRepository mensajeRepository)
        {
            this.mensajeRepository = mensajeRepository;
        }

        [HttpGet]
        public async Task<string> ObtenerMensaje(int idEmisor, int idReceptor)
        {
            return await Task.Run(() => mensajeRepository.ObtenerMensajes(idEmisor, idReceptor));
        }

        [HttpPost]
        public async Task<bool> AgregarMensaje(MensajeDTO mensaje)
        {
            return await Task.Run(() => mensajeRepository.AgregarMensaje(mensaje));
        }
    }
}
