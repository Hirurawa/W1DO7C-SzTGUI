using CarShop.WpfClient.BL.Interfaces;
using CarShop.WpfClient.Models;

namespace CarShop.WpfClient
{
    public class BrandEditorViaWindowService : IBrandEditorService
    {
        public BrandModel EditBrand(BrandModel brand)
        {
            var window = new BrandEditorWindow(brand);

            if (window.ShowDialog() == true)
            {
                return window.Brand;
            }

            return null;
        }
    }
}
