using Microsoft.EntityFrameworkCore;
using REPO.Core.Contract;
using REPO.EF.Data;


namespace REPO.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public BaseRepository(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
           return  await _context.Set<T>().FindAsync(id);
        }

        public async Task CeateAsync(T item)
        {
           await _context.Set<T>().AddAsync(item);
        }
        public async Task UpdateAsync(T item)
        {
            _context.Update(item);
        }

        public async Task<T> DeleteAsync(Guid id)
        {
            T? item =await  GetByIdAsync(id);
            if (item == null)
                return null;

            _context.Set<T>().Remove(item);
            return item;
        }

    }
}
