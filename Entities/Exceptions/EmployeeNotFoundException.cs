namespace Entities.Exceptions
{
    public class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(Guid companyId, Guid employeeId) :
            base($"The employee with id: {employeeId} at company with id: {companyId} doesn't exist in the database.")
        { }
    }
}