using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllCompanies(bool trackChanges);
        Task<CompanyDto> GetCompany(Guid companyId, bool trackChanges);
        Task<CompanyDto> CreateCompany(CompanyForCreationDto companyForCreationDto);
        Task<IEnumerable<CompanyDto>> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        Task<(IEnumerable<CompanyDto> companyDtos, string ids)> CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyForCreationDtos);
        Task DeleteCompany(Guid companyId, bool trackChanges);
        Task UpdateCompany(Guid companyId, CompanyForUpdateDto? companyForUpdateDto, bool trackChanges);
    }
}
