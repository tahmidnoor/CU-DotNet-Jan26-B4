using Microsoft.EntityFrameworkCore;
using Vagabond.Service.Exceptions;
using Vagabond.Service.Data;
using Vagabond.Service.Models;
using Vagabond.Service.Repositories;

namespace Vagabond.Service.Repositories
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly AppDbContext _context;

        public DestinationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
        {
            return await _context.Destinations.ToListAsync();
        }

        public async Task<Destination> GetByIdAsync(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);

            if (destination == null)
            {
                throw new DestinationNotFoundException($"Destination with ID {id} not found");
            }

            return destination;
        }

        public async Task AddAsync(Destination destination)
        {
            await _context.Destinations.AddAsync(destination);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Destination destination)
        {
            _context.Destinations.Update(destination);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var destination = await GetByIdAsync(id);
            _context.Destinations.Remove(destination);
            await _context.SaveChangesAsync();
        }
    }
}