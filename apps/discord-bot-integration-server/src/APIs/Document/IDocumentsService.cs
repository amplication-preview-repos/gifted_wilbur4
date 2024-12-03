using DiscordBotIntegration.APIs.Common;
using DiscordBotIntegration.APIs.Dtos;

namespace DiscordBotIntegration.APIs;

public interface IDocumentsService
{
    /// <summary>
    /// Create one Document
    /// </summary>
    public Task<Document> CreateDocument(DocumentCreateInput document);

    /// <summary>
    /// Delete one Document
    /// </summary>
    public Task DeleteDocument(DocumentWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Documents
    /// </summary>
    public Task<List<Document>> Documents(DocumentFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Document records
    /// </summary>
    public Task<MetadataDto> DocumentsMeta(DocumentFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Document
    /// </summary>
    public Task<Document> Document(DocumentWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Document
    /// </summary>
    public Task UpdateDocument(DocumentWhereUniqueInput uniqueId, DocumentUpdateInput updateDto);
}
