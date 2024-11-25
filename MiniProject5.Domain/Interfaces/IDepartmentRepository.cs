﻿using MiniProject5.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject5.Domain.Interfaces
{
    public interface IDepartmentRepository
    {
        IQueryable<Department> GetAllDepartment();
        Task<Department> GetDepartmentById(int deptNo);
        Task<Department> AddDepartment(Department department);
        Task<Department> UpdateDepartment(Department department);
        Task<bool> DeleteDepartment(int deptNo);
        Task<Employee> GetManagerByDeptNoAsync(int deptNo);
        Task<IEnumerable<Employee>> GetEmployee(int deptNo);
    }
}
