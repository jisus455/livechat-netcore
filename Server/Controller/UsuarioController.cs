using Microsoft.AspNetCore.Mvc;
using Server.Models.DTO;
using Server.Repository;

namespace Server.Controller
{
    [Route("/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<string> ObtenerUsuarios(string rol)
        {
            return await Task.Run(() => usuarioRepository.ObtenerUsuarios(rol));
        }
        
        [HttpPost]
        public async Task<bool> AgregarUsuario(UsuarioDTO usuario)
        {
            return await Task.Run(() => usuarioRepository.AgregarUsuario(usuario));
        }

        [HttpPost("/usuarios/login")]
        public async Task<string> Login(LoginDTO login)
        {
            return await Task.Run(() => usuarioRepository.Login(login));
        }
    }
}
