﻿using MiniProject5.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject5.Domain.Interfaces
{
    public interface IWorksonRepository
    {
        Task<IEnumerable<Workson>> GetAllWorkson();
        Task<Workson> GetWorksonById(int empNo, int projNo);
        Task<Workson> AddWorkson(Workson workson);
        Task<Workson> UpdateWorkson(Workson workson);
        Task<bool> DeleteWorkson(int empNo, int projNo);
    }
}
