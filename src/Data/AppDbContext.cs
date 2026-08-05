using Microsoft.EntityFrameworkCore;
using Taurus.Models;

namespace Taurus.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Tarefa> Tarefas { get; set; }
}
