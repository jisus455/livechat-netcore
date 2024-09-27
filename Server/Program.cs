using Server.Handler;
using Server.Hubs;
using Server.Repository;
using Server.Service;
using Server.Services.Repository;
using Server.Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

// Agregar los controladores
builder.Services.AddSingleton<IUsuarioRepository, UsuarioService>();
builder.Services.AddSingleton<IMensajeRepository, MensajeService>();
builder.Services.AddSingleton<IChatbotRepository, ChatbotService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configurar cors
app.UseCors(builder =>
{
    builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .WithOrigins("https://localhost:4200", "http://localhost:4200");
});
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chat");

SQLiteHandler.ConnectionString = builder.Configuration.GetConnectionString("defaultConnection");

app.Run();
