using DiscordBotIntegration.APIs;
using DiscordBotIntegration.APIs.Common;
using DiscordBotIntegration.APIs.Dtos;
using DiscordBotIntegration.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBotIntegration.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class IntegrationsControllerBase : ControllerBase
{
    protected readonly IIntegrationsService _service;

    public IntegrationsControllerBase(IIntegrationsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Integration
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Integration>> CreateIntegration(IntegrationCreateInput input)
    {
        var integration = await _service.CreateIntegration(input);

        return CreatedAtAction(nameof(Integration), new { id = integration.Id }, integration);
    }

    /// <summary>
    /// Delete one Integration
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteIntegration(
        [FromRoute()] IntegrationWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteIntegration(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Integrations
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Integration>>> Integrations(
        [FromQuery()] IntegrationFindManyArgs filter
    )
    {
        return Ok(await _service.Integrations(filter));
    }

    /// <summary>
    /// Meta data about Integration records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> IntegrationsMeta(
        [FromQuery()] IntegrationFindManyArgs filter
    )
    {
        return Ok(await _service.IntegrationsMeta(filter));
    }

    /// <summary>
    /// Get one Integration
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Integration>> Integration(
        [FromRoute()] IntegrationWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Integration(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Integration
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateIntegration(
        [FromRoute()] IntegrationWhereUniqueInput uniqueId,
        [FromQuery()] IntegrationUpdateInput integrationUpdateDto
    )
    {
        try
        {
            await _service.UpdateIntegration(uniqueId, integrationUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
