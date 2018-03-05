using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Binding5.Models;
using Windows.UI.Xaml.Controls;

namespace Binding5.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public Person person = new Person() { FirstName = "Barry", LastName = "Wallis", City = "Oceanside" };

        private Random random = new Random();

        public MainPage()
        {
            InitializeComponent();
            DataContext = person;
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

        private void XButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) => person.Num = random.Next(2);
    }
}
