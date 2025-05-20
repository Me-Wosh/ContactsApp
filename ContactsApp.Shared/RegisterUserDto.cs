using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Shared;

// User class to be used across register API calls
public class RegisterUserDto
{
    // allowed characters:
    // letters: a-z, A-Z
    // digits: 0-9
    // special characters: <<space>>!"#$%&'()*+,-./:;<=>?@[\]^_`{|}~
        
    // rules:
    // length: 12-255
    // at least one lowercase letter
    // at least one uppercase letter
    // at least one digit
    // at least one special character
    // characters only from the allowed characters list
    private const string PasswordRegex =
        "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[ !\"#$%&'()*+,\\-./:;<=>?@\\[\\\\\\]^_`{|}~])[a-zA-Z\\d !\"#$%&'()*+,\\-./:;<=>?@\\[\\\\\\]^_`{|}~]{12,255}$";
    
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(255)]
    [RegularExpression(PasswordRegex, ErrorMessage = "Your password doesn't follow the rules.")]
    public string Password { get; set; }
}