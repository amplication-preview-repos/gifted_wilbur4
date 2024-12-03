using Microsoft.AspNetCore.Mvc;

namespace DiscordBotIntegration.APIs;

[ApiController()]
public class IntegrationsController : IntegrationsControllerBase
{
    public IntegrationsController(IIntegrationsService service)
        : base(service) { }
}
