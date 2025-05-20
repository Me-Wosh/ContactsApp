using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Shared;

// Contact class to be used across POST API calls
public class CreateContactDto
{
    [MaxLength(255)]
    [Required]
    public string FirstName { get; set; }
    
    [MaxLength(255)]
    [Required]
    public string LastName { get; set; }
    
    [MaxLength(255)]
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [RegularExpression("^\\d{9}$", ErrorMessage = "Phone number must contain 9 digits")]
    public string PhoneNumber { get; set; }
    
    public DateOnly DateOfBirth { get; set; }
    
    public int CategoryId { get; set; } = 1;
    
    [MaxLength(255)]
    public string? SubCategoryName { get; set; }
}