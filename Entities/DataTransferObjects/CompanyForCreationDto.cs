using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public class CompanyForCreationDto
{
    [Required(ErrorMessage = "Name is a required field")]
    [MaxLength (30, ErrorMessage = "Name max length is 30")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Address is a required field")]
    [MaxLength (30, ErrorMessage = "Address max length is 30")]
    public string Address { get; set; }
    
    [Required(ErrorMessage = "Country is a required field")]
    [MaxLength (30, ErrorMessage = "Country max length is 30")]
    public string Country { get; set; }
    
    public IEnumerable<EmployeeForCreationDto>? Employees { get; set; }
}