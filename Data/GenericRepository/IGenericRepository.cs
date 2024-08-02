using System.Linq.Expressions;

namespace MovieStore.Data.GenericRepository;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task SaveAsync();   
    Task<TEntity?> GetByIdAsync(int Id,params string[] includes);
    Task<TEntity> InsertAsync(TEntity entity);  
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task DeleteAsync(int Id);  
    Task<List<TEntity>> GetAllAsync(params string[] includes);      
    Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> expression,params string[] includes);
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression,params string[] includes);

}