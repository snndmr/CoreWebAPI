using AutoMapper;
using Contract;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service
{
    public sealed class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;

        public EmployeeService(IMapper mapper, ILoggerManager logger, IRepositoryManager repository)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        private async Task CheckIfCompanyExists(Guid companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCompany(companyId, trackChanges);

            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }
        }

        private async Task<Employee> GetEmployeeForCompanyAndCheckIfItExists(Guid companyId, Guid employeeId, bool trackCompanyChanges = false, bool trackEmployeeChanges = false)
        {
            var company = await _repository.Company.GetCompany(companyId, trackCompanyChanges);

            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employee = await _repository.Employee.GetEmployeeForCompany(companyId, employeeId, trackEmployeeChanges);

            if (employee == null)
            {
                throw new EmployeeNotFoundException(companyId, employeeId);
            }

            return employee;
        }

        public async Task<(IEnumerable<EmployeeDto> employeeDtos, MetaData metaData)> GetAllEmployeesForCompany(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);
            var employeesWithMetaData = await _repository.Employee.GetAllEmployeesForCompany(companyId, employeeParameters, trackChanges);
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);

            return (employeeDtos, metaData: employeesWithMetaData.MetaData);
        }

        public async Task<EmployeeDto> GetEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var employee = await GetEmployeeForCompanyAndCheckIfItExists(companyId, employeeId, trackChanges);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto? employeeForCreationDto)
        {
            await CheckIfCompanyExists(companyId, false);

            var employee = _mapper.Map<Employee>(employeeForCreationDto);

            _repository.Employee.CreateEmployeeForCompany(companyId, employee);
            await _repository.SaveAsync();

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task DeleteEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var employee = await GetEmployeeForCompanyAndCheckIfItExists(companyId, employeeId, trackChanges);
            _repository.Employee.DeleteEmployeeForCompany(employee);
            await _repository.SaveAsync();
        }

        public async Task<EmployeeDto> UpdateEmployeeForCompany(Guid companyId, Guid employeeId, EmployeeForUpdateDto employeeForUpdateDto, bool trackCompanyChanges, bool trackEmployeeChanges)
        {
            var employee = await GetEmployeeForCompanyAndCheckIfItExists(companyId, employeeId, trackCompanyChanges, trackEmployeeChanges);
            _mapper.Map(employeeForUpdateDto, employee);
            await _repository.SaveAsync();

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employee)> GetEmployeeForPatch(Guid companyId, Guid employeeId, bool trackCompanyChanges, bool trackEmployeeChanges)
        {
            var employee = await GetEmployeeForCompanyAndCheckIfItExists(companyId, employeeId, trackCompanyChanges, trackEmployeeChanges);
            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employee);
            return (employeeToPatch, employee);
        }

        public async Task SaveChangesForPatch(EmployeeForUpdateDto employeeToPatch, Employee employee)
        {
            _mapper.Map(employeeToPatch, employee);
            await _repository.SaveAsync();
        }
    }
}
