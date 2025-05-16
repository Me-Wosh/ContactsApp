using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactsApp.Backend.Data.Configurations;

// ContactCategories table settings and default values
public class ContactCategoriesConfiguration : IEntityTypeConfiguration<ContactCategory>
{
    public void Configure(EntityTypeBuilder<ContactCategory> builder)
    {
        builder.ToTable("ContactCategories");

        builder
            .Property(c => c.Id)
            .IsRequired();

        builder
            .Property(c => c.Name)
            .HasMaxLength(255);

        builder.HasData(
            new ContactCategory { Id = 1, Name = "Business" },
            new ContactCategory { Id = 2, Name = "Personal" },
            new ContactCategory { Id = 3, Name = "Other" }
        );
    }
}