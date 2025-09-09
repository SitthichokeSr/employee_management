using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Employee> CreateAsync(string firstName, string lastName, string designation, DateTimeOffset hireDate, decimal salary, int? comm, int deptNo)
    {
        var listEmployee = _dbContext.Employees.Where(x => x.IsEnabled).AsQueryable();

        var isExisting = await listEmployee.Where(x => x.FIRST_NAME == firstName && x.LAST_NAME == lastName).ToListAsync();

        if (isExisting?.Any() == true)
        {
            throw new KeyNotFoundException($"employee with first name {firstName} and last name {lastName} already exists");
        }

        int latestEmpNo = await listEmployee.OrderByDescending(x => x.EMPNO).Select(x => x.EMPNO).FirstOrDefaultAsync();

        var model = new Employee(latestEmpNo, firstName, lastName, designation, hireDate, salary, comm, deptNo);

        await _dbContext.Employees.AddAsync(model);
        await _dbContext.SaveChangesAsync();

        return model;
    }

    public async Task<Employee> DeleteAsync(Employee item)
    {
        _dbContext.Employees.Update(item);
        await _dbContext.SaveChangesAsync();

        return item;
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        var item = await _dbContext.Employees.FindAsync(id);

        return item;
    }

    public async Task<IReadOnlyList<Employee>> ListAsync(string? keyword)
    {
        var items = await _dbContext.Employees
            .Where(x => String.IsNullOrEmpty(keyword) ||
                (!String.IsNullOrEmpty(keyword) && (x.EMPNO.ToString().Contains(keyword) || x.FIRST_NAME.Contains(keyword) || x.LAST_NAME.Contains(keyword) || x.DESIGNATION.Contains(keyword))))
            .OrderBy(x => x.EMPNO)
            .ThenBy(x => x.FIRST_NAME)
            .ToListAsync();

        return items;
    }

    public async Task<Employee> UpdateAsync(Employee item)
    {
        var listEmployee = _dbContext.Employees.Where(x => x.IsEnabled).AsQueryable();

        var isExisting = await listEmployee.Where(x => x.Id != item.Id && x.FIRST_NAME == item.FIRST_NAME && x.LAST_NAME == item.LAST_NAME).ToListAsync();

        if (isExisting?.Any() == true)
        {
            throw new KeyNotFoundException($"employee with first name {item.FIRST_NAME} and last name {item.LAST_NAME} already exists");
        }

        _dbContext.Employees.Update(item);
        await _dbContext.SaveChangesAsync();

        return item;
    }
}
