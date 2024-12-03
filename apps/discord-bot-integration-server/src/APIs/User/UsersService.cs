using DiscordBotIntegration.Infrastructure;

namespace DiscordBotIntegration.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(DiscordBotIntegrationDbContext context)
        : base(context) { }
}
