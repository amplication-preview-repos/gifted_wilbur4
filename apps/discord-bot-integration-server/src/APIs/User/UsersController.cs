using Microsoft.AspNetCore.Mvc;

namespace DiscordBotIntegration.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
