using System.ComponentModel.DataAnnotations;
using ClassLibrary1.Validations;

namespace ClassLibrary1.DTOs;

public class InsertDriverDTO
{
    [Required]
    [MaxLength(200, ErrorMessage = "First Name cannot be longer than 200 characters.")]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(200, ErrorMessage = "Last Name cannot be longer than 200 characters.")]
    public string LastName { get; set; }
    [Required]
    [CurrentDate(ErrorMessage = "Current Date cannot be in the future")]
    public DateTime Birthday { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "CarId must be greater than 0")]
    public int CarId { get; set; }
}