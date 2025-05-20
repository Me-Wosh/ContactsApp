namespace ContactsApp.Shared;

// Contact class to be used across GET all contacts API calls
public class ContactDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}