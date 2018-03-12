using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;

namespace LocalFolderSample.Models
{
    public class FileRepository : IDataRepository<Customer>
    {
        private string fileName = "customers.json";
        private ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
        private StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

        public FileRepository()
        {
#pragma warning disable IDE0021 // Use expression body for constructors
            Initialize();
#pragma warning restore IDE0021 // Use expression body for constructors
        }

        /// <summary>
        /// Initialize the file repository. 
        /// </summary>
        private void Initialize()
        {

        }

        /// <summary>
        /// Serialize the collection of customers. 
        /// </summary>
        /// <returns>An asynchronous task.</returns>
        private Task WriteToFile()
        {
#pragma warning disable IDE0022 // Use expression body for methods
            return Task.Run(async () =>
            {
                string json = JsonConvert.SerializeObject(customers);
                StorageFile storageFile = await OpenFileAsync();
                await FileIO.WriteTextAsync(storageFile, json);
            });
#pragma warning restore IDE0022 // Use expression body for methods
        }

        /// <summary>
        /// Open the output file asynchronously.
        /// </summary>
        /// <returns>The storage file.</returns>
        private async Task<StorageFile> OpenFileAsync() => await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

        /// <summary>
        /// Add a customer to the repository.
        /// </summary>
        /// <param name="customer">The customer to add.</param>
        /// <returns>An asynchronous task..</returns>
        public Task Add(Customer customer)
        {
            customers.Add(customer);
            return WriteToFile();
        }

        /// <summary>
        /// Load the data repository.
        /// </summary>
        /// <returns>An ObservableCollection<typeparamref name="T"/></returns>
        async public Task<ObservableCollection<Customer>> LoadAsync()
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

        /// <summary>
        /// Remove a customer from the repository. 
        /// </summary>
        /// <param name="customer">The customer to remove.</param>
        /// <returns>An asynchronous task.</returns>
        public Task Remove(Customer customer)
        {
            customers.Remove(customer);
            return WriteToFile();
        }

        /// <summary>
        /// Update the customer in the repository.
        /// </summary>
        /// <param name="customer">The customer to update.</param>
        /// <returns>An asynchronous task.</returns>
        public Task Update(Customer customer)
        {
            Customer oldCustomer = customers.FirstOrDefault(c => c.Id == customer.Id);
            if (oldCustomer is null)
            {
                throw new ArgumentException($"Customer {customer.Id} not found");
            }

            customers.Remove(oldCustomer);
            customers.Add(customer);
            return WriteToFile();
        }
    }
}
