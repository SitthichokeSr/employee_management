using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Repositories;
using MediatR;

namespace EmployeeManagement.Application.Features
{
    public class CreateEmployeeCommand : IRequest<int>
    {
        public string first_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public string designation { get; set; } = string.Empty;
        public DateTimeOffset hire_date { get; set; }
        public decimal salary { get; set; }
        public int? comm { get; set; }
        public int dept_no { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            int empNo = 0;

            var model = new Employee(empNo, request.first_name, request.last_name, request.designation, request.hire_date, request.salary, request.comm, request.dept_no);

            var result = await _employeeRepository.CreateAsync(model);

            if (result == null || result.Id <= 0)
            {
                throw new InvalidOperationException("failed to create the employee.");
            }

            return result.Id;
        }
    }
}
