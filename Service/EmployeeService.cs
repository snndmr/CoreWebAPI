using AutoMapper;
using Contract;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

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

        public IEnumerable<EmployeeDto> GetAllEmployeesForCompany(Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);

            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employees = _repository.Employee.GetAllEmployeesForCompany(companyId, trackChanges);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public EmployeeDto GetEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);

            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employee = _repository.Employee.GetEmployeeForCompany(companyId, employeeId, trackChanges);

            if (employee == null)
            {
                throw new EmployeeNotFoundException(companyId, employeeId);
            }

            return _mapper.Map<EmployeeDto>(employee);
        }

        public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employeeForCreationDto, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, false);

            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employee = _mapper.Map<Employee>(employeeForCreationDto);

            _repository.Employee.CreateEmployeeForCompany(companyId, employee);
            _repository.Save();

            return _mapper.Map<EmployeeDto>(employee);
        }
    }
}
