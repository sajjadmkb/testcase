using TestService.Infrastructure.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<WebSocketService>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseWebSockets();
        app.MapControllers();
        app.Map("/ws", async context =>
        {
            var webSocketService = context.RequestServices.GetRequiredService<WebSocketService>();
            await webSocketService.HandleWebSocketAsync(context);
        });

        app.Run();
    }
}
