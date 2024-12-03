using Microsoft.AspNetCore.Mvc;

namespace DiscordBotIntegration.APIs;

[ApiController()]
public class DocumentsController : DocumentsControllerBase
{
    public DocumentsController(IDocumentsService service)
        : base(service) { }
}
