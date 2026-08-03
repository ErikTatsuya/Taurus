namespace Taurus.services;

using System.Text.Json;
using Taurus.requests;

public class Service
{
    public static async Task<IReadOnlyList<Tarefa>> ReadTarefasAsync()
    {
        string path = "data/tasks.json";
        if (!File.Exists(path))
            return new List<Tarefa>();

        string jsonText = await File.ReadAllTextAsync(path);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        List<Tarefa> json = JsonSerializer.Deserialize<List<Tarefa>>(jsonText, options) ?? new List<Tarefa>();
        return json;
    }
    public static async Task WriteTarefasAsync(IReadOnlyList<Tarefa> tarefas)
    {
        string path = "data/tasks.json";
        if (!File.Exists(path))
        {
            FileInfo file = new FileInfo(path);
            file.Directory?.Create();
            file.Create().Close();
        }
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string jsonText = JsonSerializer.Serialize(tarefas, options);
        await File.WriteAllTextAsync(path, jsonText);
    }
    public static async Task<Tarefa?> GetTarefaByIdAsync(Guid id)
    {
        var tarefas = await ReadTarefasAsync();
        var tarefa = tarefas.FirstOrDefault(t => t.Id == id);
        return tarefa;
    }
    public static async Task<Tarefa> CreateTarefaAsync(CreateTarefaRequest request)
    {
        var tarefas = await ReadTarefasAsync();
        var newTarefa = new Tarefa
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Completed = false
        };
        var updatedTarefas = tarefas.ToList();
        updatedTarefas.Add(newTarefa);
        await WriteTarefasAsync(updatedTarefas);
        return newTarefa;
    }
    public static async Task<Tarefa?> CompleteTarefaAsync(Guid id)
    {
        var tarefas = await ReadTarefasAsync();
        var tarefa = tarefas.FirstOrDefault(t => t.Id == id);
        if (tarefa == null)
            return null;
        tarefa.Completed = true;
        await WriteTarefasAsync(tarefas);
        return tarefa;
    }
    public static async Task<Tarefa?> ChangeTarefaTitleAsync(Guid id, string title)
    {
        var tarefas = await ReadTarefasAsync();
        var tarefa = tarefas.FirstOrDefault(t => t.Id == id);

        if (tarefa == null)
            return null;

        tarefa.Title = title;
        await WriteTarefasAsync(tarefas);
        return tarefa;
    }
    public static async Task<bool> DeleteTarefaAsync(Guid id)
    {
        var tarefas = await ReadTarefasAsync();
        var updatedTarefas = tarefas.ToList();
        var index = updatedTarefas.FindIndex(t => t.Id == id);

        // Caso index não seja encontrado, -1 será retornado.
        if (index == -1)
            return false;

        updatedTarefas.RemoveAt(index);
        await WriteTarefasAsync(updatedTarefas);

        return true;
    }
}