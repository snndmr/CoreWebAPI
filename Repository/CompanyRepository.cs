using Contract;
using Entities.Models;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext context) : base(context) { }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges) => FindAll(trackChanges).OrderBy(company => company.Name).ToList();
        public Company? GetCompany(Guid companyId, bool trackChanges) => FindByCondition(company => company.Id.Equals(companyId), trackChanges).SingleOrDefault();
        public void CreateCompany(Company company) => Create(company);
    }
}
