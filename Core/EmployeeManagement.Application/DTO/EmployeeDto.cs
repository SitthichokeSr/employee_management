namespace EmployeeManagement.Application.DTO;

public class EmployeeDto
{
    public int Id { get; set; }
    public int EMPNO { get; set; }
    public string FIRST_NAME { get; set; } = string.Empty;
    public string LAST_NAME { get; set; } = string.Empty;
    public string DESIGNATION { get; set; } = string.Empty;
    public DateTimeOffset HIREDATE { get; set; }
    public decimal SALARY { get; set; }
    public int COMM { get; set; }
    public int DEPTNO { get; set; }
}
