using BLLManage.Application.Interfaces;
using BLLManage.Infrastructure.Authentication;
using BLLManage.Infrastructure.Persistence;
using BLLManage.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BLLManage.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<BLLManageDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.Configure<JwtOptions>(
        configuration.GetSection(JwtOptions.SectionName));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}