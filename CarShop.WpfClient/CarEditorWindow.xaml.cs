using CarShop.WpfClient.Models;
using CarShop.WpfClient.ViewModels;
using System.Windows;

namespace CarShop.WpfClient
{
  /// <summary>
  /// Interaction logic for CarEditorWindow.xaml
  /// </summary>
  public partial class CarEditorWindow : Window
  {
    public CarModel Car { get; set; }
    bool enableEdit;

    public CarEditorWindow(CarModel car = null, bool enableEdit = true)
    {
      InitializeComponent();
      Car = car == null
          ? new CarModel()
          : new CarModel(car);

      this.enableEdit = enableEdit;
    }

    private void OkClick(object sender, RoutedEventArgs e)
    {
      if (enableEdit)
      {
        DialogResult = true;
      }
      else
      {
        Close();
      }
    }

    private void CancelClick(object sender, RoutedEventArgs e)
    {
      if (enableEdit)
      {
        DialogResult = false;
      }
      else
      {
        Close();
      }
    }

    private void WindowLoaded(object sender, RoutedEventArgs e)
    {
      var vm = (CarEditorVM)Resources["VM"];
      vm.CurrentCar = Car;
      vm.EditEnabled = enableEdit;
    }
  }
}
