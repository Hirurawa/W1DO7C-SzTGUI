using CarShop.Endpoint.Services;
using CarShop.Logic.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CarShop.Endpoint
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSignalR();
      services.AddControllers();
      services.AddCors();

      BLInitialization.InitBlServices(services);
      //services.AddScoped<ICarLogic, CarLogic>();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Endpoint", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Endpoint v1"));
      }
      app.UseExceptionHandler(c => c.Run(async context =>
      {
        var exception = context.Features
            .Get<IExceptionHandlerPathFeature>()
            .Error;
        var response = new { Msg = exception.Message };
        await context.Response.WriteAsJsonAsync(response);
      }));

      app.UseCors(x => x
       .AllowCredentials()
       .AllowAnyMethod()
       .AllowAnyHeader()
       .WithOrigins("http://localhost:53028"));

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapHub<SignalRHub>("/hub");
      });
    }
  }
}
