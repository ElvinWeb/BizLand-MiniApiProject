using ApiProject.Core.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Core.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        public DbSet<TEntity> Table { get; }
        void Delete(TEntity entity);
        Task CreateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>>? expression, params string[]? includes);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression, params string[]? includes);
        IQueryable<TEntity> GetAllAsyncAsQueryable(Expression<Func<TEntity, bool>>? expression, params string[]? includes);
        Task<int> SaveChanges();
    }
}
