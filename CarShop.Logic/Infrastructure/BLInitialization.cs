using CarShop.Logic.Interfaces;
using CarShop.Logic.Services;
using CarShop.Repository.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CarShop.Logic.Infrastructure
{
  public static class BLInitialization
  {
    public static void InitBlServices(IServiceCollection services)
    {
      RepoInitialization.InitRepoServices(services);

      services.AddScoped<ICarLogic, CarLogic>();
      services.AddScoped<IBrandLogic, BrandLogic>();
    }
  }
}
