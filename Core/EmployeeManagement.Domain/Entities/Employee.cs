namespace EmployeeManagement.Domain.Entities;

public sealed class Employee
{
    public int Id { get; set; }
    public int EMPNO { get; set; }
    public string FIRST_NAME { get; set; }
    public string LAST_NAME { get; set; }
    public string DESIGNATION { get; set; }
    public DateTimeOffset HIREDATE { get; set; }
    public decimal SALARY { get; set; }
    public int? COMM { get; set; }
    public int DEPTNO { get; set; }
    public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset Modified { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? Deleted { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsEnabled { get; set; } = true;

    public Employee() { }

    public Employee(int empNo, string firstName, string lastName, string designation, DateTimeOffset hireDate, decimal salary, int? comm, int deptNo)
    {
        this.EMPNO = empNo;
        this.FIRST_NAME = firstName;
        this.LAST_NAME = lastName;
        this.DESIGNATION = designation;
        this.HIREDATE = hireDate;
        this.SALARY = salary;
        this.COMM = comm;
        this.DEPTNO = deptNo;
        this.Created = DateTimeOffset.Now;
        this.Modified = DateTimeOffset.Now;
    }

    public void Update(string firstName, string lastName, string designation, DateTimeOffset hireDate, decimal salary, int? comm, int deptNo)
    {
        this.FIRST_NAME = firstName;
        this.LAST_NAME = lastName;
        this.DESIGNATION = designation;
        this.HIREDATE = hireDate;
        this.SALARY = salary;
        this.COMM = comm;
        this.DEPTNO = deptNo;
        this.Modified = DateTimeOffset.Now;
    }

    public void Delete()
    {
        this.IsDeleted = true;
        this.Deleted = DateTimeOffset.Now;
    }
}
