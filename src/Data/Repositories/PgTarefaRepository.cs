using Microsoft.EntityFrameworkCore;
using Taurus.Models;

namespace Taurus.Data.Repositories;

/// <summary>
/// Implementação do repositório usando PostgreSQL via Entity Framework Core.
/// Pronta para uso quando a migração de JSON para PostgreSQL for concluída.
/// </summary>
public class PgTarefaRepository : ITarefaRepository
{
    private readonly AppDbContext _context;

    public PgTarefaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Tarefa>> GetAllAsync()
    {
        return await _context.Tarefas.ToListAsync();
    }

    public async Task<Tarefa?> GetByIdAsync(Guid id)
    {
        return await _context.Tarefas.FindAsync(id);
    }

    public async Task<Tarefa> CreateAsync(Tarefa tarefa)
    {
        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();
        return tarefa;
    }

    public async Task<Tarefa?> UpdateAsync(Tarefa tarefa)
    {
        var existing = await _context.Tarefas.FindAsync(tarefa.Id);
        if (existing == null)
            return null;

        _context.Entry(existing).CurrentValues.SetValues(tarefa);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<Tarefa?> CompleteAsync(Guid id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null)
            return null;

        tarefa.Completed = true;
        await _context.SaveChangesAsync();
        return tarefa;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null)
            return false;

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();
        return true;
    }
}
