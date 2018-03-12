using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;

namespace SQLiteSample.Models
{
    class SqliteRepository : IDataRepository<Customer>
    {
        private static readonly string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "app.SQLite");

        public SqliteRepository() => InitializeAsync();

        private async Task InitializeAsync()
        {
            using (CustomerContext db = new CustomerContext())
            {
                if (db.Customers.Any())
                {
                    await LoadAsync();
                }
                else
                {
                    db.Database.Migrate();
                    db.Customers.Add(new Customer
                    {
                        FirstName = "Phil",
                        LastName = "Japikse"
                    });
                    db.Customers.Add(new Customer()
                    {
                        FirstName = "Jon",
                        LastName = "Galloway"
                    });
                    db.Customers.Add(new Customer()
                    {
                        FirstName = "Jesse",
                        LastName = "Liberty"
                    });
                    db.Customers.Add(new Customer()
                    {
                        FirstName = "Jon",
                        LastName = "Hartwell"
                    });
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Add a data item to the repository.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>Nothing.</returns>
        public Task Add(Customer item)
        {
            using (CustomerContext db = new CustomerContext())
            {
                db.Customers.Add(item);
                return db.SaveChangesAsync(true);
            }
        }

        /// <summary>
        /// Load the data repository.
        /// </summary>
        /// <returns>An ObservableCollection<typeparamref name="T"/></returns>
        public async Task<ObservableCollection<Customer>> LoadAsync()
        {
            using (CustomerContext db = new CustomerContext())
            {
                return new ObservableCollection<Customer>(await db.Customers.ToListAsync());
            }
        }

        /// <summary>
        /// Remove an item from the repository. 
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>Nothimg/</returns>
        public Task Remove(Customer item)
        {
            using (CustomerContext db = new CustomerContext())
            {
                db.Customers.Remove(item);
                return db.SaveChangesAsync(true);
            }
        }

        /// <summary>
        /// Update the item in the repository.
        /// </summary>
        /// <param name="item">The item to update.</param>
        /// <returns>Nothing.</returns>
        public Task Update(Customer item)
        {
            using (CustomerContext db = new CustomerContext())
            {
                Customer existingCustomer = db.Customers.Find(item);
                existingCustomer = item;
                db.Customers.Update(existingCustomer);
                return db.SaveChangesAsync();   
            }
        }
    }
}
