using CarShop.WpfClient.BL.Interfaces;
using CarShop.WpfClient.Models;

namespace CarShop.WpfClient
{
  public class BrandDisplayService : IBrandDisplayService
  {
    public void Display(BrandModel brand)
    {
      var window = new BrandEditorWindow(brand, false);

      window.Show();
    }
  }
}
