using CarShop.WpfClient.BL.Interfaces;
using CarShop.WpfClient.Models;

namespace CarShop.WpfClient
{
    public class CarDisplayService : ICarDisplayService
    {
        public void Display(CarModel car)
        {
            var window = new CarEditorWindow(car, false);

            window.Show();
        }
    }
}
