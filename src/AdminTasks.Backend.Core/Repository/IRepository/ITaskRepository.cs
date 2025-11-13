using DBContext.ApplicationDbContext;
using Dto.Output;
using Models.Input;

namespace Repository.IRepository.ITaskRepository;

public interface ITaskRepository
{
    public Task<JsonResponseDto> CreateTask(Tarea inputDto);
    public Task<JsonResponseDto> UpdateTask(Tarea inputDto);
    public Task<JsonResponseDto> DeleteTask(Tarea inputDto);
    public Task<JsonResponseDto> QueryTask(Tarea inputDto);
    public Task<JsonResponseDto> GetTasks(InputGetTasks inputDto);
}