using Entities.Models;
using Shared.RequestFeatures;

namespace Contract
{
    public interface IEmployeeRepository
    {
        Task<PagedList<Employee>> GetAllEmployeesForCompany(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);
        Task<Employee?> GetEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployeeForCompany(Employee employee);
    }
}
