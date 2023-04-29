﻿using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployeesForCompany(Guid companyId, bool trackChanges);
        public EmployeeDto GetEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges);
        public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto? employeeForCreationDto, bool trackChanges);
        public void DeleteEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges);
    }
}
