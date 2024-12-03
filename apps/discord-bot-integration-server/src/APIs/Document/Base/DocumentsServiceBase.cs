using DiscordBotIntegration.APIs;
using DiscordBotIntegration.APIs.Common;
using DiscordBotIntegration.APIs.Dtos;
using DiscordBotIntegration.APIs.Errors;
using DiscordBotIntegration.APIs.Extensions;
using DiscordBotIntegration.Infrastructure;
using DiscordBotIntegration.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscordBotIntegration.APIs;

public abstract class DocumentsServiceBase : IDocumentsService
{
    protected readonly DiscordBotIntegrationDbContext _context;

    public DocumentsServiceBase(DiscordBotIntegrationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Document
    /// </summary>
    public async Task<Document> CreateDocument(DocumentCreateInput createDto)
    {
        var document = new DocumentDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            document.Id = createDto.Id;
        }

        _context.Documents.Add(document);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<DocumentDbModel>(document.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Document
    /// </summary>
    public async Task DeleteDocument(DocumentWhereUniqueInput uniqueId)
    {
        var document = await _context.Documents.FindAsync(uniqueId.Id);
        if (document == null)
        {
            throw new NotFoundException();
        }

        _context.Documents.Remove(document);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Documents
    /// </summary>
    public async Task<List<Document>> Documents(DocumentFindManyArgs findManyArgs)
    {
        var documents = await _context
            .Documents.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return documents.ConvertAll(document => document.ToDto());
    }

    /// <summary>
    /// Meta data about Document records
    /// </summary>
    public async Task<MetadataDto> DocumentsMeta(DocumentFindManyArgs findManyArgs)
    {
        var count = await _context.Documents.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Document
    /// </summary>
    public async Task<Document> Document(DocumentWhereUniqueInput uniqueId)
    {
        var documents = await this.Documents(
            new DocumentFindManyArgs { Where = new DocumentWhereInput { Id = uniqueId.Id } }
        );
        var document = documents.FirstOrDefault();
        if (document == null)
        {
            throw new NotFoundException();
        }

        return document;
    }

    /// <summary>
    /// Update one Document
    /// </summary>
    public async Task UpdateDocument(
        DocumentWhereUniqueInput uniqueId,
        DocumentUpdateInput updateDto
    )
    {
        var document = updateDto.ToModel(uniqueId);

        _context.Entry(document).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Documents.Any(e => e.Id == document.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
