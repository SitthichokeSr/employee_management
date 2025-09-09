using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Infrastructure
{
    public static class DbInitialData
    {
        public static void InitialData(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Employees.Any())
            {
                var employees = new List<Employee>
                {
                    new Employee {EMPNO = 1001, FIRST_NAME = "STEFAN", LAST_NAME = "SALVATORE", DESIGNATION = "BUSSINESS ANALYST", HIREDATE = new DateTime(1997, 11, 17), SALARY = 50000, COMM = 0, DEPTNO = 40},
                    new Employee {EMPNO = 1002, FIRST_NAME = "DIANA", LAST_NAME = "LORRENCE", DESIGNATION = "TECHNIAL ARCHITECT", HIREDATE = new DateTime(2000, 5, 1), SALARY = 70000, COMM = 0, DEPTNO = 10},
                    new Employee {EMPNO = 1003, FIRST_NAME = "JAMES", LAST_NAME = "MADINSAON", DESIGNATION = "MANAGER", HIREDATE = new DateTime(1988, 6, 19), SALARY = 80400, COMM = 0, DEPTNO = 40},
                    new Employee {EMPNO = 1005, FIRST_NAME = "LUCY", LAST_NAME = "GELLLER", DESIGNATION = "HR ASSOCIATE", HIREDATE = new DateTime(2008, 7, 13), SALARY = 35000, COMM = 200, DEPTNO = 30},
                    new Employee {EMPNO = 1006, FIRST_NAME = "ISAAC", LAST_NAME = "STEFAN", DESIGNATION = "TRAINEE", HIREDATE = new DateTime(2015, 12, 13), SALARY = 20000, COMM = 0, DEPTNO = 40},
                    new Employee {EMPNO = 1007, FIRST_NAME = "JOHN", LAST_NAME = "SMITH", DESIGNATION = "CLERK", HIREDATE = new DateTime(2005, 12, 17), SALARY = 12000, COMM = 0, DEPTNO = 10},
                    new Employee {EMPNO = 1008, FIRST_NAME = "NANCY", LAST_NAME = "GILLBERT", DESIGNATION = "SALESMAN", HIREDATE = new DateTime(1981, 2, 20), SALARY = 1600, COMM = 300, DEPTNO = 10},
                    new Employee {EMPNO = 1004, FIRST_NAME = "JONES", LAST_NAME = "NICK", DESIGNATION = "HR ANALYST", HIREDATE = new DateTime(2003, 1, 2), SALARY = 47000, COMM = 0, DEPTNO = 30},
                };

                context.Employees.AddRange(employees);
                context.SaveChangesAsync();
            }
        }
    }
}
