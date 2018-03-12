using System;
using System.Runtime.InteropServices;

using Windows.ApplicationModel.Resources;

namespace UserSpecifiedLocationsSample.Helpers
{
    internal static class ResourceExtensions
    {
        private static ResourceLoader _resLoader = new ResourceLoader();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static string GetLocalized(this string resourceKey)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            return _resLoader.GetString(resourceKey);
#pragma warning restore IDE0022 // Use expression body for methods
        }
    }
}
