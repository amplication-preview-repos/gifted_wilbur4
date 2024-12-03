using DiscordBotIntegration.APIs;

namespace DiscordBotIntegration;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IDocumentsService, DocumentsService>();
        services.AddScoped<IIntegrationsService, IntegrationsService>();
        services.AddScoped<ITasksService, TasksService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
