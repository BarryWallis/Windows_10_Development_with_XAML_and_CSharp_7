using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;

namespace UserSpecifiedLocationsSample.Models
{
    public class FileRepository : IDataRepository<Customer>
    {
        private string fileName = "customers.json";
        private ObservableCollection<Customer> customers = new ObservableCollection<Customer>();

        public FileRepository() => Initialize();

        /// <summary>
        /// Initialize the file repository. 
        /// </summary>
        private static void Initialize()
        {

        }

        /// <summary>
        /// Serialize the collection of customers. 
        /// </summary>
        /// <returns>An asynchronous task.</returns>
        private Task WriteToFile() => Task.Run(async () =>
                                                {
                                                    string json = JsonConvert.SerializeObject(customers);
                                                    StorageFile storageFile = await OpenFileAsync();
                                                    await FileIO.WriteTextAsync(storageFile, json);
                                                });

        /// <summary>
        /// Open the output file asynchronously.
        /// </summary>
        /// <returns>The storage file.</returns>
        private async Task<StorageFile> OpenFileAsync() => await FileOperations.OpenFileAsync(fileName);

        /// <summary>
        /// Add a customer to the repository.
        /// </summary>
        /// <param name="item">The customer to add.</param>
        /// <returns>An asynchronous task..</returns>
        public Task Add(Customer item)
        {
            customers.Add(item);
            return WriteToFile();
        }

        /// <summary>
        /// Load the data repository.
        /// </summary>
        /// <returns>An ObservableCollection<typeparamref name="T"/></returns>
        public async Task<ObservableCollection<Customer>> LoadAsync()
        {
            StorageFile storageFile = await FileOperations.OpenFileAsync(fileName);
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
        /// <param name="item">The customer to remove.</param>
        /// <returns>An asynchronous task.</returns>
        public Task Remove(Customer item)
        {
            customers.Remove(item);
            return WriteToFile();
        }

        /// <summary>
        /// Update the customer in the repository.
        /// </summary>
        /// <param name="item">The customer to update.</param>
        /// <returns>An asynchronous task.</returns>
        public Task Update(Customer item)
        {
            Customer oldCustomer = customers.FirstOrDefault(c => c.Id == item.Id);
            if (oldCustomer is null)
            {
                throw new ArgumentException($"Customer {item.Id} not found");
            }

            customers.Remove(oldCustomer);
            customers.Add(item);
            return WriteToFile();
        }
    }
}
