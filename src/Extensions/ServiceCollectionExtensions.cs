using Microsoft.EntityFrameworkCore;
using Taurus.Data;
using Taurus.Data.Repositories;
using Taurus.Services;

namespace Taurus.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTarefaServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Registrar DbContext para uso futuro com PostgreSQL
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ITarefaRepository, PgTarefaRepository>();

        // Registrar serviço
        services.AddScoped<ITarefaService, TarefaService>();

        return services;
    }
}
