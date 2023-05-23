using CarShop.Models.Models;
using CarShop.Models.Entities;
using System.Collections.Generic;

namespace CarShop.Logic.Interfaces
{
    public interface ICarLogic
    {
        IList<Car> ReadAll();

        Car Read(int id);

        Car Create(Car entity);

        Car Update(Car entity);

        void Delete(int id);

        IEnumerable<AverageModel> GetBrandAverages();

        List<Car> ReadAllByBrandId(int brandId);
    }
}
