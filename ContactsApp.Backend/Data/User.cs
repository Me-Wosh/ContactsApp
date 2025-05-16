namespace ContactsApp.Backend.Data;

// User entity
public class User
{
    // Primary Key
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}