using Entities.Models;

namespace Contract
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesForCompany(Guid companyId, bool trackChanges);
        Task<Employee?> GetEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployeeForCompany(Employee employee);
    }
}
