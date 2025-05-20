using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Shared;

public class UpdateContactDto
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; }
    
    [Required]
    [RegularExpression("^\\d{9}$", ErrorMessage = "Phone number must contain 9 digits")]
    public string PhoneNumber { get; set; }
    
    public DateOnly DateOfBirth { get; set; }

    public int CategoryId { get; set; } = 1;
    
    public string? SubCategoryName { get; set; }
}