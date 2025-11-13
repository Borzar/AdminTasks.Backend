using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Input;
using Models.Output;

namespace AdminTareas.BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminTasksController : ControllerBase
{
    private readonly ILogger<AdminTasksController> _logger;
    private readonly IMediator _mediator;

    public AdminTasksController(ILogger<AdminTasksController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [Route("CreateTask")]
    public async Task<ActionResult<JsonResponse>> CreateTask(InputCreateTask request)
    {
        var response = await _mediator.Send(request);
        if (response is null)
        {
            return NotFound(new JsonResponse { Status = "Failed", Description = "Error making the request" });
        }
        return Ok(response);

    }

    [HttpPatch]
    [Route("UpdateTask")]
    public async Task<ActionResult<JsonResponse>> UpdateTask(InputUpdateTask request)
    {
        var response = await _mediator.Send(request);
        if (response is null)
        {
            return NotFound(new JsonResponse { Status = "Failed", Description = "Error making the request" });
        }
        return Ok(response);

    }

    [HttpDelete]
    [Route("DeleteTask")]
    public async Task<ActionResult<JsonResponse>> DeleteTask(InputDeleteTask request)
    {
        var response = await _mediator.Send(request);
        if (response is null)
        {
            return NotFound(new JsonResponse { Status = "Failed", Description = "Error making the request" });
        }
        return Ok(response);

    }

    [HttpPost]
    [Route("QueryTask")]
    public async Task<ActionResult<JsonResponse>> QueryTask(InputQueryTask request)
    {
        var response = await _mediator.Send(request);
        if (response is null)
        {
            return NotFound(new JsonResponse { Status = "Failed", Description = "Error making the request" });
        }
        return Ok(response);

    }

    [HttpGet]
    [Route("GetTasks")]
    public async Task<ActionResult<JsonResponse>> GetTasks()
    {
        var request = new InputGetTasks();
        var response = await _mediator.Send(request);
        if (response is null)
        {
            return NotFound(new JsonResponse { Status = "Failed", Description = "Error making the request" });
        }
        return Ok(response);

    }
}
