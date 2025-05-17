namespace ContactsApp.Backend.Models;

// Contact class to be used across GET API calls
public class ContactDto
{
    public int Id { get; set; } // guid?
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string CategoryName { get; set; }
    public string? SubCategoryName { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
}