using AutoMapper;
using Contract;
using Entities.Exceptions;
using Entities.Models;
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

        public CompanyDto CreateCompany(CompanyForCreationDto companyForCreationDto)
        {
            var company = _mapper.Map<Company>(companyForCreationDto);

            _repository.Company.CreateCompany(company);
            _repository.Save();

            return _mapper.Map<CompanyDto>(company);
        }

        public IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids == null)
            {
                throw new IdParametersBadRequestException();
            }

            var enumerable = ids.ToList();
            var companies = _repository.Company.GetByIds(enumerable, trackChanges);

            if (enumerable.Count != companies.Count())
            {
                throw new CollectionByIdsBadRequestException();
            }

            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        public (IEnumerable<CompanyDto> companyDtos, string ids) CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyForCreationDtos)
        {
            if (companyForCreationDtos == null)
            {
                throw new CompanyCollectionBadRequest();
            }

            var companies = _mapper.Map<IEnumerable<Company>>(companyForCreationDtos);

            foreach (var company in companies)
            {
                _repository.Company.CreateCompany(company);
            }

            _repository.Save();

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companies).ToList();
            return (companiesToReturn, string.Join(",", companiesToReturn.Select(company => company.Id)));
        }

        public void DeleteCompany(Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);

            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            _repository.Company.DeleteCompany(company);
            _repository.Save();
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
