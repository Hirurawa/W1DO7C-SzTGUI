using CarShop.Repository.DbContexts;
using CarShop.Repository.Interfaces;
using System.Linq;

namespace CarShop.Repository.Repositories
{
  public abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
  {
    protected CarShopDbContext Context;

    protected RepositoryBase(CarShopDbContext context)
    {
      Context = context;
    }

    public IQueryable<TEntity> ReadAll()
    {
      return Context.Set<TEntity>();
    }

    public abstract TEntity Read(TKey id);

    public TEntity Create(TEntity entity)
    {
      var result = Context.Add(entity);
      Context.SaveChanges();
      return result.Entity;
    }

    public TEntity Update(TEntity entity)
    {
      var result = Context.Update(entity);
      Context.SaveChanges();
      return result.Entity;
    }

    public void Delete(TKey id)
    {
      Context.Remove(Read(id));
      Context.SaveChanges();
    }
  }
}
