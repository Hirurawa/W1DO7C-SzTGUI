using CarShop.Models.Entities;

namespace CarShop.Repository.Interfaces
{
    public interface IBrandRepository : IRepositoryBase<Brand, int>
    {
        void Delete(Brand entity);
    }
}
