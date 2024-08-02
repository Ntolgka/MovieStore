using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Base;
using MovieStore.Data.Context;

namespace MovieStore.Data.GenericRepository;

internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }   

        public async Task<TEntity?> GetByIdAsync(int id, params string[] includes)
        {
            var query = dbContext.Set<TEntity>().AsQueryable();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            entity.IsActive = true;
            entity.InsertDate = DateTime.UtcNow;
            await dbContext.Set<TEntity>().AddAsync(entity);
            return entity;  
        }

        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await dbContext.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                dbContext.Set<TEntity>().Remove(entity);
            }
        }
    
        public async Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> expression, params string[] includes)
        {
            var query = dbContext.Set<TEntity>().Where(expression).AsQueryable();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.ToListAsync();
        }
        
        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, params string[] includes)   
        {
            var query = dbContext.Set<TEntity>().AsQueryable();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<List<TEntity>> GetAllAsync(params string[] includes)
        {
            var query = dbContext.Set<TEntity>().AsQueryable();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.ToListAsync();
        }
    }