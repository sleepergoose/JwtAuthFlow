using Infrastructure.EFCore.Config;
using Infrastructure.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.Contexts;

internal sealed class ReadDbContext : DbContext
{
    public DbSet<UserReadModel> Users { get; set; }

    public ReadDbContext(DbContextOptions<ReadDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Constants.DbDefaultSchema);
        modelBuilder.ApplyConfiguration(new ReadConfiguration());
    }
}
