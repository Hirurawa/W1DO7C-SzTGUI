using CarShop.WpfClient.BL.Interfaces;
using CarShop.WpfClient.Models;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CarShop.WpfClient.ViewModels
{
  public class MainWindowVM : ViewModelBase
  {

    private CarModel currentCar;

    public CarModel CurrentCar
    {
      get { return currentCar; }
      set { Set(ref currentCar, value); }
    }

    private BrandModel currentBrand;

    public BrandModel CurrentBrand
    {
      get { return currentBrand; }
      set { Set(ref currentBrand, value); }
    }

    public ObservableCollection<CarModel> Cars { get; private set; }
    public ObservableCollection<BrandModel> Brands { get; private set; }
    public ObservableCollection<AverageModel> Averages { get; private set; }

    public ICommand AddCarCommand { get; private set; }
    public ICommand ModifyCarCommand { get; private set; }
    public ICommand DeleteCarCommand { get; private set; }
    public ICommand ViewCarCommand { get; private set; }
    public ICommand LoadCommand { get; private set; }

    public ICommand AddBrandCommand { get; private set; }
    public ICommand ModifyBrandCommand { get; private set; }
    public ICommand DeleteBrandCommand { get; private set; }
    public ICommand ViewBrandCommand { get; private set; }

    readonly ICarHandlerService carHandlerService;
    readonly IBrandHandlerService brandHandlerService;

    public MainWindowVM(ICarHandlerService carHandlerService, IBrandHandlerService brandHandlerService)
    {
      this.carHandlerService = carHandlerService;
      this.brandHandlerService = brandHandlerService;
      Cars = new ObservableCollection<CarModel>();
      Brands = new ObservableCollection<BrandModel>();
      Averages = new ObservableCollection<AverageModel>();

      if (IsInDesignMode)
      {
        Cars.Add(new CarModel(1, 1200, "3", 1));
        Cars.Add(new CarModel(2, 2200, "MX8", 1));
        var astra = new CarModel(3, 1400, "Astra G", 2);
        Cars.Add(astra);
        Cars.Add(new CarModel(4, 3000, "A4", 3));
        CurrentCar = astra;

        var mazda = new BrandModel(1, "Mazda");
        Brands.Add(mazda);
        Brands.Add(new BrandModel(2, "Opel"));
        Brands.Add(new BrandModel(3, "BMW"));
        CurrentBrand = mazda;
      }

      LoadCommand = new RelayCommand(() =>
      {
        var cars = this.carHandlerService.GetAll();
        Cars.Clear();

        foreach (var car in cars)
        {
          Cars.Add(car);
        }
        var brands = this.brandHandlerService.GetAll();
        Brands.Clear();

        foreach (var brand in brands)
        {
          Brands.Add(brand);
        }

        var avg = this.carHandlerService.GetBrandAverages();
        Averages.Clear();

        foreach (var average in avg)
        {
          Averages.Add(average);
        }
      });

      AddCarCommand = new RelayCommand(() => { this.carHandlerService.AddCar(Cars); LoadCommand.Execute(null); });
      ModifyCarCommand = new RelayCommand(() => { this.carHandlerService.ModifyCar(Cars, CurrentCar); LoadCommand.Execute(null); });
      DeleteCarCommand = new RelayCommand(() => { this.carHandlerService.DeleteCar(Cars, CurrentCar); LoadCommand.Execute(null); });
      ViewCarCommand = new RelayCommand(() => { this.carHandlerService.ViewCar(CurrentCar); });

      AddBrandCommand = new RelayCommand(() => { this.brandHandlerService.AddBrand(Brands); LoadCommand.Execute(null); });
      ModifyBrandCommand = new RelayCommand(() => { this.brandHandlerService.ModifyBrand(Brands, CurrentBrand); LoadCommand.Execute(null); });
      DeleteBrandCommand = new RelayCommand(() => { this.brandHandlerService.DeleteBrand(Brands, CurrentBrand); LoadCommand.Execute(null); });
      ViewBrandCommand = new RelayCommand(() => { this.brandHandlerService.ViewBrand(CurrentBrand); });
    }

    public MainWindowVM() : this(IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<ICarHandlerService>(), IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<IBrandHandlerService>())
    {
    }
  }
}
