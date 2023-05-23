using CarShop.WpfClient.BL.Interfaces;
using CarShop.WpfClient.Models;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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

        public ObservableCollection<CarModel> Cars { get; private set; }

        public ICommand AddCommand { get; private set; }
        public ICommand ModifyCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand ViewCommand { get; private set; }

        public ICommand LoadCommand { get; private set; }

        readonly ICarHandlerService carHandlerService;

        public MainWindowVM(ICarHandlerService carHandlerService)
        {
            this.carHandlerService = carHandlerService;
            Cars = new ObservableCollection<CarModel>();

            if (IsInDesignMode)
            {
                Cars.Add(new CarModel(1, 1200, "3", 1));
                Cars.Add(new CarModel(2, 2200, "MX8", 1));
                var astra = new CarModel(3, 1400, "Astra G", 2);
                Cars.Add(astra);
                Cars.Add(new CarModel(4, 3000, "A4", 3));
                CurrentCar = astra;
            }

            LoadCommand = new RelayCommand(() =>
            {
                var cars = this.carHandlerService.GetAll();
                Cars.Clear();

                foreach (var car in cars)
                {
                    Cars.Add(car);
                }
            });

            AddCommand = new RelayCommand(() => this.carHandlerService.AddCar(Cars));
            ModifyCommand = new RelayCommand(() => this.carHandlerService.ModifyCar(Cars, CurrentCar));
            DeleteCommand = new RelayCommand(() => this.carHandlerService.DeleteCar(Cars, CurrentCar));
            ViewCommand = new RelayCommand(() => this.carHandlerService.ViewCar(CurrentCar));
        }

        public MainWindowVM() : this(IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<ICarHandlerService>())
        {
        }
    }
}
