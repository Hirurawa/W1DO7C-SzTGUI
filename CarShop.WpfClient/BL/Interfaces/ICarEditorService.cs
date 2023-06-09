using CarShop.WpfClient.Models;

namespace CarShop.WpfClient.BL.Interfaces
{
  public interface ICarEditorService
  {
    CarModel EditCar(CarModel car);
  }
}
