using DiscordBotIntegration.APIs;
using DiscordBotIntegration.APIs.Common;
using DiscordBotIntegration.APIs.Dtos;
using DiscordBotIntegration.APIs.Errors;
using DiscordBotIntegration.APIs.Extensions;
using DiscordBotIntegration.Infrastructure;
using DiscordBotIntegration.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscordBotIntegration.APIs;

public abstract class TasksServiceBase : ITasksService
{
    protected readonly DiscordBotIntegrationDbContext _context;

    public TasksServiceBase(DiscordBotIntegrationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Task
    /// </summary>
    public async Task<Task> CreateTask(TaskCreateInput createDto)
    {
        var task = new TaskDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            task.Id = createDto.Id;
        }

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<TaskDbModel>(task.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Task
    /// </summary>
    public async Task DeleteTask(TaskWhereUniqueInput uniqueId)
    {
        var task = await _context.Tasks.FindAsync(uniqueId.Id);
        if (task == null)
        {
            throw new NotFoundException();
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Tasks
    /// </summary>
    public async Task<List<Task>> Tasks(TaskFindManyArgs findManyArgs)
    {
        var tasks = await _context
            .Tasks.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return tasks.ConvertAll(task => task.ToDto());
    }

    /// <summary>
    /// Meta data about Task records
    /// </summary>
    public async Task<MetadataDto> TasksMeta(TaskFindManyArgs findManyArgs)
    {
        var count = await _context.Tasks.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Task
    /// </summary>
    public async Task<Task> Task(TaskWhereUniqueInput uniqueId)
    {
        var tasks = await this.Tasks(
            new TaskFindManyArgs { Where = new TaskWhereInput { Id = uniqueId.Id } }
        );
        var task = tasks.FirstOrDefault();
        if (task == null)
        {
            throw new NotFoundException();
        }

        return task;
    }

    /// <summary>
    /// Update one Task
    /// </summary>
    public async Task UpdateTask(TaskWhereUniqueInput uniqueId, TaskUpdateInput updateDto)
    {
        var task = updateDto.ToModel(uniqueId);

        _context.Entry(task).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Tasks.Any(e => e.Id == task.Id))
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
