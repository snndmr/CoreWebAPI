using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CompaniesController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _serviceManager.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);
        }

        [HttpGet("{companyId:guid}", Name = "CompanyById")]
        public IActionResult GetCompany(Guid companyId)
        {
            var company = _serviceManager.CompanyService.GetCompany(companyId, trackChanges: false);
            return Ok(company);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto companyForCreationDto)
        {
            if (companyForCreationDto is null)
            {
                return BadRequest("CompanyForCreationDto object is null");
            }

            var createdCompany = _serviceManager.CompanyService.CreateCompany(companyForCreationDto);
            return CreatedAtRoute("CompanyById", new { companyId = createdCompany.Id }, createdCompany);
        }
    }
}
