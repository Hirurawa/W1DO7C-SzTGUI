using CarShop.Client.Infrastructure;
using CarShop.Models.Entities;
using CarShop.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarShop.Client
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Waiting for server..");
      Console.ReadLine();
      //OldTesting();
      var httpService = new HttpService("Car", "http://localhost:24577/api/");

      // Get All
      var cars = httpService.GetAll<Car>();
      DisplayCars(cars);

      // Get one
      var oneCar = httpService.Get<Car, int>(cars.First().Id);
      DisplayCar(oneCar);

      // Create
      var newCar = new Car()
      {
        Model = "Corsa",
        Price = 1345,
        BrandId = 2
      };

      var result = httpService.Create(newCar);

      if (result.IsSuccess)
      {
        Console.WriteLine("Creation was succesfull");
      }

      // Check
      cars = httpService.GetAll<Car>();
      DisplayCars(cars);

      // Update
      var carForUpdate = cars.First();
      carForUpdate.Model = "Astra H";
      carForUpdate.Price = 123568;

      result = httpService.Update(carForUpdate);

      if (result.IsSuccess)
      {
        Console.WriteLine("Update was successfull.");
      }

      // Check
      cars = httpService.GetAll<Car>();
      DisplayCars(cars);

      // Delete
      result = httpService.Delete(carForUpdate.Id);

      if (result.IsSuccess)
      {
        Console.WriteLine("Deletion was successfull.");
      }

      // Check
      cars = httpService.GetAll<Car>();
      DisplayCars(cars);

      // Get brand averages
      var brandAverages = httpService.GetAll<AverageModel>("GetBrandAverages");
      DisplayBrandAverages(brandAverages);

      Console.ReadLine();
    }

    private static void DisplayBrandAverages(List<AverageModel> brandAverages)
    {
      Console.WriteLine();

      foreach (var brandAverage in brandAverages)
      {
        Console.WriteLine(brandAverage);
      }
    }

    private static void DisplayCar(Car car)
    {
      Console.WriteLine("Id:{0}\nModel:{1}\nPrice:{2}\nBrandId:{3}", car.Id, car.Model, car.Price, car.BrandId);
    }

    private static void DisplayCars(List<Car> cars)
    {
      Console.WriteLine();

      foreach (var car in cars)
      {
        DisplayCar(car);
      }
    }

    private static void OldTesting()
    {
      var jsonOption = new JsonSerializerOptions(JsonSerializerDefaults.Web);

      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("http://localhost:24577/api/");

        var response = client.GetAsync("Car").GetAwaiter().GetResult();
        Console.WriteLine(response);

        var stringResult = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        var cars = JsonSerializer.Deserialize<List<Car>>(stringResult, jsonOption);

        DisplayCars(cars);

        // Post
        var newCar = new Car()
        {
          Model = "Corsa",
          BrandId = 1,
          Price = 1234
        };

        var postResponse = client.PostAsJsonAsync<Car>("Car", newCar).GetAwaiter().GetResult();

        var apiResult = JsonSerializer.Deserialize<ApiResult>(postResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult(), jsonOption);

        if (apiResult.IsSuccess)
        {
          Console.WriteLine("Creation was successfull");
        }
      }
    }
  }
}
