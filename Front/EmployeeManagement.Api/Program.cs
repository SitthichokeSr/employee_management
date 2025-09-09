using EmployeeManagement.Api.Middlewares;
using EmployeeManagement.Application;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructure(configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase(databaseName: "EmployeeTestDb");
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

await SeedInitialData(app);

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();

async Task SeedInitialData(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

        if (context == null) return;

        if (context.Employees.Any())
        {
            return;
        }

        var employees = new List<Employee>
        {
            new Employee {EMPNO = 1001, FIRST_NAME = "STEFAN", LAST_NAME = "SALVATORE", DESIGNATION = "BUSSINESS ANALYST", HIREDATE = new DateTimeOffset(1997, 11, 17, 0, 0, 0, TimeSpan.FromHours(7)), SALARY = 50000, COMM = null, DEPTNO = 40},
            new Employee {EMPNO = 1002, FIRST_NAME = "DIANA", LAST_NAME = "LORRENCE", DESIGNATION = "TECHNIAL ARCHITECT", HIREDATE = new DateTimeOffset(2000, 5, 1, 0, 0, 0, TimeSpan.FromHours(7)), SALARY = 70000, COMM = null, DEPTNO = 10},
            new Employee {EMPNO = 1003, FIRST_NAME = "JAMES", LAST_NAME = "MADINSAON", DESIGNATION = "MANAGER", HIREDATE = new DateTimeOffset(1988, 6, 19, 0, 0, 0, TimeSpan.FromHours(7)), SALARY = 80400, COMM = null, DEPTNO = 40},
            new Employee {EMPNO = 1005, FIRST_NAME = "LUCY", LAST_NAME = "GELLLER", DESIGNATION = "HR ASSOCIATE", HIREDATE = new DateTimeOffset(2008, 7, 13, 0, 0, 0, TimeSpan.FromHours(7)), SALARY = 35000, COMM = 200, DEPTNO = 30},
            new Employee {EMPNO = 1006, FIRST_NAME = "ISAAC", LAST_NAME = "STEFAN", DESIGNATION = "TRAINEE", HIREDATE = new DateTimeOffset(2015, 12, 13, 0, 0, 0, TimeSpan.FromHours(7)), SALARY = 20000, COMM = null, DEPTNO = 40},
            new Employee {EMPNO = 1007, FIRST_NAME = "JOHN", LAST_NAME = "SMITH", DESIGNATION = "CLERK", HIREDATE = new DateTimeOffset(2005, 12, 17, 0, 0, 0, TimeSpan.FromHours(7)), SALARY = 12000, COMM = null, DEPTNO = 10},
            new Employee {EMPNO = 1008, FIRST_NAME = "NANCY", LAST_NAME = "GILLBERT", DESIGNATION = "SALESMAN", HIREDATE = new DateTimeOffset(1981, 2, 20, 0, 0, 0, TimeSpan.FromHours(7)), SALARY = 1600, COMM = 300, DEPTNO = 10},
            new Employee {EMPNO = 1004, FIRST_NAME = "JONES", LAST_NAME = "NICK", DESIGNATION = "HR ANALYST", HIREDATE = new DateTimeOffset(2003, 1, 2, 0, 0, 0, TimeSpan.FromHours(7)), SALARY = 47000, COMM = null, DEPTNO = 30},
        };

        context.Employees.AddRange(employees);

        await context.SaveChangesAsync();
    }
}
