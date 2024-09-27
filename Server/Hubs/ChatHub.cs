using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Server.Models.Model;
using Server.Service;
using Server.Services.Service;


namespace Server.Hubs
{
    public class ChatHub : Hub
    {
        private UsuarioService usuarioService = new UsuarioService();
        private ChatService2 chatService2 = new ChatService2();

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string connection = Context.ConnectionId;

            //await Groups.RemoveFromGroupAsync(connection, "SignalR Users");
            await base.OnDisconnectedAsync(exception);

            bool resuiltado = chatService2.BorrarConexion(connection).Result;
            string conexiones = chatService2.ObtenerConexiones().Result;

            await Clients.All.SendAsync("UpdateConnection", conexiones);
        }


        public async Task SendMessage(SendMessage message)
        {
            //string connectionE = Context.ConnectionId;
            string nombreE = usuarioService.ObtenerNombrePorUsuario(message.emisor).Result;

            string connectionR = chatService2.ObtenerConnection(message.receptor).Result;
            //string nombreR = usuarioService.ObtenerNombrePorUsuario(message.receptor).Result;

            //var json = JSON.parse(connectionR);
            var json = JsonConvert.DeserializeObject<List<Connections>>(connectionR);

            NewMessage mensaje = new NewMessage(nombreE, message.mensaje);
            foreach (var item in json)
            {
                await Clients.Client(item.Connection).SendAsync("NewMessage", mensaje);
            }

        }

        public async Task GetConnection(int idUsuario)
        {
            string connection = Context.ConnectionId;

            bool resuiltado = chatService2.AgregarConexion(idUsuario, connection, "CONECTADO").Result;
            string conexiones = chatService2.ObtenerConexiones().Result;

            await Clients.All.SendAsync("UpdateConnection", conexiones);
        }

        public async Task OnWriting()
        {
            string connection = Context.ConnectionId;

            bool resultado = chatService2.ModificarConexion(connection, "ESCRIBIENDO").Result;
            string conexiones = chatService2.ObtenerConexiones().Result;

            await Clients.All.SendAsync("UpdateConnection", conexiones);
        }

        public async Task OnConnect()
        {
            string connection = Context.ConnectionId;

            bool resultado = chatService2.ModificarConexion(connection, "CONECTADO").Result;
            string conexiones = chatService2.ObtenerConexiones().Result;

            await Clients.All.SendAsync("UpdateConnection", conexiones);
        }
    }

    public record SendMessage(int emisor, int receptor, string mensaje);
    public record NewMessage(string emisor, string mensaje);

}
