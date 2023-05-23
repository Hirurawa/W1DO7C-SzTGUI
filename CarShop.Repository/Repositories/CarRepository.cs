using CarShop.Models.Entities;
using CarShop.Repository.DbContexts;
using CarShop.Repository.Interfaces;
using System.Linq;

namespace CarShop.Repository.Repositories
{
    public class CarRepository : RepositoryBase<Car, int>, ICarRepository
    {
        public CarRepository(CarShopDbContext context) : base(context)
        {
        }

        public override Car Read(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }
    }
}
