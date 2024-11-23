namespace ServerOfSchool.Repository
{
    using Microsoft.EntityFrameworkCore;
    using ServerOfSchool.Data;
    using System.Collections.Generic;
    using System.Threading.Tasks;

       
       
    using static ServerOfSchool.Interfaces.IRepository;

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual void Remove(T entity)
        {

        }


        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}

