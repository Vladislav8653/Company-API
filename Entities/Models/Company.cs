using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Company // некоторые поля сделал required, по учебнику этого не было
{
    [Column("CompanyId")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Company name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public required string Name { get; set; }
    [Required(ErrorMessage = "Company address is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters")]
    public required string Address { get; set; }
    [MaxLength(60, ErrorMessage = "Maximum length for the Country is 60 characters")]
    public required string Country { get; set; }
    public ICollection<Employee> Employees { get; set; } = [];

}