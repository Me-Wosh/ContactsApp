using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactsApp.Backend.Data.Configurations;

// ContactSubCategories table settings and default values
public class ContactSubCategoriesConfiguration : IEntityTypeConfiguration<ContactSubCategory>
{
    public void Configure(EntityTypeBuilder<ContactSubCategory> builder)
    {
        builder.ToTable("ContactSubCategories");

        builder
            .Property(s => s.Id)
            .IsRequired();

        builder
            .Property(s => s.Name)
            .HasMaxLength(255);

        builder.HasData(
            new ContactSubCategory { Id = 1, Name = "CEO" },
            new ContactSubCategory { Id = 2, Name = "Manager" },
            new ContactSubCategory { Id = 3, Name = "Coworker" },
            new ContactSubCategory { Id = 4, Name = "Client" }
        );
    }
}