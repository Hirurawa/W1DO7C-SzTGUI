using CarShop.Models.Entities;
using CarShop.Repository.DbContexts;
using CarShop.Repository.Interfaces;
using System.Linq;

namespace CarShop.Repository.Repositories
{
  public class BrandRepository : RepositoryBase<Brand, int>, IBrandRepository
  {
    public BrandRepository(CarShopDbContext context) : base(context)
    {
    }

    public void Delete(Brand entity)
    {
      Context.Remove(entity);
      Context.SaveChanges();
    }

    public override Brand Read(int id)
    {
      return ReadAll().SingleOrDefault(x => x.Id == id);
    }
  }
}
