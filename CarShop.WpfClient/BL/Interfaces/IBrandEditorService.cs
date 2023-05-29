using CarShop.WpfClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.WpfClient.BL.Interfaces
{
  public interface IBrandEditorService
  {
    BrandModel EditBrand(BrandModel brand);
  }
}
