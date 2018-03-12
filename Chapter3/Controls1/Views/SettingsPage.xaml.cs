using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Controls1.Services;

using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Controls1.Views
{
    // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/pages/settings-codebehind.md
    // TODO WTS: Change the URL for your privacy policy in the Resource File, currently set to https://YourPrivacyUrlGoesHere
    public sealed partial class SettingsPage : Page, INotifyPropertyChanged
    {
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;

        public ElementTheme ElementTheme
        {
#pragma warning disable IDE0027 // Use expression body for accessors
            get { return _elementTheme; }
#pragma warning restore IDE0027 // Use expression body for accessors

#pragma warning disable IDE0027 // Use expression body for accessors
            set { Set(ref _elementTheme, value); }
#pragma warning restore IDE0027 // Use expression body for accessors
        }

        private string _versionDescription;

        public string VersionDescription
        {
#pragma warning disable IDE0027 // Use expression body for accessors
            get { return _versionDescription; }
#pragma warning restore IDE0027 // Use expression body for accessors

#pragma warning disable IDE0027 // Use expression body for accessors
            set { Set(ref _versionDescription, value); }
#pragma warning restore IDE0027 // Use expression body for accessors
        }

        public SettingsPage()
        {
#pragma warning disable IDE0021 // Use expression body for constructors
            InitializeComponent();
#pragma warning restore IDE0021 // Use expression body for constructors
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            Initialize();
#pragma warning restore IDE0022 // Use expression body for methods
        }

        private void Initialize()
        {
#pragma warning disable IDE0022 // Use expression body for methods
            VersionDescription = GetVersionDescription();
#pragma warning restore IDE0022 // Use expression body for methods
        }

        private string GetVersionDescription()
        {
#pragma warning disable IDE0008 // Use explicit type
            var package = Package.Current;
#pragma warning restore IDE0008 // Use explicit type
#pragma warning disable IDE0008 // Use explicit type
            var packageId = package.Id;
#pragma warning restore IDE0008 // Use explicit type
#pragma warning disable IDE0008 // Use explicit type
            var version = packageId.Version;
#pragma warning restore IDE0008 // Use explicit type

            return $"{package.DisplayName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        private async void ThemeChanged_CheckedAsync(object sender, RoutedEventArgs e)
        {
#pragma warning disable IDE0008 // Use explicit type
            var param = (sender as RadioButton)?.CommandParameter;
#pragma warning restore IDE0008 // Use explicit type

            if (param != null)
            {
                await ThemeSelectorService.SetThemeAsync((ElementTheme)param);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
