using Domain.Entities;
using Infrastructure.EFCore.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.Contexts;

internal sealed class WriteDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public WriteDbContext(DbContextOptions<WriteDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Constants.DbDefaultSchema);
        modelBuilder.ApplyConfiguration(new WriteConfiguration());
    }
}
