using Microsoft.AspNetCore.Mvc;

namespace DiscordBotIntegration.APIs;

[ApiController()]
public class TasksController : TasksControllerBase
{
    public TasksController(ITasksService service)
        : base(service) { }
}
