using System.Text.Json;

public class Service
{
    public static async Task<IReadOnlyList<Tarefa>> ReadJsonAsync()
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
        Console.WriteLine(jsonText);

        return json;
    }
}