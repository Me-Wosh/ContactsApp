using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Backend.Models;

// Class to be used across API calls
public class UserDto
{
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}