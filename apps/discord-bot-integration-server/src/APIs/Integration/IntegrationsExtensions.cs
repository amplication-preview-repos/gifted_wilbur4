using DiscordBotIntegration.APIs.Dtos;
using DiscordBotIntegration.Infrastructure.Models;

namespace DiscordBotIntegration.APIs.Extensions;

public static class IntegrationsExtensions
{
    public static Integration ToDto(this IntegrationDbModel model)
    {
        return new Integration
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static IntegrationDbModel ToModel(
        this IntegrationUpdateInput updateDto,
        IntegrationWhereUniqueInput uniqueId
    )
    {
        var integration = new IntegrationDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            integration.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            integration.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return integration;
    }
}
