﻿using AutoMapper;
using Contract;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;

        public ServiceManager(ILoggerManager logger, IMapper mapper, IRepositoryManager repositoryManager)
        {
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(mapper, logger, repositoryManager));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(mapper, logger, repositoryManager));
        }

        public ICompanyService CompanyService => _companyService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;
    }
}