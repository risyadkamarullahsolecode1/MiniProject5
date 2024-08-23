using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject5.Application.Dtos
{
    public class EmployeeDto
    {
        public string Fname { get; set; } = null!;
        public string Lname {  get; set; } = null!;
        public DateOnly? Dob {  get; set; }
        public string Address { get; set; } = null!;
        public int? Salary { get; set; }
        public int? Phonenumber { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
        public int? Deptno { get; set; }
        public string? Employeetype { get; set; }
        public int? Level { get; set; }
        public DateTime? Lastupdateddate { get; set; }
    }
}
