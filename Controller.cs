namespace Taurus;

public class Controller
{
    public async static Task<IResult> GetTarefasAsync()
    {
        var tarefas = await Service.ReadJsonAsync();

        return Results.Ok(tarefas);
    }
}