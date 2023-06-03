using CarShop.WpfClient.Models;
using System.Collections.Generic;

namespace CarShop.WpfClient.BL.Interfaces
{
    public interface ICarHandlerService
    {
        void AddCar(IList<CarModel> collection);
        void ModifyCar(IList<CarModel> collection, CarModel car);
        void DeleteCar(IList<CarModel> collection, CarModel car);
        void ViewCar(CarModel car);
        IList<CarModel> GetAll();

        IList<BrandModel> GetAllBrands();
        IList<AverageModel> GetBrandAverages();
    }
}
