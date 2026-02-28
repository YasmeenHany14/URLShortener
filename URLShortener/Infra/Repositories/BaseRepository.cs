using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using URLShortener.Interfaces.Repositories;
using URLShortener.Models.Common;

namespace URLShortener.Infra.Repositories;

public class BaseRepository<TEntity>(UrlContext context) : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    // add, delete
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    // assume hard delete for now
    // TODO:think about soft delete later
    public void Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }
}
