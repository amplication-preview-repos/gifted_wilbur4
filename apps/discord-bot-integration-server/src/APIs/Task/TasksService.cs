using DiscordBotIntegration.Infrastructure;

namespace DiscordBotIntegration.APIs;

public class TasksService : TasksServiceBase
{
    public TasksService(DiscordBotIntegrationDbContext context)
        : base(context) { }
}
