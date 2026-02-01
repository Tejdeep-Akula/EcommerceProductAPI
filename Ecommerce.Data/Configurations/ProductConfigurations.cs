using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        builder.HasKey(p => p.ProductId);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(1000);
        builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(p => p.StockQuantity).IsRequired();
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.IsActive).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired(false);
        builder.HasOne<Category>()
               .WithMany()
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(p => p.UserCreatedId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(p => new { p.CategoryId, p.IsActive })
            .HasDatabaseName("Product_CategoryId_IsActive_Index");
    }
}