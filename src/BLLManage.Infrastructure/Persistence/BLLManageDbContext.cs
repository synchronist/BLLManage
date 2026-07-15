using BLLManage.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace BLLManage.Infrastructure.Persistence;

public sealed class BLLManageDbContext : DbContext
{
    public BLLManageDbContext(DbContextOptions<BLLManageDbContext> options)
        : base(options)
    {
    }

    public DbSet<Company> Companies => Set<Company>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BLLManageDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}