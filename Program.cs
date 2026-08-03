namespace Taurus;

using Taurus.controllers;

public class Program
{
    public static void Main()
    {
        FileInfo TarefasJson = new FileInfo("data/tasks.json");
        if (!TarefasJson.Exists)
            TarefasJson.Create();

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseUrls("http://localhost:8765");

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
        app.MapGet("/tasks/{id}", Controller.GetTarefaByIdAsync);
        app.MapPost("/tasks", Controller.CreateTarefaAsync);
        app.MapPatch("/tasks/{id}/complete", Controller.CompleteTarefaAsync);
        app.MapPatch("/tasks/{id}/title", Controller.ChangeTarefaTitleAsync);
        app.MapDelete("/tasks/{id}", Controller.DeleteTarefaAsync);

        app.Run();
    }
}