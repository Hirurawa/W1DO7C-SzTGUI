using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace CarShop.WpfClient.Infrastructure
{
  public class SimpleIocAsServiceLocator : SimpleIoc, IServiceLocator
  {
    public static SimpleIocAsServiceLocator Instance { get; private set; } = new SimpleIocAsServiceLocator();
  }
}
