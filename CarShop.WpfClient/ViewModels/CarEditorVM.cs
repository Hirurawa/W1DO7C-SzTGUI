using CarShop.WpfClient.BL.Interfaces;
using CarShop.WpfClient.Models;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Linq;

namespace CarShop.WpfClient.ViewModels
{
    public class CarEditorVM : ViewModelBase
    {
        private CarModel currentCar;

        public CarModel CurrentCar
        {
            get { return currentCar; }
            set 
            {
                Set(ref currentCar, value);
                SelectedBrand = AvailableBrands?.SingleOrDefault(x => x.Id == currentCar.BrandId);
            }
        }

        private BrandModel brandModel;

        public BrandModel SelectedBrand
        {
            get { return brandModel; }
            set {
                Set(ref brandModel, value);
                currentCar.BrandId = brandModel?.Id ?? 0;
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

        public CarEditorVM(IBrandHandlerService brandHandlerService)
        {
            CurrentCar = new CarModel();

            if (IsInDesignModeStatic)
            {
                AvailableBrands = new List<BrandModel>()
                {
                    new BrandModel(1, "Mazda"),
                    new BrandModel(2, "Opel"),
                    new BrandModel(3, "BMW"),
                };

                SelectedBrand = AvailableBrands[1];
                CurrentCar.Model = "Astra G";
                CurrentCar.Price = 1750;
            }
            else
            {
                AvailableBrands = brandHandlerService.GetAll();
        SelectedBrand = AvailableBrands[1];
      }
        }

        public CarEditorVM() : this(IsInDesignModeStatic ? null: ServiceLocator.Current.GetInstance<IBrandHandlerService>())
        {
        }
    }
}
