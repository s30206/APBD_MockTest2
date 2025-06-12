using System.ComponentModel.DataAnnotations;
using ClassLibrary1.Validations;

namespace ClassLibrary1.DTOs;

public class InsertDriverCompetitionDTO
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "DriverId must be greater than 0")]
    public int DriverId { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "CompetitionId must be greater than 0")]
    public int CompetitionId { get; set; }
    [Required]
    [CurrentDate(ErrorMessage = "Current Date cannot be in the future")]
    public DateTime Date { get; set; }
}