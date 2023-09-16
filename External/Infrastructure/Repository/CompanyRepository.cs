using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.SqlDatabase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CompanyRepository : IRepository<Company>
    {
        private readonly AppDbContext _dbContext;

        public CompanyRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Company entity)
        {
            await _dbContext.Companies.AddAsync(entity);
        }

        public async Task DeleteAsync(Company entity)
        {
            await Task.Run(() => _dbContext.Companies.Remove(entity));
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _dbContext.Companies.ToListAsync();
        }

        public async Task<Company?> GetAsync(Guid id)
        {
            return await _dbContext.Companies.FindAsync(id);
        }

        public async Task UpdateAsync(Company entity)
        {
            await Task.Run(() => _dbContext.Update(entity));
        }
    }
}
