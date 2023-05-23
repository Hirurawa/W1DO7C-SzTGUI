using CarShop.WpfClient.BL.Interfaces;
using CarShop.WpfClient.Models;

namespace CarShop.WpfClient
{
    public class CarEditorViaWindowService : ICarEditorService
    {
        public CarModel EditCar(CarModel car)
        {
            var window = new CarEditorWindow(car);

            if (window.ShowDialog() == true)
            {
                return window.Car;
            }

            return null;
        }
    }
}
