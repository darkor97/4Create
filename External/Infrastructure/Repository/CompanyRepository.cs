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
            var company = await _dbContext.Companies.FindAsync(entity.Id);
            if (company != null)
            {
                company.Employees?.Clear();
                _dbContext.Companies.Remove(company);
            }
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _dbContext.Companies.Include(x => x.Employees).ToListAsync();
        }

        public async Task<Company?> GetAsync(Guid id)
        {
            return await _dbContext.Companies.Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Company entity)
        {
            await Task.Run(() => _dbContext.Update(entity));
        }
    }
}
