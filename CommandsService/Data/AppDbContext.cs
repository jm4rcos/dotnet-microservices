using Microsoft.EntityFrameworkCore;
using CommandsService.Models;

namespace CommandsService.Data
{
  public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
  {
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Command> Commands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder
        .Entity<Platform>()
        .HasMany(p => p.Commands)
        .WithOne(p => p.Platform!)
        .HasForeignKey(p => p.PlatformId);

      modelBuilder
        .Entity<Command>()
        .HasOne(p => p.Platform)
        .WithMany(p => p.Commands)
        .HasForeignKey(p => p.PlatformId);
    }
  }
}