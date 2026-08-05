using System.Text.Json;
using Taurus.Models;

namespace Taurus.Data.Repositories;

public class JsonTarefaRepository : ITarefaRepository
{
    private readonly string _filePath = "data/tasks.json";

    public async Task<IReadOnlyList<Tarefa>> GetAllAsync()
    {
        if (!File.Exists(_filePath))
            return new List<Tarefa>();

        string jsonText = await File.ReadAllTextAsync(_filePath);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        List<Tarefa> tarefas = JsonSerializer.Deserialize<List<Tarefa>>(jsonText, options) ?? new List<Tarefa>();
        return tarefas;
    }

    public async Task<Tarefa?> GetByIdAsync(Guid id)
    {
        var tarefas = await GetAllAsync();
        return tarefas.FirstOrDefault(t => t.Id == id);
    }

    public async Task<Tarefa> CreateAsync(Tarefa tarefa)
    {
        var tarefas = (await GetAllAsync()).ToList();
        tarefas.Add(tarefa);
        await WriteAllAsync(tarefas);
        return tarefa;
    }

    public async Task<Tarefa?> UpdateAsync(Tarefa tarefa)
    {
        var tarefas = (await GetAllAsync()).ToList();
        var index = tarefas.FindIndex(t => t.Id == tarefa.Id);

        if (index == -1)
            return null;

        tarefas[index] = tarefa;
        await WriteAllAsync(tarefas);
        return tarefa;
    }

    public async Task<Tarefa?> CompleteAsync(Guid id)
    {
        var tarefas = (await GetAllAsync()).ToList();
        var tarefa = tarefas.FirstOrDefault(t => t.Id == id);

        if (tarefa == null)
            return null;

        tarefa.Completed = true;
        await WriteAllAsync(tarefas);
        return tarefa;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var tarefas = (await GetAllAsync()).ToList();
        var index = tarefas.FindIndex(t => t.Id == id);

        if (index == -1)
            return false;

        tarefas.RemoveAt(index);
        await WriteAllAsync(tarefas);
        return true;
    }

    private async Task WriteAllAsync(IReadOnlyList<Tarefa> tarefas)
    {
        if (!File.Exists(_filePath))
        {
            FileInfo file = new FileInfo(_filePath);
            file.Directory?.Create();
            file.Create().Close();
        }

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string jsonText = JsonSerializer.Serialize(tarefas, options);
        await File.WriteAllTextAsync(_filePath, jsonText);
    }
}
