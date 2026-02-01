using Microsoft.Extensions.DependencyInjection;
public static class CoreDependencyInjection{
    
    public static void RegisterCoreDependencies(IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAdminUserService, AdminUserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<AuthService>();
        services.AddScoped<JwtTokenGenerator>();
    }
}