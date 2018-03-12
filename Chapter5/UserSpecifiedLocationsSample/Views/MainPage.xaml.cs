using System;
using UserSpecifiedLocationsSample.Models;
using UserSpecifiedLocationsSample.ViewModels;

using Windows.UI.Xaml.Controls;

namespace UserSpecifiedLocationsSample.Views
{
    public sealed partial class MainPage : Page
    {
        private IDataRepository<Customer> customerRepository = null;

        public MainViewModel ViewModel { get; }

        public MainPage()
        {
            InitializeComponent();
            customerRepository = new FileRepository();
            ViewModel = new MainViewModel(customerRepository);
            ViewModel.Initialize();
            DataContext = ViewModel;
        }

        private void CmdSave_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) =>
            ViewModel.AddCustomer(new Customer() { Email = Email.Text, FirstName = FirstName.Text, LastName = LastName.Text, Title = Title.Text });

        private void CmdDelete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) => ViewModel?.DeleteCustomer(ViewModel.SelectedItem);
    }
}
