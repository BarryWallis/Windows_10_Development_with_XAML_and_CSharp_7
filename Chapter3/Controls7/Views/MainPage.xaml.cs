using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Windows.UI.Xaml.Controls;

namespace Controls7.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public MainPage()
        {
            InitializeComponent();
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

        private void ToggleButton_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) => Message.Text = "Button was toggled on!";

        private void ToggleButton_Unchecked(object sender, Windows.UI.Xaml.RoutedEventArgs e) => Message.Text = "Button was toggled off!";
    }
}
