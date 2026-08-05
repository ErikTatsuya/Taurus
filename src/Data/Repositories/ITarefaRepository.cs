using Taurus.Models;

namespace Taurus.Data.Repositories;

public interface ITarefaRepository
{
    Task<IReadOnlyList<Tarefa>> GetAllAsync();
    Task<Tarefa?> GetByIdAsync(Guid id);
    Task<Tarefa> CreateAsync(Tarefa tarefa);
    Task<Tarefa?> UpdateAsync(Tarefa tarefa);
    Task<Tarefa?> CompleteAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
}
