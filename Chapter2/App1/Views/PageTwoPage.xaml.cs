using System;
using System.Diagnostics;
using App1.Models;
using App1.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace App1.Views
{
    public sealed partial class PageTwoPage : Page
    {
        public PageTwoViewModel ViewModel { get; } = new PageTwoViewModel();

        public PageTwoPage()
        {
#pragma warning disable IDE0021 // Use expression body for constructors
            InitializeComponent();
#pragma warning restore IDE0021 // Use expression body for constructors
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = e.Parameter as Customer;
            Debug.Assert(DataContext != null);
        }
    }
}
