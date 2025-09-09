using EmployeeManagement.Infrastructure.Repositories;
using MediatR;

namespace EmployeeManagement.Application.Features
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public int id { get; set; }
    }

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeCommandHandler(
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var response = false;

            try
            {
                var model = await _employeeRepository.GetByIdAsync(request.id);

                if (model == null || model.Id == 0)
                {
                    throw new KeyNotFoundException($"employee with id {request.id} not found.");
                }

                model.Delete();

                await _employeeRepository.DeleteAsync(model);

                if (model.Id > 0)
                {
                    return true;
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
