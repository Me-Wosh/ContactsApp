using ContactsApp.Backend.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Backend.Data;

// Database configuration
public class ContactsDbContext : DbContext
{
    public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options) { }
    
    // Database Tables
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ContactCategory> ContactCategories { get; set; }
    public DbSet<ContactSubCategory> ContactSubCategories { get; set; }
    public DbSet<User> Users { get; set; }

    // Tables settings
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContactsConfiguration());
        modelBuilder.ApplyConfiguration(new ContactCategoriesConfiguration());
        modelBuilder.ApplyConfiguration(new ContactSubCategoriesConfiguration());
        modelBuilder.ApplyConfiguration(new UsersConfiguration());
    }
}