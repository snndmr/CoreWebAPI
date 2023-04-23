using AutoMapper;
using Contract;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;

        public CompanyService(IMapper mapper, ILoggerManager logger, IRepositoryManager repository)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
            var companies = _repository.Company.GetAllCompanies(trackChanges);
            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        public CompanyDto GetCompany(Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            return company == null ? throw new CompanyNotFoundException(companyId) : _mapper.Map<CompanyDto>(company);
        }
    }
}
