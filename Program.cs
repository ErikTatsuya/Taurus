using Taurus.Extensions;

var builder = WebApplication.CreateBuilder();
builder.WebHost.UseUrls("http://localhost:8765");

// Registrar serviços da aplicação
builder.Services.AddControllers();
builder.Services.AddTarefaServices(builder.Configuration);

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

app.MapControllers();

app.Run();
