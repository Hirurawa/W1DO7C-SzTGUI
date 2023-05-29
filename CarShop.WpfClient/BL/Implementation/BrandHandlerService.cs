using CarShop.Models.DTOs;
using CarShop.Models.Entities;
using CarShop.WpfClient.BL.Interfaces;
using CarShop.WpfClient.Infrastructure;
using CarShop.WpfClient.Models;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CarShop.WpfClient.BL.Implementation
{
  public class BrandHandlerService : IBrandHandlerService
  {
    readonly IMessenger messenger;
    readonly IBrandEditorService editorService;
    readonly IBrandDisplayService brandDisplayService;
    HttpService httpService;

    public BrandHandlerService(IMessenger messenger, IBrandEditorService editorService, IBrandDisplayService brandDisplayService) // Should come from IoC <- dependency injection
    {
      this.messenger = messenger;
      this.editorService = editorService;
      this.brandDisplayService = brandDisplayService;
      httpService = new HttpService("Brand", "http://localhost:24577/api/"); // This can be taken via IoC in the future
    }

    public void AddBrand(IList<BrandModel> collection)
    {
      BrandModel brandToEdit = null;
      bool operationFinished = false;

      do
      {
        var newBrand = editorService.EditBrand(brandToEdit);

        if (newBrand != null)
        {
          var operationResult = httpService.Create(new BrandDTO()
          {
            Id = newBrand.Id,
            Name = newBrand.Name
          });

          brandToEdit = newBrand;
          operationFinished = operationResult.IsSuccess;

          if (operationResult.IsSuccess)
          {
            //collection.Add(newBrand);
            RefreshCollectionFromServer(collection);

            SendMessage("Brand add was successful");
          }
          else
          {
            SendMessage(operationResult.Messages.ToArray());
          }
        }
        else
        {
          SendMessage("Brand add has cancelled");
          operationFinished = true;
        }
      } while (!operationFinished);
    }

    private void RefreshCollectionFromServer(IList<BrandModel> collection)
    {
      collection.Clear();
      var newBrands = GetAll();

      foreach (var brand in newBrands)
      {
        collection.Add(brand);
      }
    }

    private void SendMessage(params string[] messages)
    {
      messenger.Send(messages, "BlOperationResult");
    }

    public void ModifyBrand(IList<BrandModel> collection, BrandModel brand)
    {
      BrandModel brandToEdit = brand;
      bool operationFinished = false;

      do
      {
        var editedBrand = editorService.EditBrand(brandToEdit);

        if (editedBrand != null)
        {
          var operationResult = httpService.Update(new BrandDTO()
          {
            Id = brand.Id, // This prop cannot be changed
            Name = editedBrand.Name
          });

          brandToEdit = editedBrand;
          operationFinished = operationResult.IsSuccess;

          if (operationResult.IsSuccess)
          {
            RefreshCollectionFromServer(collection);
            SendMessage("Brand modification was successful");
          }
          else
          {
            SendMessage(operationResult.Messages.ToArray());
          }
        }
        else
        {
          SendMessage("Brand modification has cancelled");
          operationFinished = true;
        }
      } while (!operationFinished);
    }

    public void DeleteBrand(IList<BrandModel> collection, BrandModel brand)
    {
      string messageBoxText = "This will delete all cars with this brand!";
      string caption = "WARNING!";
      MessageBoxButton button = MessageBoxButton.YesNoCancel;
      MessageBoxImage icon = MessageBoxImage.Warning;
      MessageBoxResult result;
      result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

      if (result == MessageBoxResult.Yes) {
        if (brand != null)
        {
          var operationResult = httpService.Delete(brand.Id);

          if (operationResult.IsSuccess)
          {
            RefreshCollectionFromServer(collection);
            SendMessage("Brand deletion was successful");
          }
          else
          {
            SendMessage(operationResult.Messages.ToArray());
          }
        }
        else
        {
          SendMessage("Brand deletion failed");
        }
      }
    }

    public IList<BrandModel> GetAll()
    {
      var brands = httpService.GetAll<Brand>();

      return brands.Select(x => new BrandModel(x.Id, x.Name)).ToList();
    }

    public void ViewBrand(BrandModel brand)
    {
      brandDisplayService.Display(brand);
    }
  }
}
