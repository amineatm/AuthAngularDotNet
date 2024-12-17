using AuthECApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthECApi.Extensions;

public static class EFCoreExtensions
{
    public static IServiceCollection InjectDbContext(this IServiceCollection services, IConfiguration config)
    {

        services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
        return services;

    }


}