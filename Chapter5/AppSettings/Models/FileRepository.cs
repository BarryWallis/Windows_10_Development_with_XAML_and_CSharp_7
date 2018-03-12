using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;

namespace AppSettings.Models
{
    public class FileRepository : IDataRepository<Customer>
    {
        private string fileName = "customers.json";
        private ObservableCollection<Customer> customers;
        private StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

        public FileRepository()
        {
            //Initialize();
        }

        private Task WriteToFile() => Task.Run(async () =>
        {
            string json = JsonConvert.SerializeObject(customers);
            StorageFile file = await OpenFileAsync();
            await FileIO.WriteTextAsync(file, json);
        });

        private async Task<StorageFile> OpenFileAsync() => await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

        private void Initialize() => throw new NotImplementedException();

        public Task Add(Customer customer)
        {
            customers.Add(customer);
            return WriteToFile();
        }

        public async Task<ObservableCollection<Customer>> Load()
        {
            StorageFile storageFile = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            string fileContents = string.Empty;
            if (storageFile != null)
            {
                fileContents = await FileIO.ReadTextAsync(storageFile);
            }

            IList<Customer> customersFromJson = JsonConvert.DeserializeObject<List<Customer>>(fileContents) ?? new List<Customer>();
            customers = new ObservableCollection<Customer>(customersFromJson);
            return customers;
        }

        public Task Remove(Customer customer)
        {
            customers.Remove(customer);
            return WriteToFile();
        }

        public Task Update(Customer customer)
        {
            Customer oldCustomer = customers.FirstOrDefault(c => c.Id == customer.Id);
            if (oldCustomer is null)
            {
                throw new ArgumentException("Customer not found");
            }

            customers.Remove(oldCustomer);
            customers.Add(customer);
            return WriteToFile();
        }
    }
}
