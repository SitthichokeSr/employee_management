using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EmployeeManagement.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Employee?> CreateAsync(Employee item)
    {
        try
        {
            var isExisting = await _dbContext.Employees.AnyAsync(x => x.IsEnabled && x.FIRST_NAME == item.FIRST_NAME && x.LAST_NAME == item.LAST_NAME);

            if (isExisting)
            {
                throw new InvalidOperationException($"Employee with first name {item.FIRST_NAME} and last name {item.LAST_NAME} already exists");
            }

            // condition for test, this approach has a potential race condition.
            int latestEmpNo = await _dbContext.Employees.OrderByDescending(x => x.EMPNO).Select(x => x.EMPNO).FirstOrDefaultAsync();

            item.EMPNO = latestEmpNo + 1;

            await _dbContext.Employees.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }
        catch (Exception e)
        {
            Debug.WriteLine($"An error occurred: {e.Message}");
            return null;
        }
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

    public async Task<Employee?> UpdateAsync(Employee item)
    {
        try
        {
            var isExisting = await _dbContext.Employees.AnyAsync(x => x.IsEnabled && x.FIRST_NAME == item.FIRST_NAME && x.LAST_NAME == item.LAST_NAME);

            if (isExisting)
            {
                throw new InvalidOperationException($"Employee with first name {item.FIRST_NAME} and last name {item.LAST_NAME} already exists");
            }

            _dbContext.Employees.Update(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }
        catch (Exception e)
        {
            Debug.WriteLine($"An error occurred: {e.Message}");
            return null;
        }
    }
}
