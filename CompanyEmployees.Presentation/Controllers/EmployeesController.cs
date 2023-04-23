using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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

        [HttpGet("{employeeId:guid}")]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            var employee = _serviceManager.EmployeeService.GetEmployeeForCompany(companyId, employeeId, false);
            return Ok(employee);
        }
    }
}
