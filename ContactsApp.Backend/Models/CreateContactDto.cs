using ContactsApp.Backend.Data;

namespace ContactsApp.Backend.Models;

public class CreateContactDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public ContactCategory Category { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}