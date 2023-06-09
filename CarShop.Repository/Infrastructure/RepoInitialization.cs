using CarShop.Repository.DbContexts;
using CarShop.Repository.Interfaces;
using CarShop.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CarShop.Repository.Infrastructure
{
  public static class RepoInitialization
  {
    public static void InitRepoServices(IServiceCollection services)
    {
      services.AddScoped<CarShopDbContext>((sp) => new CarShopDbContext());
      services.AddScoped<IBrandRepository, BrandRepository>();
      services.AddScoped<ICarRepository, CarRepository>();
    }
  }
}
