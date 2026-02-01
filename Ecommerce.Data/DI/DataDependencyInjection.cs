using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public static class DataDependencyInjection
{
    public static void RegisterDataDependency(IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<ConnectionStrings>(
            configuration.GetSection("ConnectionStrings"));
        services.AddDbContext<EcommerceDbContext>((sp, options) =>
        {
            var conn = sp.GetRequiredService<IOptions<ConnectionStrings>>().Value;
            options.UseSqlServer(conn.SqlConnectionString);
        });
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
    }
}
