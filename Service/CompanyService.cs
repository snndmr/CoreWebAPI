using Contract;
using Service.Contracts;

namespace Service
{
    public sealed class EmployeeService : IEmployeeService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;

        public EmployeeService(ILoggerManager logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }
    }
}
