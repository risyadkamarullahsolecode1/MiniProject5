﻿using Microsoft.EntityFrameworkCore;
using MiniProject5.Domain.Entities;
using MiniProject5.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject5.Infrastucture.Data.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly FinancialCompanyContext _context;

        public EmployeeRepository(FinancialCompanyContext context)
        {
            _context = context;
        }
        public IQueryable<Employee> GetAllEmployee()
        {
            var employee = _context.Employees.AsQueryable();
            return employee;
        }

        public async Task<Employee> GetEmployeeById(int empNo)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Empno == empNo);
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteEmployee(int empNo)
        {
            var employee = await _context.Employees.FindAsync(empNo);
            if (employee == null)
            {
                return false;
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false); 
        }
    }
}
