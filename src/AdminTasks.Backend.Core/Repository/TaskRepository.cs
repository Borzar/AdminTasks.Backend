using System.Runtime.InteropServices;
using DBContext.ApplicationDbContext;
using Dto.Output;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Models.Input;
using Repository.IRepository.ITaskRepository;

namespace Repository.TaskRepository;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<JsonResponseDto> CreateTask(Tarea inputDto)
    {
        await _context.Tareas.AddAsync(inputDto);
        await _context.SaveChangesAsync();
        return new JsonResponseDto { StatusDto = "Success", DescriptionDto = "", ResultDto = new List<TaskOutputDto>() };
    }

    public async Task<JsonResponseDto> UpdateTask(Tarea inputDto)
    {
        var existingTaskDto = await _context.Tareas.FindAsync(inputDto.Id);

        if (existingTaskDto == null)
        {
            return new JsonResponseDto
            {
                StatusDto = "Failed",
                DescriptionDto = $"The task with ID {inputDto.Id} does not exist",
                ResultDto = new List<TaskOutputDto>()
            };
        }

        if (!string.IsNullOrWhiteSpace(inputDto.Titulo))
        {
            existingTaskDto.Titulo = inputDto.Titulo.Trim();
        };

        if (!string.IsNullOrWhiteSpace(inputDto.Descripcion))
        {
            existingTaskDto.Descripcion = inputDto.Descripcion.Trim();
        };

        await _context.SaveChangesAsync();
        return new JsonResponseDto { StatusDto = "Success", DescriptionDto = "", ResultDto = new List<TaskOutputDto>() };
    }

    public async Task<JsonResponseDto> DeleteTask(Tarea inputDto)
    {
        var existingTaskDto = await _context.Tareas.FindAsync(inputDto.Id);
        _context.Tareas.Remove(existingTaskDto);
        await _context.SaveChangesAsync();

        return new JsonResponseDto { StatusDto = "Success", DescriptionDto = "", ResultDto = new List<TaskOutputDto>() };
    }

    public async Task<JsonResponseDto> QueryTask(Tarea inputDto)
    {
        var existingTaskDto = await _context.Tareas.FindAsync(inputDto.Id);

        var taskDto = new TaskOutputDto
        {
            IdDto = existingTaskDto.Id,
            TitleDto = existingTaskDto.Titulo,
            DescriptionDto = existingTaskDto.Descripcion
        };

        return new JsonResponseDto { StatusDto = "Success", DescriptionDto = "", ResultDto = new List<TaskOutputDto> { taskDto } };
    }

    public async Task<JsonResponseDto> GetTasks(InputGetTasks inputDto)
    {
        var tasksDto = new List<TaskOutputDto>(); 

        var taskListDto = await _context.Tareas.Where(p => p.Id != 0).ToListAsync();

        foreach (var tarea in taskListDto)
        {
            tasksDto.Add(new TaskOutputDto
            {
                IdDto = tarea.Id,
                TitleDto = tarea.Titulo,
                DescriptionDto = tarea.Descripcion
            });

        };

        return new JsonResponseDto { StatusDto = "Success", DescriptionDto = "", ResultDto =  tasksDto };
    }

}