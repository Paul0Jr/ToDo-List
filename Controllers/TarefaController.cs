using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Tarefas;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.DTO;
using TrilhaApiDesafio.Services;


namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefas response;
        public TarefaController(ITarefas service)
        {
            response = service;
        }

        [HttpGet("All")]
        public async Task<ActionResult> ObterTodos()
        {
            var tarefas = await response.GetAllTarefa();
            if (tarefas == null || tarefas.Count == 0)
                return NotFound("A lista de tarefas está vazia");
            return Ok(tarefas);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId([FromRoute] int id)
        {
            var tarefa = await response.GetByIdTarefa(id);
            if (tarefa == null) return NotFound($"A tarefa de {id} não existe");
            return Ok(tarefa);
        }

        [HttpGet("Titulo")]
        public async Task<IActionResult> ObterPorTitulo([FromRoute] string titulo)
        {
            var tarefa = await response.GetByTitulo(titulo);
            if (tarefa == null) return NotFound($"A tarefa de título '{titulo}' não existe");
            return Ok(tarefa);
        }

        [HttpGet("Data")]
        public async Task<IActionResult> ObterPorData([FromRoute] DateTime data)
        {
            var tarefas = await response.GetByDateTarefa(data);
            if (tarefas == null || tarefas.Count == 0) return NotFound("Não existem tarefas para a data selecionada");
            return Ok(tarefas);

        }

        [HttpGet("Status")]
        public async Task<IActionResult> ObterPorStatus([FromRoute] EnumStatusTarefa status)
        {
            var tarefas = await response.GetByStatusTarefa(status);
            if (tarefas == null || tarefas.Count == 0) return NotFound($"Não existem tarefas com status '{status}'.");
            return Ok(tarefas);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Criar(Tarefa tarefa)
        {
            //if (tarefa.Data == DateTime.MinValue) { return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" }); }
            var task = await response.AddTarefa(tarefa);
            if (task == null) return BadRequest("Erro ao adicionar tarefa.");
            return Ok(task);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Atualizar([FromRoute] int id, Tarefa tarefa)
        {
            var task = await response.GetByIdTarefa(id);
            if (task == null) return NotFound();
            await response.UpdateTarefa(id, tarefa);

            return Ok(task);
        }

        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Deletar([FromRoute] int id)
        {
            try
            {
                var task = await response.RemoveTarefa(id);
                if (task == null) return NotFound($"Não foi possível encontrar tarefa com id {id}.");
                return Ok("Tarefa excluída com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar tarefa: {ex.Message}");
            }
        }
    }
}