using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WlChallenge.Domain;
using WlChallenge.Domain.Data.Abstractions;
using WlChallenge.Domain.Repositories;
using WlChallenge.Infra.Data;
using WlChallenge.Infra.Repositories;

namespace WlChallenge.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(opt => { opt.UseNpgsql(Configuration.Database.ConnectionString); });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}