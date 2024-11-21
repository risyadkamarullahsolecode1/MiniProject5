using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject5.Application.Dtos
{
    public class EmployeeRegistrationDto
    {
        public int? EmpNo { get; set; }
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Sex { get; set; }
        public DateOnly? Dob { get; set; }
        public string Address { get; set; } = null!;
        public int? Salary { get; set; }
        public int? Phonenumber { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
        public int? Deptno { get; set; }
        public string? Employeetype { get; set; }
        public int? Level { get; set; }
        public List<DependentDto> Dependents { get; set; }
    }
}
