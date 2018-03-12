using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LocalFolderSample.Helpers;
using LocalFolderSample.Models;

namespace LocalFolderSample.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IDataRepository<Customer> customerRepository;

        private Customer selectedItem;
        public Customer SelectedItem
        {
#pragma warning disable IDE0027 // Use expression body for accessors
            get { return selectedItem; }
#pragma warning restore IDE0027 // Use expression body for accessors
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Customer> customers;
        public ObservableCollection<Customer> Customers
        {
#pragma warning disable IDE0027 // Use expression body for accessors
            get { return customers; }
#pragma warning restore IDE0027 // Use expression body for accessors
            set
            {
                if (customers != value)
                {
                    customers = value;
                    OnPropertyChanged();
                }
            }
        }


        public MainViewModel() => throw new NotImplementedException();

        public MainViewModel(IDataRepository<Customer> customerRepository) => 
            this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));

        /// <summary>
        /// Initialize the view model with the customer repository.
        /// </summary>
        public async void Initialize() => Customers = await customerRepository.LoadAsync();

        /// <summary>
        /// Add the customer to the customer repository.
        /// </summary>
        /// <param name="customer">The customer to add.</param>
        internal void AddCustomer(Customer customer) => customerRepository.Add(customer);

        /// <summary>
        /// Delete the customer from the repository.
        /// </summary>
        /// <param name="customer">The customer to delete.</param>
        internal void DeleteCustomer(Customer customer) => customerRepository.Remove(customer);

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise the PropertyChanged event.
        /// </summary>
        /// <param name="fieldName">The field whose property has changed.</param>
        private void OnPropertyChanged([CallerMemberName] string fieldName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(fieldName));
    }
}
