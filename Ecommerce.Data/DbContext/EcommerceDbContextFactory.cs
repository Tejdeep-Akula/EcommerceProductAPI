using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class EcommerceDbContextFactory
    : IDesignTimeDbContextFactory<EcommerceDbContext>
{
    public EcommerceDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EcommerceDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=EcommerceDb;User Id=sa;Password=Product775@password;TrustServerCertificate=True;"
        );

        return new EcommerceDbContext(optionsBuilder.Options);
    }
    // hii
}
