using CarShop.Models.DTOs;
using CarShop.Models.Entities;
using CarShop.WpfClient.BL.Interfaces;
using CarShop.WpfClient.Infrastructure;
using CarShop.WpfClient.Models;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Linq;

namespace CarShop.WpfClient.BL.Implementation
{
  public class CarHandlerService : ICarHandlerService
  {
    readonly IMessenger messenger;
    readonly ICarEditorService editorService;
    readonly ICarDisplayService carDisplayService;
    HttpService httpService;

    public CarHandlerService(IMessenger messenger, ICarEditorService editorService, ICarDisplayService carDisplayService) // Should come from IoC <- dependency injection
    {
      this.messenger = messenger;
      this.editorService = editorService;
      this.carDisplayService = carDisplayService;
      httpService = new HttpService("Car", "http://localhost:24577/api/"); // This can be taken via IoC in the future
    }

    public void AddCar(IList<CarModel> collection)
    {
      CarModel carToEdit = null;
      bool operationFinished = false;

      do
      {
        var newCar = editorService.EditCar(carToEdit);

        if (newCar != null)
        {
          var operationResult = httpService.Create(new CarDTO()
          {
            BrandId = newCar.BrandId,
            Model = newCar.Model,
            Price = newCar.Price
          });

          carToEdit = newCar;
          operationFinished = operationResult.IsSuccess;

          if (operationResult.IsSuccess)
          {
            //collection.Add(newCar);
            RefreshCollectionFromServer(collection);

            SendMessage("Car add was successful");
          }
          else
          {
            SendMessage(operationResult.Messages.ToArray());
          }
        }
        else
        {
          SendMessage("Car add has cancelled");
          operationFinished = true;
        }
      } while (!operationFinished);
    }

    private void RefreshCollectionFromServer(IList<CarModel> collection)
    {
      collection.Clear();
      var newCars = GetAll();

      foreach (var car in newCars)
      {
        collection.Add(car);
      }
    }

    private void SendMessage(params string[] messages)
    {
      messenger.Send(messages, "BlOperationResult");
    }

    public void ModifyCar(IList<CarModel> collection, CarModel car)
    {
      CarModel carToEdit = car;
      bool operationFinished = false;

      do
      {
        var editedCar = editorService.EditCar(carToEdit);

        if (editedCar != null)
        {
          var operationResult = httpService.Update(new CarDTO()
          {
            Id = car.Id, // This prop cannot be changed
            BrandId = editedCar.BrandId,
            Model = editedCar.Model,
            Price = editedCar.Price
          });

          carToEdit = editedCar;
          operationFinished = operationResult.IsSuccess;

          if (operationResult.IsSuccess)
          {
            RefreshCollectionFromServer(collection);
            SendMessage("Car modification was successful");
          }
          else
          {
            SendMessage(operationResult.Messages.ToArray());
          }
        }
        else
        {
          SendMessage("Car modification has cancelled");
          operationFinished = true;
        }
      } while (!operationFinished);
    }

    public void DeleteCar(IList<CarModel> collection, CarModel car)
    {
      if (car != null)
      {
        var operationResult = httpService.Delete(car.Id);

        if (operationResult.IsSuccess)
        {
          RefreshCollectionFromServer(collection);
          SendMessage("Car deletion was successful");
        }
        else
        {
          SendMessage(operationResult.Messages.ToArray());
        }
      }
      else
      {
        SendMessage("Car deletion failed");
      }
    }

    public IList<CarModel> GetAll()
    {
      // This data comes from DB, API or something like that
      var cars = httpService.GetAll<Car>();

      return cars.Select(x => new CarModel(x.Id, x.Price, x.Model, x.BrandId)).ToList(); // TODO: use AutoMapper in the future
    }

    public IList<BrandModel> GetAllBrands()
    {
      var brands = new List<BrandModel>()
            {
                new BrandModel(1, "Mazda"),
                new BrandModel(2, "Opel"),
                new BrandModel(3, "BMW"),
            }; // TODO: get it from API endpoint!

      return brands; // Note: at this point we have to map the data
    }

    public void ViewCar(CarModel car)
    {
      carDisplayService.Display(car);
    }

    public IList<AverageModel> GetBrandAverages()
    {
      IList<AverageModel> averages = httpService.GetBrandAverages<AverageModel>();

      return averages.ToList();
    }

    public IList<CarModel> GetExpensiveCar()
    {
      return httpService.GetExpensiveCar<CarModel>();
    }
    public IList<CarModel> GetCheapCar()
    {
      return httpService.GetCheapCar<CarModel>();
    }
  }
}
