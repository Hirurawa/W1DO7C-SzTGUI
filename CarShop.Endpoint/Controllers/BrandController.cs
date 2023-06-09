using CarShop.Endpoint.Services;
using CarShop.Logic.Interfaces;
using CarShop.Models.DTOs;
using CarShop.Models.Entities;
using CarShop.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarShop.Endpoint.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class BrandController : ControllerBase
  {
    readonly IBrandLogic brandLogic;
    readonly ICarLogic carLogic;
    IHubContext<SignalRHub> hub;

    public BrandController(ICarLogic carLogic, IBrandLogic brandLogic, IHubContext<SignalRHub> hub)
    {
      this.carLogic = carLogic;
      this.brandLogic = brandLogic;
      this.hub = hub;
    }

    // GET: api/Brand/GetAll
    [HttpGet]
    [ActionName("GetAll")]
    public IEnumerable<Brand> Get()
    {
      return brandLogic.ReadAll();
    }

    // GET api/Brand/Get/5
    [HttpGet("{id}")]
    [ActionName("Get")]
    public Brand Get(int id)
    {
      return brandLogic.Read(id);
    }

    // POST api/Brand/Create
    [HttpPost]
    [ActionName("Create")]
    public ApiResult Post(BrandDTO brand)
    {
      var result = new ApiResult(true);

      try
      {
        var toCreate = new Brand()
        {
          Id = brand.Id,
          Name = brand.Name
        };
        brandLogic.Create(toCreate);
        this.hub.Clients.All.SendAsync("BrandCreated", toCreate);
      }
      catch (Exception ex)
      {
        result.IsSuccess = false;
        result.Messages = new List<string>() { ex.Message };
      }

      return result;
    }

    // PUT api/Brand/Update
    [HttpPut]
    [ActionName("Update")]
    public ApiResult Put(BrandDTO brand)
    {
      var result = new ApiResult(true);

      try
      {
        var toUpdate = new Brand()
        {
          Id = brand.Id,
          Name = brand.Name
        };
        brandLogic.Update(toUpdate);
        this.hub.Clients.All.SendAsync("BrandUpdated", toUpdate);
      }
      catch (Exception ex)
      {
        result.IsSuccess = false;
        result.Messages = new List<string>() { ex.Message };
      }

      return result;
    }

    // DELETE api/Brand/Delete/5
    [HttpDelete("{id}")]
    [ActionName("Delete")]
    public ApiResult Delete(int id)
    {
      var result = new ApiResult(true);

      try
      {
        var toDelete = this.brandLogic.Read(id);
        var carsToDelete = this.carLogic.ReadAll().Where(c => c.BrandId == toDelete.Id);
        foreach (var car in carsToDelete)
        {
          this.carLogic.Delete(car.Id);
        }
        brandLogic.Delete(id);
        this.hub.Clients.All.SendAsync("BrandDeleted", toDelete);
      }
      catch (Exception ex)
      {
        result.IsSuccess = false;
        result.Messages = new List<string>() { ex.Message };
      }

      return result;
    }
  }
}
