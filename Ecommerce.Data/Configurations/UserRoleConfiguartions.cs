using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRole");
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });
        builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(ur => ur.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Role>()
               .WithMany()
               .HasForeignKey(ur => ur.RoleId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.Property(ur => ur.IsActive).IsRequired();
                builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(p => p.UserCreatedId)
               .OnDelete(DeleteBehavior.Restrict);
       builder.Property(ur => ur.CreatedAt)
               .IsRequired();
    }
}