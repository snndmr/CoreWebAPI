using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public abstract record EmployeeForManipulationDto
{
    [Required(ErrorMessage = "Name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the 'Name' is 30 characters.")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "Age is a required field.")]
    [Range(18, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 18")]
    public int Age { get; init; }

    [Required(ErrorMessage = "Position is a required field.")]
    [MaxLength(50, ErrorMessage = "Maximum length for the 'Position' is 50 characters.")]
    public string? Position { get; init; }
}