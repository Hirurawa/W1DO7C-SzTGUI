using CarShop.Models.Entities;
using System.Collections.Generic;

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
