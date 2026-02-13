using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.DTO;

namespace TrilhaApiDesafio.Services;

public interface ITarefas
{
    Task<Tarefa> GetByIdTarefa(int id);
    Task<List<Tarefa>> GetAllTarefa();
    Task<Tarefa> GetByNameTarefa(string name);
    Task<Tarefa> GetByTitulo(string title);
    Task<List<Tarefa>> GetByDateTarefa(DateTime data);
    Task<List<Tarefa>> GetByStatusTarefa(EnumStatusTarefa status);
    Task<Tarefa> AddTarefa(Tarefa tarefa);
    Task<Tarefa> UpdateTarefa(int id, Tarefa tarefa);
    Task<Tarefa> RemoveTarefa(int id);
}