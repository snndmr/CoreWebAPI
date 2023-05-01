using Contract;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context) : base(context) { }

        public async Task<PagedList<Employee>> GetAllEmployeesForCompany(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employees = await FindByCondition(employee => employee.CompanyId.Equals(companyId), trackChanges)
                .FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge)
                .SearchEmployees(employeeParameters.SearchTerm)
                .SortEmployees(employeeParameters.OrderBy)
                .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
                .Take(employeeParameters.PageSize)
                .ToListAsync();

            var count = await FindByCondition(employee => employee.CompanyId.Equals(companyId), trackChanges).CountAsync();
            return PagedList<Employee>.ToPagedList(employees, count, employeeParameters.PageNumber, employeeParameters.PageSize);
        }

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
