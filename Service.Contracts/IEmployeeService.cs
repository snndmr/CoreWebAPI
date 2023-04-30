﻿using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployeesForCompany(Guid companyId, bool trackChanges);
        public EmployeeDto GetEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges);
        public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto? employeeForCreationDto);
        public void DeleteEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges);
        public EmployeeDto UpdateEmployeeForCompany(Guid companyId, Guid employeeId, EmployeeForUpdateDto employeeForUpdateDto, bool trackCompanyChanges, bool trackEmployeeChanges);
        public (EmployeeForUpdateDto employeeToPatch, Employee employee) GetEmployeeForPatch(Guid companyId, Guid employeeId, bool trackCompanyChanges, bool trackEmployeeChanges);
        public void SaveChangesForPatch(EmployeeForUpdateDto employeeForUpdateDto, Employee employee);
    }
}
