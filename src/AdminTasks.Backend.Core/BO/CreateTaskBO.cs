using DBContext.ApplicationDbContext;
using MediatR;
using Models.Input;
using Models.Output;
using Repository.IRepository.ITaskRepository;

public class CreateTaskBO : IRequestHandler<InputCreateTask, JsonResponse>
{
    private readonly ITaskRepository _ITaskRepository;

    public CreateTaskBO(ITaskRepository ITaskRepository)
    {
        _ITaskRepository = ITaskRepository;
    }

    public async Task<JsonResponse> Handle(InputCreateTask request, CancellationToken cancellationToken)
    {

        var inputTask = new Tarea();

        try
        {
            inputTask.Titulo = request.Title;
            inputTask.Descripcion = request.Description;

            var resultDto = await _ITaskRepository.CreateTask(inputTask);

            return new JsonResponse
            {
                Status = resultDto.StatusDto,
                Description = resultDto.DescriptionDto,
                Result = new List<TaskOutput>()
            };

        } catch (Exception ex)
        {
            return new JsonResponse
            {
                Status = "Failed",
                Description = $"Error creating task: {ex.Message}",
                Result = new List<TaskOutput>()
            };
        }
    }

}