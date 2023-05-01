using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        Task<(IEnumerable<EmployeeDto> employeeDtos, MetaData metaData)> GetAllEmployeesForCompany(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);
        Task<EmployeeDto> GetEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges);
        Task<EmployeeDto> CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto? employeeForCreationDto);
        Task DeleteEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges);
        Task<EmployeeDto> UpdateEmployeeForCompany(Guid companyId, Guid employeeId, EmployeeForUpdateDto employeeForUpdateDto, bool trackCompanyChanges, bool trackEmployeeChanges);
        Task<(EmployeeForUpdateDto employeeToPatch, Employee employee)> GetEmployeeForPatch(Guid companyId, Guid employeeId, bool trackCompanyChanges, bool trackEmployeeChanges);
        Task SaveChangesForPatch(EmployeeForUpdateDto employeeForUpdateDto, Employee employee);
    }
}
