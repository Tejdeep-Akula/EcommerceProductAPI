using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");
        builder.HasKey(r => r.RoleId);
        builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
        builder.Property(r => r.CreatedAt).IsRequired();
        builder.Property(r => r.IsActive).IsRequired();
                builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(p => p.UserCreatedId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.Property(r => r.UpdatedAt).IsRequired(false);
    }
}