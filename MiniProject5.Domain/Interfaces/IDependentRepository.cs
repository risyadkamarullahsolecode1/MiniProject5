﻿using MiniProject5.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject5.Domain.Interfaces
{
    public interface IDependentRepository
    {
        Task<IEnumerable<Dependent>> GetAllDependent();
        Task<Dependent> GetDependentById(int dependentno);
        Task<Dependent> AddDependent(Dependent dependent);
        Task<Dependent> UpdateDependent(Dependent dependent);
        Task<bool> DeleteDependent(int dependentno);
        Task SaveChangesAsync();
        Task<IEnumerable<object>> GetDependentsByEmployeeAsync(int empNo);
    }
}
