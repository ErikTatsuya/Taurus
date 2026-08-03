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
    public static async Task<IResult> GetTarefaByIdAsync(Guid id)
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
    public static async Task<IResult> CompleteTarefaAsync(Guid id)
    {
        var tarefa = await Service.CompleteTarefaAsync(id);
        if (tarefa == null)
            return Results.NotFound();
        return Results.Ok(tarefa);
    }
    public static async Task<IResult> ChangeTarefaTitleAsync(Guid id, ChangeTarefaTitleRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return Results.BadRequest("O título é obrigatório.");

        if (request.Title.Length > 32)
            return Results.BadRequest("O título deve ter no máximo 32 caracteres.");

        var tarefa = await Service.ChangeTarefaTitleAsync(id, request.Title);

        if (tarefa == null)
            return Results.NotFound();

        return Results.Ok(tarefa);
    }
    public static async Task<IResult> DeleteTarefaAsync(Guid id)
    {
        var isSucess = await Service.DeleteTarefaAsync(id);
        if (isSucess)
            return Results.NoContent();
        return Results.NotFound();
    }
}