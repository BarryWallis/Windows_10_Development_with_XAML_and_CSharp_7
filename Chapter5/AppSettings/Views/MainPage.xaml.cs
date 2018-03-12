using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AppSettings.Models;
using AppSettings.ViewModels;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AppSettings.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private IDataRepository<Customer> customers;
        private ViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            customers = new FileRepository();
            viewModel = new ViewModel(customers);
            viewModel.Initialize();
            DataContext = viewModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void CmdSave_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer()
            {
                Email = Email.Text,
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                Title = Title.Text
            };

            viewModel.AddCustomer(customer);
        }

        private void CmdDelete_Click(object sender, RoutedEventArgs e)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            viewModel?.DeleteCustomer(viewModel.SelectedItem);
#pragma warning restore IDE0022 // Use expression body for methods
        }
    }
}
