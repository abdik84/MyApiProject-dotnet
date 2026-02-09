using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// DIsable HTTPS
// app.UseHttpsRedirection();

// Websockets
app.UseWebSockets();

app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        var buffer = new byte[1024];

        while (true)
        {
            var result = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer),
                CancellationToken.None
            );

            if (result.MessageType == WebSocketMessageType.Close)
                break;

            var message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
            var response = $"Server received: {message}";

            await webSocket.SendAsync(
                System.Text.Encoding.UTF8.GetBytes(response),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None
            );
        }
    }
    else
    {
        context.Response.StatusCode = 400;
    }
});

app.MapControllers();
app.Run();
