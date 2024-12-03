using DiscordBotIntegration.APIs.Common;
using DiscordBotIntegration.APIs.Dtos;

namespace DiscordBotIntegration.APIs;

public interface ITasksService
{
    /// <summary>
    /// Create one Task
    /// </summary>
    public Task<Task> CreateTask(TaskCreateInput task);

    /// <summary>
    /// Delete one Task
    /// </summary>
    public Task DeleteTask(TaskWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Tasks
    /// </summary>
    public Task<List<Task>> Tasks(TaskFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Task records
    /// </summary>
    public Task<MetadataDto> TasksMeta(TaskFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Task
    /// </summary>
    public Task<Task> Task(TaskWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Task
    /// </summary>
    public Task UpdateTask(TaskWhereUniqueInput uniqueId, TaskUpdateInput updateDto);
}
