using AutoMapper;
using Contract;
using Service.Contracts;

namespace Service
{
    public sealed class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;

        public EmployeeService(IMapper mapper, ILoggerManager logger, IRepositoryManager repository)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }
    }
}
