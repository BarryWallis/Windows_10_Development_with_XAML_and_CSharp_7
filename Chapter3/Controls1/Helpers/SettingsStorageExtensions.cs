using System;
using System.IO;
using System.Threading.Tasks;

using Windows.Storage;
using Windows.Storage.Streams;

namespace Controls1.Helpers
{
    // Use these extension methods to store and retrieve local and roaming app data
    // More details regarding storing and retrieving app data at https://docs.microsoft.com/windows/uwp/app-settings/store-and-retrieve-app-data
    public static class SettingsStorageExtensions
    {
        private const string FileExtension = ".json";

        public static bool IsRoamingStorageAvailable(this ApplicationData appData)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            return appData.RoamingStorageQuota == 0;
#pragma warning restore IDE0022 // Use expression body for methods
        }

        public static async Task SaveAsync<T>(this StorageFolder folder, string name, T content)
        {
#pragma warning disable IDE0008 // Use explicit type
            var file = await folder.CreateFileAsync(GetFileName(name), CreationCollisionOption.ReplaceExisting);
#pragma warning restore IDE0008 // Use explicit type
#pragma warning disable IDE0008 // Use explicit type
            var fileContent = await Json.StringifyAsync(content);
#pragma warning restore IDE0008 // Use explicit type

            await FileIO.WriteTextAsync(file, fileContent);
        }

        public static async Task<T> ReadAsync<T>(this StorageFolder folder, string name)
        {
            if (!File.Exists(Path.Combine(folder.Path, GetFileName(name))))
            {
                return default(T);
            }

#pragma warning disable IDE0008 // Use explicit type
            var file = await folder.GetFileAsync($"{name}.json");
#pragma warning restore IDE0008 // Use explicit type
#pragma warning disable IDE0008 // Use explicit type
            var fileContent = await FileIO.ReadTextAsync(file);
#pragma warning restore IDE0008 // Use explicit type

            return await Json.ToObjectAsync<T>(fileContent);
        }

        public static async Task SaveAsync<T>(this ApplicationDataContainer settings, string key, T value)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            settings.SaveString(key, await Json.StringifyAsync(value));
#pragma warning restore IDE0022 // Use expression body for methods
        }

        public static void SaveString(this ApplicationDataContainer settings, string key, string value)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            settings.Values[key] = value;
#pragma warning restore IDE0022 // Use expression body for methods
        }

        public static async Task<T> ReadAsync<T>(this ApplicationDataContainer settings, string key)
        {
#pragma warning disable IDE0018 // Inline variable declaration
            object obj = null;
#pragma warning restore IDE0018 // Inline variable declaration

            if (settings.Values.TryGetValue(key, out obj))
            {
                return await Json.ToObjectAsync<T>((string)obj);
            }

            return default(T);
        }

        public static async Task<StorageFile> SaveFileAsync(this StorageFolder folder, byte[] content, string fileName, CreationCollisionOption options = CreationCollisionOption.ReplaceExisting)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("ExceptionSettingsStorageExtensionsFileNameIsNullOrEmpty".GetLocalized(), nameof(fileName));
            }

#pragma warning disable IDE0008 // Use explicit type
            var storageFile = await folder.CreateFileAsync(fileName, options);
#pragma warning restore IDE0008 // Use explicit type
            await FileIO.WriteBytesAsync(storageFile, content);
            return storageFile;
        }

        public static async Task<byte[]> ReadFileAsync(this StorageFolder folder, string fileName)
        {
#pragma warning disable IDE0008 // Use explicit type
            var item = await folder.TryGetItemAsync(fileName).AsTask().ConfigureAwait(false);
#pragma warning restore IDE0008 // Use explicit type

            if ((item != null) && item.IsOfType(StorageItemTypes.File))
            {
#pragma warning disable IDE0008 // Use explicit type
                var storageFile = await folder.GetFileAsync(fileName);
#pragma warning restore IDE0008 // Use explicit type
                byte[] content = await storageFile.ReadBytesAsync();
                return content;
            }

            return null;
        }

        public static async Task<byte[]> ReadBytesAsync(this StorageFile file)
        {
            if (file != null)
            {
                using (IRandomAccessStream stream = await file.OpenReadAsync())
                {
#pragma warning disable IDE0008 // Use explicit type
                    using (var reader = new DataReader(stream.GetInputStreamAt(0)))
#pragma warning restore IDE0008 // Use explicit type
                    {
                        await reader.LoadAsync((uint)stream.Size);
#pragma warning disable IDE0008 // Use explicit type
                        var bytes = new byte[stream.Size];
#pragma warning restore IDE0008 // Use explicit type
                        reader.ReadBytes(bytes);
                        return bytes;
                    }
                }
            }

            return null;
        }

        private static string GetFileName(string name)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            return string.Concat(name, FileExtension);
#pragma warning restore IDE0022 // Use expression body for methods
        }
    }
}
