using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Binding1.Models;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Binding1.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private Person person = new Person() { FirstName = "Jon", /*LastName = "Hartwell"*/ };

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

        protected override void OnNavigatedTo(NavigationEventArgs e) => DataContext = person;
    }
}
