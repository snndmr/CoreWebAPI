using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies/{companyId:guid}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EmployeesController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public IActionResult GetAllEmployeesForCompany(Guid companyId)
        {
            var employees = _serviceManager.EmployeeService.GetAllEmployeesForCompany(companyId, false);
            return Ok(employees);
        }

        [HttpGet("{employeeId:guid}", Name = "GetEmployeeForCompany")]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            var employee = _serviceManager.EmployeeService.GetEmployeeForCompany(companyId, employeeId, false);
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployee(Guid companyId, [FromBody] EmployeeForCreationDto employeeForCreationDto)
        {
            if (employeeForCreationDto == null)
            {
                return BadRequest("EmployeeForCreationDto is null");
            }

            var employee = _serviceManager.EmployeeService.CreateEmployeeForCompany(companyId, employeeForCreationDto, false);
            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, employeeId = employee.Id }, employee);
        }
    }
}
