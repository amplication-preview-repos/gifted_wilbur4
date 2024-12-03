using DiscordBotIntegration.Infrastructure;

namespace DiscordBotIntegration.APIs;

public class IntegrationsService : IntegrationsServiceBase
{
    public IntegrationsService(DiscordBotIntegrationDbContext context)
        : base(context) { }
}
