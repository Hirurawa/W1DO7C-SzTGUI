using CarShop.WpfClient.Models;
using System.Collections.Generic;

namespace CarShop.WpfClient.BL.Interfaces
{
  public interface IBrandHandlerService
  {
    void AddBrand(IList<BrandModel> collection);
    void ModifyBrand(IList<BrandModel> collection, BrandModel car);
    void DeleteBrand(IList<BrandModel> collection, BrandModel car);
    void ViewBrand(BrandModel car);
    IList<BrandModel> GetAll();
  }
}
