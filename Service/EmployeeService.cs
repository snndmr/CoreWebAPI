using Contract;
using Service.Contracts;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;

        public CompanyService(ILoggerManager logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }
    }
}
