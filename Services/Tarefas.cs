using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.DTO;
using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

#nullable enable

namespace Services.Tarefas;

public class Tarefas : ITarefas

{

    private readonly OrganizadorContext _context;

    public Tarefas(OrganizadorContext context)
    {
        _context = context;
    }


    //GET ID
    public async Task<Tarefa?> GetByIdTarefa(int id)
        => await _context.Tarefas.FindAsync(id);


    //GET ALL
    public async Task<List<Tarefa>> GetAllTarefa()
        => await Task.FromResult(_context.Tarefas.ToList());


    //GET DATE
    public async Task<List<Tarefa>> GetByDateTarefa(DateTime data)
        => await _context.Tarefas.Where(x => x.Data.Date == data.Date).ToListAsync();


    //GET NAME
    public async Task<Tarefa?> GetByNameTarefa(string name)
        => await _context.Tarefas.FindAsync(name);


    //GET STATUS
    public async Task<List<Tarefa>> GetByStatusTarefa(EnumStatusTarefa status)
        => await _context.Tarefas.Where(x => x.Status == status).ToListAsync();


    //GET TITLE
    public async Task<Tarefa?> GetByTitulo(string title)
        => await _context.Tarefas.FindAsync(title);


    //POST TASK
    public async Task<Tarefa?> AddTarefa(Tarefa tarefa)
    {
        _context.Add(tarefa);
        await _context.SaveChangesAsync();
        return tarefa;
    }


    //PUT TASK
    public async Task<Tarefa?> UpdateTarefa(int id, Tarefa tarefa)
    {
        var tarefaBanco = await GetByIdTarefa(id);
        if (tarefaBanco == null) return null;

        tarefaBanco.Titulo = tarefa.Titulo;
        tarefaBanco.Descricao = tarefa.Descricao;
        tarefaBanco.Data = tarefa.Data;
        tarefaBanco.Status = tarefa.Status;
        await _context.SaveChangesAsync();
        return tarefaBanco;

    }

    //DELETE TASK
    public async Task<Tarefa?> RemoveTarefa(int id)
    {
        var tarefa = await GetByIdTarefa(id);
        if (tarefa == null) return null;
        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();
        return tarefa;
    }
}