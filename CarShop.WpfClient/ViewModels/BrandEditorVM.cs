using CarShop.WpfClient.BL.Interfaces;
using CarShop.WpfClient.Models;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Linq;

namespace CarShop.WpfClient.ViewModels
{
  public class BrandEditorVM : ViewModelBase
  {
    private BrandModel currentBrand;

    public BrandModel CurrentBrand
    {
      get { return currentBrand; }
      set
      {
        Set(ref currentBrand, value);
        SelectedBrand = AvailableBrands?.SingleOrDefault(x => x.Id == currentBrand.Id);
      }
    }

    private BrandModel brandModel;

    public BrandModel SelectedBrand
    {
      get { return brandModel; }
      set
      {
        Set(ref brandModel, value);
        currentBrand.Id = brandModel?.Id ?? 0;
      }
    }

    public IList<BrandModel> AvailableBrands { get; private set; }

    private bool editEnabled;

    public bool EditEnabled
    {
      get { return editEnabled; }
      set
      {
        Set(ref editEnabled, value);
        RaisePropertyChanged(nameof(CancelButtonVisibility));
      }
    }

    public System.Windows.Visibility CancelButtonVisibility => EditEnabled ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;

    public BrandEditorVM(IBrandHandlerService brandHandlerService)
    {
      CurrentBrand = new BrandModel();

      if (IsInDesignModeStatic)
      {
        AvailableBrands = new List<BrandModel>()
                {
                    new BrandModel(1, "Mazda"),
                    new BrandModel(2, "Opel"),
                    new BrandModel(3, "BMW"),
                };

        SelectedBrand = AvailableBrands[1];
      }
      else
      {
        AvailableBrands = brandHandlerService.GetAll();
      }
    }

    public BrandEditorVM() : this(IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<IBrandHandlerService>())
    {
    }
  }
}
