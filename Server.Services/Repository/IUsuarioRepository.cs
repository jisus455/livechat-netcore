using Server.Models.DTO;

namespace Server.Repository
{
    public interface IUsuarioRepository
    {
        public Task<string> ObtenerUsuarios(string rol);
        public Task<bool> AgregarUsuario(UsuarioDTO usuario);
        public Task<string> Login(LoginDTO login);
    }
}
