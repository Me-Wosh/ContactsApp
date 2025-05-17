namespace ContactsApp.Backend.Models;

// Contact class to be used across PATCH API calls
public class UpdateContactDto
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int CategoryId { get; set; }
    public string? SubCategoryName { get; set; }
}