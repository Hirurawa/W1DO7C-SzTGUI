using CarShop.Logic.Interfaces;
using CarShop.Models.Entities;
using CarShop.Models.Models;
using CarShop.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarShop.Logic.Services
{
    public class CarLogic : ICarLogic
    {
        ICarRepository _carRepository;
        IBrandRepository _brandRepository;

        public CarLogic(ICarRepository carRepository, IBrandRepository brandRepository)
        {
            _carRepository = carRepository;
            _brandRepository = brandRepository;
        }

        public IList<Car> ReadAll()
        {
            return _carRepository.ReadAll().ToList();
        }

        public Car Read(int id)
        {
            return _carRepository.Read(id);
        }

        public Car Create(Car entity)
        {
            // TODO check access

            // Validations
            if (entity.BrandId == 0)
            {
                throw new ApplicationException("Please choose brand!");
            }

            // TODO: check brand existance

            if (String.IsNullOrWhiteSpace(entity.Model))
            {
                throw new ApplicationException("Model is required!");
            }

            if (entity.Price < 0)
            {
                throw new ApplicationException("Price must be greater or equal to 0!");
            }

            var result = _carRepository.Create(entity);

            // TODO: log

            return result;
        }
        public Car Update(Car entity)
        {
            // TODO check access

            // TODO: validate !!!

            // TODO: map

            var result = _carRepository.Update(entity);

            // TODO: log

            return result;
        }

        public void Delete(int id)
        {
            // TODO check access

            // TODO: check relations

            _carRepository.Delete(id);
        }

        public IEnumerable<AverageModel> GetBrandAverages()
        {
            var averages = from car in _carRepository.ReadAll()
                           group car by car.BrandId into grouped
                           select new
                           {
                               BrandId = grouped.Key,
                               Average = grouped.Average(x => x.Price)
                           };

            //var result = from brand in _brandRepository.ReadAll()
            //             join average in averages
            //                on brand.Id equals average.BrandId
            //             select new AverageModel()
            //             {
            //                 BrandName = brand.Name,
            //                 Average = average.Average
            //             };

            var result = from brand in _brandRepository.ReadAll()
                         from average in averages.Where(x => x.BrandId == brand.Id).DefaultIfEmpty()
                         select new AverageModel()
                         {
                             BrandName = brand.Name,
                             Average = average != null ? average.Average : 0
                         };

            return result.ToList();
        }

        public List<Car> ReadAllByBrandId(int brandId)
        {
            return _carRepository.ReadAll().Where(x => x.BrandId == brandId).ToList();
        }
    }
}
