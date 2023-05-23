using CarShop.Endpoint.Services;
using CarShop.Logic.Interfaces;
using CarShop.Models.DTOs;
using CarShop.Models.Entities;
using CarShop.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;

namespace CarShop.Endpoint.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        readonly ICarLogic carLogic;
        IHubContext<SignalRHub> hub;

    public CarController(ICarLogic carLogic, IHubContext<SignalRHub> hub)
        {
            this.carLogic = carLogic;
            this.hub = hub;
    }

        // GET: api/Car/GetAll
        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<Car> Get()
        {
            return carLogic.ReadAll();
        }

        // GET api/Car/Get/5
        [HttpGet("{id}")]
        [ActionName("Get")]
        public Car Get(int id)
        {
            return carLogic.Read(id);
        }

        // POST api/Car/Create
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Post(CarDTO car)
        {
            var result = new ApiResult(true);

            try
            {
                var toCreate = new Car()
                {
                  Id = car.Id,
                  BrandId = car.BrandId,
                  Model = car.Model,
                  Price = car.Price
                };
                carLogic.Create(toCreate);
                this.hub.Clients.All.SendAsync("CarCreated", toCreate);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Messages = new List<string>() { ex.Message };
            }

            return result;
        }

        // PUT api/Car/Update
        [HttpPut]
        [ActionName("Update")]
        public ApiResult Put(CarDTO car)
        {
            var result = new ApiResult(true);

            try
            {
              var toUpdate = new Car()
              {
                Id = car.Id,
                BrandId = car.BrandId,
                Model = car.Model,
                Price = car.Price
              };
                carLogic.Update(toUpdate);
              this.hub.Clients.All.SendAsync("CarUpdated", toUpdate);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Messages = new List<string>() { ex.Message };
            }

            return result;
        }

        // DELETE api/Car/Delete/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public ApiResult Delete(int id)
        {
            var result = new ApiResult(true);

            try
            {
              var toDelete = this.carLogic.Read(id);
              carLogic.Delete(id);
              this.hub.Clients.All.SendAsync("CarDeleted", toDelete);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Messages = new List<string>() { ex.Message };
            }

            return result;
        }

        // GET: api/Car/GetBrandAverages
        [HttpGet]
        public IEnumerable<AverageModel> GetBrandAverages()
        {
            return carLogic.GetBrandAverages();
        }

        [HttpGet]
        public IEnumerable<Brand> GetAllBrands()
        {
            // TODO: get it from DB
            return new Brand[]
            {
                new Brand() { Id = 1, Name = "Mazda" },
                new Brand() { Id = 2, Name = "Opel" },
                new Brand() { Id = 3, Name = "BMW" },
            };
        }
    }
}
