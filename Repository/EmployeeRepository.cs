using Contract;
using Entities.Models;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context) : base(context) { }

        public IEnumerable<Employee> GetAllEmployeesForCompany(Guid companyId, bool trackChanges) =>
            FindByCondition(employee => employee.CompanyId.Equals(companyId), trackChanges).OrderBy(employee => employee.Name).ToList();

        public Employee? GetEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges) =>
            FindByCondition(employee => employee.CompanyId.Equals(companyId) && employee.Id.Equals(employeeId), trackChanges).SingleOrDefault();

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }
    }
}
