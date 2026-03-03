using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using URLShortener.Interfaces.Repositories;
using URLShortener.Models.Common;

namespace URLShortener.Infra.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly UrlContext Context;
    protected BaseRepository(UrlContext context) => Context = context;
    
    // add, delete
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    // assume hard delete for now
    // TODO:think about soft delete later
    public void Delete(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await Context.SaveChangesAsync();
    }
}
