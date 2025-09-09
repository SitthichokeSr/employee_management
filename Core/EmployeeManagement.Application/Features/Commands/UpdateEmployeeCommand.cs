using EmployeeManagement.Infrastructure.Repositories;
using MediatR;

namespace EmployeeManagement.Application.Features
{
    public class UpdateEmployeeCommand : IRequest<int>
    {
        public int id { get; set; }
        public string first_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public string designation { get; set; } = string.Empty;
        public DateTimeOffset hire_date { get; set; }
        public decimal salary { get; set; }
        public int? comm { get; set; }
        public int dept_no { get; set; }
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<int> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var response = new int();

            try
            {
                var model = await _employeeRepository.GetByIdAsync(request.id);

                if (model == null || model.Id == 0)
                {
                    throw new KeyNotFoundException($"employee with id {request.id} not found.");
                }

                model.Update(request.first_name, request.last_name, request.designation, request.hire_date, request.salary, request.comm, request.dept_no);

                await _employeeRepository.UpdateAsync(model);

                if (model.Id > 0)
                {
                    response = model.Id;
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
