using System.Linq.Expressions;
using WFN.Models.Models.Base;

namespace API_WorkForceNexxus.Data.Interfaces.Base;

public interface IBaseRepository<T> where T : BaseModel
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Int64 id);
    Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void Update(T entity);
    Task Delete(Int64 id);
    Task<bool> SaveChangesAsync();
}