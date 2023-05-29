using CarShop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Logic.Interfaces
{
  public interface IBrandLogic
  {
    IList<Brand> ReadAll();

    Brand Read(int id);

    Brand Create(Brand entity);

    Brand Update(Brand entity);

    void Delete(int id);
  }
}
