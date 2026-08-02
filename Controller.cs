namespace Taurus.controllers;

using Taurus.services;
using Taurus.requests;

public class Controller
{
    public static async Task<IResult> GetTarefasAsync()
    {
        var tarefas = await Service.ReadTarefasAsync();

        return Results.Ok(tarefas);
    }
    public static async Task<IResult> GetTarefaByIdAsync(int id)
    {
        var tarefa = await Service.GetTarefaByIdAsync(id);
        if (tarefa == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(tarefa);
    }
    public static async Task<IResult> CreateTarefaAsync(CreateTarefaRequest request)
    {
        var createdTarefa = await Service.CreateTarefaAsync(request);
        return Results.Created($"/tarefas/{createdTarefa.Id}", createdTarefa);
    }
    public static async Task<IResult> CompleteTarefaAsync(int id)
    {
        var tarefa = await Service.CompleteTarefaAsync(id);
        if (tarefa == null)
        {
            Console.WriteLine($"Tarefa com o id {id} não existe.");
            return Results.NotFound();
        }

        return Results.Ok(tarefa);
    }
}