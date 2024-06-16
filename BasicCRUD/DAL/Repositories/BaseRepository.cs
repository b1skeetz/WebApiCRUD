using BasicCRUD.Domain.Interfaces;

namespace BasicCRUD.DAL.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly ApplicationContext _context;

    public BaseRepository(ApplicationContext context)
    {
        _context = context;
    }


    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public async Task<T> CreateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("Entity is null");
        }

        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("Entity is null");
        }
        
        _context.Update(entity);
        await _context.SaveChangesAsync();
        
        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("Entity is null");
        }
        
        _context.Remove(entity);
        await _context.SaveChangesAsync();
        
        return entity;
    }
}