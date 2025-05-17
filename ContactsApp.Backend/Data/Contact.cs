namespace ContactsApp.Backend.Data;

// Contact entity
public class Contact
{
    // Primary Key
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
    
    // Foreign Key (one-to-many)
    public int CategoryId { get; set; }
    public ContactCategory Category { get; set; }

    // Foreign Key (one-to-many) (optional)
    public int? SubCategoryId { get; set; }
    public ContactSubCategory? SubCategory { get; set; }
}