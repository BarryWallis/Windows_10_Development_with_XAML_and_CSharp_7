using System;
using System.Threading.Tasks;

using Controls1.Helpers;

using Windows.Storage;
using Windows.UI.Xaml;

namespace Controls1.Services
{
    public static class ThemeSelectorService
    {
        private const string SettingsKey = "AppBackgroundRequestedTheme";

        public static ElementTheme Theme { get; set; } = ElementTheme.Default;

        public static async Task InitializeAsync()
        {
#pragma warning disable IDE0022 // Use expression body for methods
            Theme = await LoadThemeFromSettingsAsync();
#pragma warning restore IDE0022 // Use expression body for methods
        }

        public static async Task SetThemeAsync(ElementTheme theme)
        {
            Theme = theme;

            SetRequestedTheme();
            await SaveThemeInSettingsAsync(Theme);
        }

        public static void SetRequestedTheme()
        {
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                frameworkElement.RequestedTheme = Theme;
            }
        }

        private static async Task<ElementTheme> LoadThemeFromSettingsAsync()
        {
            ElementTheme cacheTheme = ElementTheme.Default;
            string themeName = await ApplicationData.Current.LocalSettings.ReadAsync<string>(SettingsKey);

            if (!string.IsNullOrEmpty(themeName))
            {
                Enum.TryParse(themeName, out cacheTheme);
            }

            return cacheTheme;
        }

        private static async Task SaveThemeInSettingsAsync(ElementTheme theme)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            await ApplicationData.Current.LocalSettings.SaveAsync(SettingsKey, theme.ToString());
#pragma warning restore IDE0022 // Use expression body for methods
        }
    }
}
