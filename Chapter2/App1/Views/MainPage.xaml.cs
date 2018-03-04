using System;
using App1.Models;
using App1.ViewModels;

using Windows.UI.Xaml.Controls;

namespace App1.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void NavigateBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageTwoPage), ((this.DataContext as MainViewModel) != null)
                ? (this.DataContext as MainViewModel).MyCustomer
                : (new Customer() { FirstName = "Jane", LastName = "Doe" }));
        }
    }
}
