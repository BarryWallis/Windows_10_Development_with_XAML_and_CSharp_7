using System;

using App1.Helpers;
using App1.Models;

namespace App1.ViewModels
{
    public class MainViewModel : Observable
    {
        public Customer MyCustomer { get; set; }

        private RelayCommand updateNameCommand;
        public RelayCommand UpdateNameCommand
        {
            get
            {
                if (updateNameCommand is null)
                    updateNameCommand = new RelayCommand(UpdateName);
                return updateNameCommand;
            }

            set => updateNameCommand = value; 
        }


        public MainViewModel() => MyCustomer = new Customer() { FirstName = "Bob", LastName = "Smith" };

        private void UpdateName() => MyCustomer.FirstName = "Sue";
    }
}
