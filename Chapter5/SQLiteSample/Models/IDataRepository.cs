using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteSample.Models
{
    public interface IDataRepository<T>
    {
        /// <summary>
        /// Add a data item to the repository.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>Nothing.</returns>
        Task Add(T item);

        /// <summary>
        /// Load the data repository.
        /// </summary>
        /// <returns>An ObservableCollection<typeparamref name="T"/></returns>
        Task<ObservableCollection<T>> LoadAsync();

        /// <summary>
        /// Remove an item from the repository. 
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>Nothimg/</returns>
        Task Remove(T item);

        /// <summary>
        /// Update the item in the repository.
        /// </summary>
        /// <param name="item">The item to update.</param>
        /// <returns>Nothing.</returns>
        Task Update(T item);
    }
}
