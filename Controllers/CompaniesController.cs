using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers;

[ApiController]
[Route("api/[companies]")]
public class CompaniesController : ControllerBase
{
    private IRepositoryManager _repository;
    private ILoggerManager _logger;
    
    public CompaniesController(IRepositoryManager repository, ILoggerManager loggerManager)
    {
        _repository = repository;
        _logger = loggerManager;
    }

    [HttpGet]
    public IActionResult GetCompanies()
    {
        try
        {
            var companies = _repository.Company.GetAllCompanies(trackChanges: false);
            return Ok(companies);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong in {nameof(GetCompanies)} action {ex}");
            return StatusCode(500, "Internal server error.");
        } 
    }
}