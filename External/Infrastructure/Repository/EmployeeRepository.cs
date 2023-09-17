using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.SqlDatabase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Employee entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public async Task DeleteAsync(Employee entity)
        {
            var employee = await _dbContext.Employees.Include(x => x.Companies).FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (employee != null)
            {
                employee.Companies?.Clear();
                _dbContext.Employees.Remove(employee);
            }
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees.Include(x => x.Companies).ToListAsync();
        }

        public async Task<Employee?> GetAsync(Guid id)
        {
            return await _dbContext.Employees.Include(x => x.Companies).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Employee entity)
        {
            await Task.Run(() => _dbContext.Update(entity));
        }
    }
}
