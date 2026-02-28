using URLShortener.Models.Common;

namespace URLShortener.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public Task<TEntity> CreateAsync(TEntity entity);
    public void Delete(TEntity entity);
}
