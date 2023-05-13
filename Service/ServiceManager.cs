using AutoMapper;
using Contract;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(ILoggerManager logger, IMapper mapper, IRepositoryManager repositoryManager,
            IDataShaper<EmployeeDto> dataShaper, UserManager<User> userManager, IConfiguration configuration)
        {
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(mapper, logger, repositoryManager));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(mapper, logger, repositoryManager, dataShaper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(mapper, logger, configuration, userManager));
        }

        public ICompanyService CompanyService => _companyService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
