using System;
using System.Runtime.InteropServices;

using Windows.ApplicationModel.Resources;

namespace SQLiteSample.Helpers
{
    internal static class ResourceExtensions
    {
        private static ResourceLoader _resLoader = new ResourceLoader();

        public static string GetLocalized(this string resourceKey)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            return _resLoader.GetString(resourceKey);
#pragma warning restore IDE0022 // Use expression body for methods
        }
    }
}
