using DiscordBotIntegration.APIs.Common;
using DiscordBotIntegration.APIs.Dtos;

namespace DiscordBotIntegration.APIs;

public interface IIntegrationsService
{
    /// <summary>
    /// Create one Integration
    /// </summary>
    public Task<Integration> CreateIntegration(IntegrationCreateInput integration);

    /// <summary>
    /// Delete one Integration
    /// </summary>
    public Task DeleteIntegration(IntegrationWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Integrations
    /// </summary>
    public Task<List<Integration>> Integrations(IntegrationFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Integration records
    /// </summary>
    public Task<MetadataDto> IntegrationsMeta(IntegrationFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Integration
    /// </summary>
    public Task<Integration> Integration(IntegrationWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Integration
    /// </summary>
    public Task UpdateIntegration(
        IntegrationWhereUniqueInput uniqueId,
        IntegrationUpdateInput updateDto
    );
}
