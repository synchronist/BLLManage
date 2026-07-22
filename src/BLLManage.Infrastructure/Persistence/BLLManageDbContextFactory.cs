using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BLLManage.Infrastructure.Persistence;

public sealed class BLLManageDbContextFactory
    : IDesignTimeDbContextFactory<BLLManageDbContext>
{
    public BLLManageDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../BLLManage.Api"))
            .AddJsonFile("appsettings.json")
            .Build();

        var options = new DbContextOptionsBuilder<BLLManageDbContext>();

        options.UseNpgsql(
            configuration.GetConnectionString("DefaultConnection"));

        return new BLLManageDbContext(options.Options);
    }
}