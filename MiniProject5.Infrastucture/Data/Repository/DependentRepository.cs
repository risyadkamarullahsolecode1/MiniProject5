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
    public class DependentRepository:IDependentRepository
    {
        private readonly FinancialCompanyContext _context;

        public DependentRepository(FinancialCompanyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dependent>> GetAllDependent()
        {
            return await _context.Dependents.ToListAsync();
        }
        public async Task<Dependent> GetDependentById(int dependantno)
        {
            return await _context.Dependents.FindAsync(dependantno);
        }
        public async Task<Dependent> AddDependent(Dependent dependant)
        {
            _context.Dependents.Add(dependant);
            await _context.SaveChangesAsync();
            return dependant;
        }
        public async Task<Dependent> UpdateDependent(Dependent dependant)
        {
            _context.Dependents.Update(dependant);
            await _context.SaveChangesAsync();
            return dependant;
        }
        public async Task<bool> DeleteDependent(int dependantno)
        {
            var dependant = await _context.Dependents.FindAsync(dependantno);
            if (dependant == null)
            {
                return false;
            }
            _context.Dependents.Remove(dependant);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false); 
        }
    }
}
