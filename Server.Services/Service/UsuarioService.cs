using Server.Handler;
using Server.Models.DTO;
using Server.Repository;
using ServiceStack;
using ServiceStack.Script;

namespace Server.Service
{
    public class UsuarioService : IUsuarioRepository
    {
        public async Task<string> ObtenerUsuarios(string rol)
        {
            string query = $"SELECT id, nombre, email, rol, estado FROM usuarios WHERE rol != '{rol}'";
            string resultado = SQLiteHandler.GetJson(query);
            return await Task.Run(() => resultado);
        }

        public async Task<bool> AgregarUsuario(UsuarioDTO usuario)
        {
            string password = usuario.clave;
            string passwordHash =  BCrypt.Net.BCrypt.EnhancedHashPassword(password, 12);

            string query = $"INSERT INTO usuarios VALUES(null,'{usuario.nombre}', '{usuario.email}', '{passwordHash}', 'USUARIO', 'CONECTADO')";
            bool resultado = SQLiteHandler.Exec(query);
            return await Task.Run(() => resultado);
        }

        public async Task<string> Login(LoginDTO login)
        {
            string query = $"SELECT clave FROM usuarios WHERE email = '{login.email}'";
            string resultado = SQLiteHandler.GetScalar(query);

            if (resultado != string.Empty || resultado != null)
            {
                bool verificacion = BCrypt.Net.BCrypt.EnhancedVerify(login.clave, resultado);
                if(verificacion)
                {
                    query = $"SELECT id, nombre, email, rol, estado FROM usuarios WHERE email = '{login.email}'";
                    resultado = SQLiteHandler.GetJson(query);
                }
                else
                {
                    resultado = "[]";
                }
            }
            else
            {
                resultado = "[]";
            }

            return await Task.Run(() => resultado);
        }

        public async Task<string> ObtenerNombrePorUsuario(int id)
        {
            string query = $"SELECT nombre FROM usuarios WHERE id = {id}";
            string resultado = SQLiteHandler.GetScalar(query);
            return await Task.Run(() => resultado);
        }

        public async Task<string> ObtenerRolPorUsuario(int id)
        {
            string query = $"SELECT rol FROM usuarios WHERE id = {id}";
            string resultado = SQLiteHandler.GetScalar(query);
            return await Task.Run(() => resultado);
        }



    }
}
