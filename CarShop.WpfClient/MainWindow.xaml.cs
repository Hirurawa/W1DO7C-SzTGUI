using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;

namespace CarShop.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<string[]>(this, "BlOperationResult",
                (msgs) => MessageBox.Show(String.Join(Environment.NewLine, msgs), "BL result", MessageBoxButton.OK, MessageBoxImage.Information));
        }

        private void WindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Messenger.Default.Unregister(this);
        }
    }
}
