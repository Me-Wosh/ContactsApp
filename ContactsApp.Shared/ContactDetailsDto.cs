namespace ContactsApp.Shared;

// Contact class to be used across GET single contact API calls
public class ContactDetailsDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string? SubCategoryName { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
}