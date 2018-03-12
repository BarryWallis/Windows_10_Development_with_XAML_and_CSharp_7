using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AppSettings.Models;

namespace AppSettings.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private IDataRepository<Customer> dataRepository;

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
                    NotifyPropertyChanged();
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
                customers = value;
                NotifyPropertyChanged();
            }
        }

        public ViewModel(IDataRepository<Customer> data) => dataRepository = data;

        public async void Initialize() => await dataRepository.Load();

        public void AddCustomer(Customer customer) => dataRepository.Add(customer);

        public void DeleteCustomer(Customer customer) => dataRepository.Remove(customer);

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string fieldName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(fieldName));
    }
}
