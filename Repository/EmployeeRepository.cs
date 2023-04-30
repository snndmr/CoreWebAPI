using Contract;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context) : base(context) { }

        public async Task<IEnumerable<Employee>> GetAllEmployeesForCompany(Guid companyId, bool trackChanges) =>
            await FindByCondition(employee => employee.CompanyId.Equals(companyId), trackChanges).OrderBy(employee => employee.Name).ToListAsync();

        public async Task<Employee?> GetEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges) =>
            await FindByCondition(employee => employee.CompanyId.Equals(companyId) && employee.Id.Equals(employeeId), trackChanges).SingleOrDefaultAsync();

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployeeForCompany(Employee employee) => Delete(employee);
    }
}
