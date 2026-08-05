using Taurus.DTOs;
using Taurus.Models;

namespace Taurus.Services;

public interface ITarefaService
{
    Task<IReadOnlyList<Tarefa>> GetAllAsync();
    Task<Tarefa?> GetByIdAsync(Guid id);
    Task<Tarefa> CreateAsync(CreateTarefaRequest request);
    Task<Tarefa?> CompleteAsync(Guid id);
    Task<Tarefa?> ChangeTitleAsync(Guid id, ChangeTarefaTitleRequest request);
    Task<bool> DeleteAsync(Guid id);
}
