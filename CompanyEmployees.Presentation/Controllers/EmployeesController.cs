using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<IActionResult> GetAllEmployeesForCompany(Guid companyId)
        {
            var employees = await _serviceManager.EmployeeService.GetAllEmployeesForCompany(companyId, false);
            return Ok(employees);
        }

        [HttpGet("{employeeId:guid}", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            var employee = await _serviceManager.EmployeeService.GetEmployeeForCompany(companyId, employeeId, false);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Guid companyId, [FromBody] EmployeeForCreationDto? employeeForCreationDto)
        {
            if (employeeForCreationDto == null)
            {
                return BadRequest("EmployeeForCreationDto is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var employee = await _serviceManager.EmployeeService.CreateEmployeeForCompany(companyId, employeeForCreationDto);
            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, employeeId = employee.Id }, employee);
        }

        [HttpDelete("{employeeId:guid}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            await _serviceManager.EmployeeService.DeleteEmployeeForCompany(companyId, employeeId, false);
            return NoContent();
        }

        [HttpPut("{employeeId:guid}")]
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid employeeId, [FromBody] EmployeeForUpdateDto? employeeForUpdateDto)
        {
            if (employeeForUpdateDto == null)
            {
                return BadRequest("EmployeeForUpdateDto is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var employee = await _serviceManager.EmployeeService.UpdateEmployeeForCompany(companyId, employeeId, employeeForUpdateDto, false, true);
            return Ok(employee);
        }

        [HttpPatch("{employeeId:guid}")]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid employeeId, [FromBody] JsonPatchDocument<EmployeeForUpdateDto>? patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("patchDoc is null");
            }

            var (employeeToPatch, employee) = await _serviceManager.EmployeeService.GetEmployeeForPatch(companyId, employeeId, false, true);
            patchDoc.ApplyTo(employeeToPatch, ModelState);

            TryValidateModel(employeeToPatch);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            await _serviceManager.EmployeeService.SaveChangesForPatch(employeeToPatch, employee);
            return NoContent();
        }
    }
}
