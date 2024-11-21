using Microsoft.EntityFrameworkCore;
using MiniProject5.Domain.Entities;
using MiniProject5.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject5.Infrastucture.Data.Repository
{
    public class LocationRepository:ILocationRepository
    {
        private readonly FinancialCompanyContext _context;

        public LocationRepository(FinancialCompanyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAllLocations()
        {
            return await _context.Locations.ToListAsync();
        }
        public async Task<Location> GetLocationById(int id)
        {
            return await _context.Locations.FindAsync(id);
        }
        public async Task<Location> AddLocation(Location location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }
        public async Task<Location> UpdateLocation(Location location)
        {
            _context.Locations.Update(location);
            await _context.SaveChangesAsync();
            return location;
        }
        public async Task DeleteLocation(Location location)
        {
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return;
        }
    }
}
