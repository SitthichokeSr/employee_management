using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeManagement.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
