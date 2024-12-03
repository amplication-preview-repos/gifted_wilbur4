using DiscordBotIntegration.Infrastructure;

namespace DiscordBotIntegration.APIs;

public class DocumentsService : DocumentsServiceBase
{
    public DocumentsService(DiscordBotIntegrationDbContext context)
        : base(context) { }
}
