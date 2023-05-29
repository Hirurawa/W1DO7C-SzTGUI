using CarShop.WpfClient.Models;
using CarShop.WpfClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
