using CarShop.WpfClient.BL.Implementation;
using CarShop.WpfClient.BL.Interfaces;
using CarShop.WpfClient.Infrastructure;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace CarShop.WpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIocAsServiceLocator.Instance);

            SimpleIocAsServiceLocator.Instance.Register<ICarEditorService, CarEditorViaWindowService>();
            SimpleIocAsServiceLocator.Instance.Register<ICarDisplayService, CarDisplayService>();
            SimpleIocAsServiceLocator.Instance.Register<ICarHandlerService, CarHandlerService>();
            SimpleIocAsServiceLocator.Instance.Register(() => Messenger.Default);
        }
    }
}
