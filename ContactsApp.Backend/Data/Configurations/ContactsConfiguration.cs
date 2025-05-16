using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactsApp.Backend.Data.Configurations;

// Contacts table settings and default values
public class ContactsConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");

        builder
            .Property(c => c.Id)
            .IsRequired();

        builder
            .Property(c => c.FirstName)
            .HasMaxLength(50);
        
        builder
            .Property(c => c.LastName)
            .HasMaxLength(100);
        
        builder
            .Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .HasIndex(c => c.Email)
            .IsUnique();
        
        builder
            .Property(c => c.PhoneNumber)
            .HasMaxLength(9);

        builder.HasData(
            new Contact { Id = 1, FirstName = "Anna", LastName = "Kowalska", Email = "anna.kowalska92@gmail.com", PhoneNumber = "512345678", DateOfBirth = new DateOnly(1992, 5, 14), CategoryId = 1, SubCategoryId = 1 },
            new Contact { Id = 2, FirstName = "Michał", LastName = "Nowak", Email = "michal.nowak87@wp.pl", PhoneNumber = "601234567", DateOfBirth = new DateOnly(1987, 11, 3), CategoryId = 1, SubCategoryId = 2 },
            new Contact { Id = 3, FirstName = "Katarzyna", LastName = "Wiśniewska", Email = "k.wisniewska@onet.pl", PhoneNumber = "789456123", DateOfBirth = new DateOnly(1990, 2, 27), CategoryId = 1, SubCategoryId = 3  },
            new Contact { Id = 4, FirstName = "Piotr", LastName = "Zieliński", Email = "piotr.zielinski@gmail.com", PhoneNumber = "693321789", DateOfBirth = new DateOnly(1989, 7, 19), CategoryId = 1, SubCategoryId = 3  },
            new Contact { Id = 5, FirstName = "Agnieszka", LastName = "Dąbrowska", Email = "agnieszka.dabrowska@interia.pl", PhoneNumber = "530987654", DateOfBirth = new DateOnly(1991, 3, 9), CategoryId = 1, SubCategoryId = 4 },
            new Contact { Id = 6, FirstName = "Tomasz", LastName = "Wójcik", Email = "tomwojcik90@o2.pl", PhoneNumber = "665443221", DateOfBirth = new DateOnly(1990, 10, 21), CategoryId = 1, SubCategoryId = 4 },
            new Contact { Id = 7, FirstName = "Magdalena", LastName = "Kamińska", Email = "magda.kaminska@onet.eu", PhoneNumber = "507111333", DateOfBirth = new DateOnly(1993, 8, 5), CategoryId = 2 },
            new Contact { Id = 8, FirstName = "Krzysztof", LastName = "Lewandowski", Email = "krz.lewandowski@wp.pl", PhoneNumber = "602556778", DateOfBirth = new DateOnly(1985, 12, 30), CategoryId = 2 },
            new Contact { Id = 9, FirstName = "Paulina", LastName = "Jankowska", Email = "paulina.jankowska89@gmail.com", PhoneNumber = "723998445", DateOfBirth = new DateOnly(1989, 4, 16), CategoryId = 2 },
            new Contact { Id = 10, FirstName = "Jakub", LastName = "Mazur", Email = "jakub.mazur@interia.eu", PhoneNumber = "511223344", DateOfBirth = new DateOnly(1994, 6, 11), CategoryId = 2 }
        );
    }
}