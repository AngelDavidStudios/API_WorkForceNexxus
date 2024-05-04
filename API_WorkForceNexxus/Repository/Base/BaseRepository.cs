using System.Linq.Expressions;
using API_WorkForceNexxus.Data;
using API_WorkForceNexxus.Data.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using WFN.Models.Models.Base;

namespace API_WorkForceNexxus.Repository.Base;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
{
    private readonly AppDBContext _context;
    private readonly DbSet<T> _entities;
    
    public BaseRepository(AppDBContext context)
    {
        _context = context ?? throw new ArgumentException(nameof(context));
        _entities = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entities.ToListAsync();
    }

    public async Task<T> GetByIdAsync(Int64 id)
    {
        return await _entities.FindAsync(id);
    }
    public async Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }
    public void Add(T entity)
    {
        entity.CreateDate = DateTime.Now;
        entity.UpdateDate = DateTime.Now;
        _entities.Add(entity);
    }
    public void Update(T entity)
    {
        entity.UpdateDate = DateTime.Now;
        _entities.Update(entity);
    }
    public async Task Delete(Int64 id)
    {
        var entity = await _entities.FindAsync(id);
        _entities.Remove(entity);
    }
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
    }
}