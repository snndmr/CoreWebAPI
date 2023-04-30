using Contract;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext context) : base(context) { }

        public async Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges) => await FindAll(trackChanges).OrderBy(company => company.Name).ToListAsync();
        public async Task<Company?> GetCompany(Guid companyId, bool trackChanges) => await FindByCondition(company => company.Id.Equals(companyId), trackChanges).SingleOrDefaultAsync();
        public void CreateCompany(Company company) => Create(company);
        public async Task<IEnumerable<Company>> GetByIds(IEnumerable<Guid> ids, bool trackChanges) => await FindByCondition(company => ids.Contains(company.Id), trackChanges).ToListAsync();
        public void DeleteCompany(Company company) => Delete(company);
    }
}
