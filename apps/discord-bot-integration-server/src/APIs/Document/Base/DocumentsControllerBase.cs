using DiscordBotIntegration.APIs;
using DiscordBotIntegration.APIs.Common;
using DiscordBotIntegration.APIs.Dtos;
using DiscordBotIntegration.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBotIntegration.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class DocumentsControllerBase : ControllerBase
{
    protected readonly IDocumentsService _service;

    public DocumentsControllerBase(IDocumentsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Document
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Document>> CreateDocument(DocumentCreateInput input)
    {
        var document = await _service.CreateDocument(input);

        return CreatedAtAction(nameof(Document), new { id = document.Id }, document);
    }

    /// <summary>
    /// Delete one Document
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteDocument([FromRoute()] DocumentWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteDocument(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Documents
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Document>>> Documents(
        [FromQuery()] DocumentFindManyArgs filter
    )
    {
        return Ok(await _service.Documents(filter));
    }

    /// <summary>
    /// Meta data about Document records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> DocumentsMeta(
        [FromQuery()] DocumentFindManyArgs filter
    )
    {
        return Ok(await _service.DocumentsMeta(filter));
    }

    /// <summary>
    /// Get one Document
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Document>> Document(
        [FromRoute()] DocumentWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Document(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Document
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateDocument(
        [FromRoute()] DocumentWhereUniqueInput uniqueId,
        [FromQuery()] DocumentUpdateInput documentUpdateDto
    )
    {
        try
        {
            await _service.UpdateDocument(uniqueId, documentUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
