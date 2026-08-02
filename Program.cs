namespace Taurus;

public class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();
        // Cors -_- | desenvolvimento
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        var app = builder.Build();
        app.UseCors();

        app.MapGet("/", () =>
        {
            var date = DateTime.Now.Date;
            var hour = DateTime.Now.Hour;
            return Results.Ok($"Olá, agora são {hour} do dia {date:dd/MM/yyyy}");
        });

        app.MapGet("/tasks", Controller.GetTarefasAsync);

        app.Run();
    }
}