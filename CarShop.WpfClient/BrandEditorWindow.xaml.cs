using CarShop.WpfClient.Models;
using CarShop.WpfClient.ViewModels;
using System.Windows;

namespace CarShop.WpfClient
{
  /// <summary>
  /// Interaction logic for BrandEditorWindow.xaml
  /// </summary>
  public partial class BrandEditorWindow : Window
  {
    public BrandModel Brand { get; set; }
    bool enableEdit;

    public BrandEditorWindow(BrandModel brand = null, bool enableEdit = true)
    {
      InitializeComponent();
      Brand = brand == null
          ? new BrandModel()
          : new BrandModel(brand);

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
      var vm = (BrandEditorVM)Resources["VM"];
      vm.CurrentBrand = Brand;
      vm.EditEnabled = enableEdit;
    }
  }
}
