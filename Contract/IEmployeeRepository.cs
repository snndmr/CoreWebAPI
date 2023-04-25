using Entities.Models;

namespace Contract
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployeesForCompany(Guid companyId, bool trackChanges);
        Employee? GetEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
    }
}
