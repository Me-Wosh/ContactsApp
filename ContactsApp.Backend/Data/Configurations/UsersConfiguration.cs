using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactsApp.Backend.Data.Configurations;

// Users table settings
public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder
            .Property(u => u.Id)
            .IsRequired();

        builder
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(u => u.PasswordHash)
            .IsRequired();
    }
}