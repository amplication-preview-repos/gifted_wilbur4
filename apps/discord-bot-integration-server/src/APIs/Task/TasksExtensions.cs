using DiscordBotIntegration.APIs.Dtos;
using DiscordBotIntegration.Infrastructure.Models;

namespace DiscordBotIntegration.APIs.Extensions;

public static class TasksExtensions
{
    public static Task ToDto(this TaskDbModel model)
    {
        return new Task
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static TaskDbModel ToModel(this TaskUpdateInput updateDto, TaskWhereUniqueInput uniqueId)
    {
        var task = new TaskDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            task.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            task.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return task;
    }
}
