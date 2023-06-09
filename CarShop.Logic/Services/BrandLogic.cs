using CarShop.Logic.Interfaces;
using CarShop.Models.Entities;
using CarShop.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarShop.Logic.Services
{
  public class BrandLogic : IBrandLogic
  {
    IBrandRepository _brandRepository;

    public BrandLogic(IBrandRepository brandRepository)
    {
      _brandRepository = brandRepository;
    }

    public Brand Create(Brand entity)
    {
      if (entity.Name == null)
      {
        throw new ApplicationException("Please enter a name!");
      }

      var result = _brandRepository.Create(entity);

      return result;
    }

    public void Delete(int id)
    {
      _brandRepository.Delete(id);
    }

    public Brand Read(int id)
    {
      return _brandRepository.Read(id);
    }

    public IList<Brand> ReadAll()
    {
      return _brandRepository.ReadAll().ToList();
    }

    public Brand Update(Brand entity)
    {
      var result = _brandRepository.Update(entity);

      return result;
    }
  }
}
