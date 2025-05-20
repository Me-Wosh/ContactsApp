using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Shared;

// User class to be used across login API calls
public class LoginUserDto
{
    [Required]
    [MaxLength(255)]
    // no [EmailAddress] annotation in case it implementation changed and user could not login
    public string Email { get; set; }
    
    [Required]
    [MaxLength(255)]
    // no [RegularExpression] annotation to prevent against brute force attacks
    public string Password { get; set; }
}