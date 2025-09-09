using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Infrastructure.Repositories;

public interface IEmployeeRepository
{
    Task<Employee?> CreateAsync(Employee item);
    Task<Employee> DeleteAsync(Employee item);
    Task<Employee?> GetByIdAsync(int id);
    Task<IReadOnlyList<Employee>> ListAsync(string? keyword);
    Task<Employee?> UpdateAsync(Employee item);
}
