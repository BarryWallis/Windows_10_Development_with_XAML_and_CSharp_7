using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;

namespace UserSpecifiedLocationsSample.Models
{
    class FileOperations
    {
        private static string mruToken;
        private static string tokenKey = nameof(mruToken);

        public static ApplicationDataContainer Settings => ApplicationData.Current.LocalSettings;

        /// <summary>
        /// Open the file with the given <paramref name="fileName"/>.
        /// </summary>
        /// <param name="fileName">The name of the file to open.</param>
        /// <returns>A StorageFile referring to the given <paramref name="fileName"/>.</returns>
        public static async Task<StorageFile> OpenFileAsync(string fileName)
        {
            mruToken = Settings.Values[tokenKey]?.ToString();
            if (mruToken != null)
                return await GetFileFromMruAsync();
            StorageFile storageFile = await GetFileAsync();
            return storageFile is null ? await CreateFile() : storageFile;
        }

        /// <summary>
        /// Create a new StorageFile selected by the user. 
        /// </summary>
        /// <returns>A StorageFile that was created to represent the saved file.</returns>
        private static async Task<StorageFile> CreateFile()
        {
            FileSavePicker fileSavePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            fileSavePicker.FileTypeChoices.Clear();
            fileSavePicker.FileTypeChoices.Add("JSON", new List<string>() { ".json" });
            return SaveMRU(await fileSavePicker.PickSaveFileAsync());
        }

        /// <summary>
        /// Add the <paramref name="storageFile"/> to the most recently used file list. 
        /// </summary>
        /// <param name="storageFile">The StorageFile to add to the most recently used file list.</param>
        /// <returns>The StorageFile that was passed in.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="storageFile"/> cannot be null.</exception>
        private static StorageFile SaveMRU(StorageFile storageFile)
        {
            if (storageFile != null)
            {
                mruToken = StorageApplicationPermissions.MostRecentlyUsedList.Add(storageFile);
                Settings.Values[tokenKey] = mruToken;
            }
            return storageFile;
        }

        /// <summary>
        /// Get a StorageFile selected by the user.
        /// </summary>
        /// <returns>The selected StorageFile.</returns>
        private static async Task<StorageFile> GetFileAsync()
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker() { SuggestedStartLocation = PickerLocationId.DocumentsLibrary, ViewMode = PickerViewMode.List };
            fileOpenPicker.FileTypeFilter.Clear();
            fileOpenPicker.FileTypeFilter.Add(".json");
            StorageFile storageFile = await fileOpenPicker.PickSingleFileAsync();
            return SaveMRU(storageFile);
        }

        /// <summary>
        /// Get StorageFilee specified by mruToken from the most recently used file list.
        /// </summary>
        /// <returns>The StorageFile sepcified by mruToken</returns>
        private static async Task<StorageFile> GetFileFromMruAsync() => await StorageApplicationPermissions.MostRecentlyUsedList.GetFileAsync(mruToken);
    }
}
