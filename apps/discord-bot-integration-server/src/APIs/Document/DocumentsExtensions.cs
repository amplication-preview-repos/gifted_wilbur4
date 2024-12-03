using DiscordBotIntegration.APIs.Dtos;
using DiscordBotIntegration.Infrastructure.Models;

namespace DiscordBotIntegration.APIs.Extensions;

public static class DocumentsExtensions
{
    public static Document ToDto(this DocumentDbModel model)
    {
        return new Document
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static DocumentDbModel ToModel(
        this DocumentUpdateInput updateDto,
        DocumentWhereUniqueInput uniqueId
    )
    {
        var document = new DocumentDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            document.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            document.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return document;
    }
}
