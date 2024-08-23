using MiniProject5.Application.Dtos;
using MiniProject5.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject5.Application.Mappers
{
    public static class EmployeeSortedMapper
    {
        public static EmployeeSortedDto ToEmployeeSortedDto(this Employee employee, Department department)
        {
            return new EmployeeSortedDto
            {
                Fname = employee.Fname,
                Deptname = department.Deptname,
                Position = employee.Position,
                Employeetype = employee.Employeetype,
                Level = employee.Level,
                Lastupdateddate = employee.Lastupdateddate
            };
        }
    }
}
