using AutoMapper;
using EmployeeManagement.Application.DTO;
using EmployeeManagement.Infrastructure.Repositories;
using MediatR;

namespace EmployeeManagement.Application.Features
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDto>
    {
        public int id { get; set; }
    }

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new EmployeeDto();

            try
            {
                var model = await _employeeRepository.GetByIdAsync(request.id);

                if (model == null || model.Id == 0)
                {
                    throw new KeyNotFoundException($"employee with id {request.id} not found.");
                }

                response = _mapper.Map<EmployeeDto>(model);

                return response;
            }
            catch
            {
                throw new KeyNotFoundException();
            }

            return response;
        }
    }
}
