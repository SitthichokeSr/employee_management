using AutoMapper;
using EmployeeManagement.Application.DTO;
using EmployeeManagement.Infrastructure.Repositories;
using MediatR;

namespace EmployeeManagement.Application.Features
{
    public class ListEmployeeQuery : IRequest<ListEmployeeResponse>
    {
        public string? keyword { get; set; }
    }

    public class ListEmployeeResponse
    {
        public int no_record { get; set; }
        public IReadOnlyList<EmployeeDto>? items { get; set; }
    }

    public class ListEmployeeQueryHandler : IRequestHandler<ListEmployeeQuery, ListEmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public ListEmployeeQueryHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<ListEmployeeResponse> Handle(ListEmployeeQuery request, CancellationToken cancellationToken)
        {
            var response = new ListEmployeeResponse();

            try
            {
                var keyword = request.keyword != null ? request.keyword.Trim() : null;

                var list = await _employeeRepository.ListAsync(keyword);

                if (list?.Any() == true)
                {
                    var items = _mapper.Map<IReadOnlyList<EmployeeDto>>(list);

                    response.items = items;
                    response.no_record = list.Count;

                    return response;
                }
            }
            catch
            {
                throw new KeyNotFoundException();
            }

            return response;
        }
    }
}
