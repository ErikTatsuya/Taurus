using Microsoft.AspNetCore.Mvc;
using Taurus.DTOs;
using Taurus.Models;
using Taurus.Services;

namespace Taurus.Controllers;

[ApiController]
[Route("tasks")]
public class TarefaController : ControllerBase
{
    private readonly ITarefaService _service;

    public TarefaController(ITarefaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Tarefa>>> GetAll()
    {
        var tarefas = await _service.GetAllAsync();
        return Ok(tarefas);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Tarefa>> GetById(Guid id)
    {
        var tarefa = await _service.GetByIdAsync(id);
        if (tarefa == null)
            return NotFound();
        return Ok(tarefa);
    }

    [HttpPost]
    public async Task<ActionResult<Tarefa>> Create([FromBody] CreateTarefaRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return BadRequest("O título é obrigatório.");

        if (request.Title.Length > 32)
            return BadRequest("O título deve ter no máximo 32 caracteres.");

        var createdTarefa = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = createdTarefa.Id }, createdTarefa);
    }

    [HttpPatch("{id:guid}/complete")]
    public async Task<ActionResult<Tarefa>> Complete(Guid id)
    {
        var tarefa = await _service.CompleteAsync(id);
        if (tarefa == null)
            return NotFound();
        return Ok(tarefa);
    }

    [HttpPatch("{id:guid}/title")]
    public async Task<ActionResult<Tarefa>> ChangeTitle(Guid id, [FromBody] ChangeTarefaTitleRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return BadRequest("O título é obrigatório.");

        if (request.Title.Length > 32)
            return BadRequest("O título deve ter no máximo 32 caracteres.");

        var tarefa = await _service.ChangeTitleAsync(id, request);
        if (tarefa == null)
            return NotFound();
        return Ok(tarefa);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isSuccess = await _service.DeleteAsync(id);
        if (isSuccess)
            return NoContent();
        return NotFound();
    }
}
