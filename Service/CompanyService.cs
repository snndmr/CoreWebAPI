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

        public async Task<CompanyDto> CreateCompany(CompanyForCreationDto companyForCreationDto)
        {
            var company = _mapper.Map<Company>(companyForCreationDto);

            _repository.Company.CreateCompany(company);
            await _repository.SaveAsync();

            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<IEnumerable<CompanyDto>> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids == null)
            {
                throw new IdParametersBadRequestException();
            }

            var enumerable = ids.ToList();
            var companies = await _repository.Company.GetByIds(enumerable, trackChanges);

            if (enumerable.Count != companies.Count())
            {
                throw new CollectionByIdsBadRequestException();
            }

            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        public async Task<(IEnumerable<CompanyDto> companyDtos, string ids)> CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyForCreationDtos)
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

            await _repository.SaveAsync();

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companies).ToList();
            return (companiesToReturn, string.Join(",", companiesToReturn.Select(company => company.Id)));
        }

        public async Task DeleteCompany(Guid companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCompany(companyId, trackChanges);

            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            _repository.Company.DeleteCompany(company);
            await _repository.SaveAsync();
        }

        public async Task UpdateCompany(Guid companyId, CompanyForUpdateDto? companyForUpdateDto, bool trackChanges)
        {
            var company = await _repository.Company.GetCompany(companyId, trackChanges);

            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            _mapper.Map(companyForUpdateDto, company);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompanies(bool trackChanges)
        {
            var companies = await _repository.Company.GetAllCompanies(trackChanges);
            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        public async Task<CompanyDto> GetCompany(Guid companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCompany(companyId, trackChanges);
            return company == null ? throw new CompanyNotFoundException(companyId) : _mapper.Map<CompanyDto>(company);
        }
    }
}
