using DiscordBotIntegration.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiscordBotIntegration.Infrastructure;

public class DiscordBotIntegrationDbContext : IdentityDbContext<IdentityUser>
{
    public DiscordBotIntegrationDbContext(DbContextOptions<DiscordBotIntegrationDbContext> options)
        : base(options) { }

    public DbSet<DocumentDbModel> Documents { get; set; }

    public DbSet<TaskDbModel> Tasks { get; set; }

    public DbSet<IntegrationDbModel> Integrations { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
