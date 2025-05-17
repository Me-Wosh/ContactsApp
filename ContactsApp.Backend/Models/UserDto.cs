namespace ContactsApp.Backend.Models;

// User class to be used across register/login API calls
public class UserDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}