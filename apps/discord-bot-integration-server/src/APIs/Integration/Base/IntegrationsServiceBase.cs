using DiscordBotIntegration.APIs;
using DiscordBotIntegration.APIs.Common;
using DiscordBotIntegration.APIs.Dtos;
using DiscordBotIntegration.APIs.Errors;
using DiscordBotIntegration.APIs.Extensions;
using DiscordBotIntegration.Infrastructure;
using DiscordBotIntegration.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscordBotIntegration.APIs;

public abstract class IntegrationsServiceBase : IIntegrationsService
{
    protected readonly DiscordBotIntegrationDbContext _context;

    public IntegrationsServiceBase(DiscordBotIntegrationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Integration
    /// </summary>
    public async Task<Integration> CreateIntegration(IntegrationCreateInput createDto)
    {
        var integration = new IntegrationDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            integration.Id = createDto.Id;
        }

        _context.Integrations.Add(integration);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<IntegrationDbModel>(integration.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Integration
    /// </summary>
    public async Task DeleteIntegration(IntegrationWhereUniqueInput uniqueId)
    {
        var integration = await _context.Integrations.FindAsync(uniqueId.Id);
        if (integration == null)
        {
            throw new NotFoundException();
        }

        _context.Integrations.Remove(integration);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Integrations
    /// </summary>
    public async Task<List<Integration>> Integrations(IntegrationFindManyArgs findManyArgs)
    {
        var integrations = await _context
            .Integrations.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return integrations.ConvertAll(integration => integration.ToDto());
    }

    /// <summary>
    /// Meta data about Integration records
    /// </summary>
    public async Task<MetadataDto> IntegrationsMeta(IntegrationFindManyArgs findManyArgs)
    {
        var count = await _context.Integrations.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Integration
    /// </summary>
    public async Task<Integration> Integration(IntegrationWhereUniqueInput uniqueId)
    {
        var integrations = await this.Integrations(
            new IntegrationFindManyArgs { Where = new IntegrationWhereInput { Id = uniqueId.Id } }
        );
        var integration = integrations.FirstOrDefault();
        if (integration == null)
        {
            throw new NotFoundException();
        }

        return integration;
    }

    /// <summary>
    /// Update one Integration
    /// </summary>
    public async Task UpdateIntegration(
        IntegrationWhereUniqueInput uniqueId,
        IntegrationUpdateInput updateDto
    )
    {
        var integration = updateDto.ToModel(uniqueId);

        _context.Entry(integration).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Integrations.Any(e => e.Id == integration.Id))
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
