using Taurus.Data.Repositories;
using Taurus.DTOs;
using Taurus.Models;

namespace Taurus.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _repository;

    public TarefaService(ITarefaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Tarefa>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Tarefa?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Tarefa> CreateAsync(CreateTarefaRequest request)
    {
        var tarefa = new Tarefa
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Completed = false
        };

        return await _repository.CreateAsync(tarefa);
    }

    public async Task<Tarefa?> CompleteAsync(Guid id)
    {
        return await _repository.CompleteAsync(id);
    }

    public async Task<Tarefa?> ChangeTitleAsync(Guid id, ChangeTarefaTitleRequest request)
    {
        var tarefa = await _repository.GetByIdAsync(id);
        if (tarefa == null)
            return null;

        tarefa.Title = request.Title;
        return await _repository.UpdateAsync(tarefa);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }
}
