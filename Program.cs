using LibertyRustAcquiring.Data;
using LibertyRustAcquiring.Data.Extensions;
using LibertyRustAcquiring.Settings;
using LibertyRustAcquiring.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

builder.WebHost.UseUrls("http://0.0.0.0:6060", "https://0.0.0.0:6061");

builder.Services.AddControllers();

builder.Services.AddSingleton<IPubKeyProvider, PubKeyProvider>();

builder.Services.AddScoped<IServerConnection, ServerConnection>();
builder.Services.AddScoped<IPhotoService, PhotoService>();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
});

builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(opts =>
        opts.UseSqlite(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddCors(options =>

    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://test.liberty-rust.com.ua");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        //policy.AllowCredentials();
    })
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();

app.UseMigration();

app.UseCors();

app.MapControllers();

async Task<string> ConnectRustRconAsync(ServerInfo serverInfo, string command)
{
    var serverUri = new Uri($"ws://{serverInfo.Hostname}:{serverInfo.RconPort}/{serverInfo.RconPassword}");

    using var ws = new ClientWebSocket();

    try
    {
        await ws.ConnectAsync(serverUri, CancellationToken.None);

        var payload = new
        {
            Identifier = 1,
            Message = command,
            Name = "WebRcon"
        };

        var jsonString = JsonSerializer.Serialize(payload);

        var bytesToSend = Encoding.UTF8.GetBytes(jsonString);

        await ws.SendAsync(
            new ArraySegment<byte>(bytesToSend),
            WebSocketMessageType.Text,
            endOfMessage: true,
            cancellationToken: CancellationToken.None
        );

        var buffer = new byte[4096];

        var result = await ws.ReceiveAsync(
            new ArraySegment<byte>(buffer),
            CancellationToken.None
        );

        var responseString = Encoding.UTF8.GetString(buffer, 0, result.Count);

        using var doc = JsonDocument.Parse(responseString);

        var message = doc.RootElement.GetProperty("Message").GetString();

        return message ?? "Нет поля 'Message' в ответе.";
    }
    catch (Exception ex)
    {
        return $"Failed to connect. {ex.Message}";
    }
}

app.MapPost("/send-command", async (IConfiguration configuration, RconCommandRequest request, IServerConnection server) =>
{
    if (string.IsNullOrWhiteSpace(request.Command))
    {
        return Results.BadRequest(new { error = "Поле 'command' отсутствует или пустое" });
    }

    var serverInfo = new ServerInfo
    {
        Hostname = configuration[$"{request.ServerName}:Ip"] ?? throw new ObjectIsNullException<ServerInfo>(),
        RconPort = configuration[$"{request.ServerName}:Port"] ?? throw new ObjectIsNullException<ServerInfo>(),
        RconPassword = configuration[$"{request.ServerName}:Password"] ?? throw new ObjectIsNullException<ServerInfo>(),
    };

    //var serverInfo = new ServerInfo
    //{
    //    Hostname = "168.100.161.151",
    //    RconPort = "28069",
    //    RconPassword = "Leberty"
    //};

    var response = await server.SendCommand(serverInfo, request.Command);

    return Results.Ok(response);
});

app.Run();