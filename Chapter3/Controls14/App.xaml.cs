using System;

using Controls14.Services;

using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace Controls14
{
    public sealed partial class App : Application
    {
        private Lazy<ActivationService> _activationService;

        private ActivationService ActivationService
        {
#pragma warning disable IDE0025 // Use expression body for properties
            get { return _activationService.Value; }
#pragma warning restore IDE0025 // Use expression body for properties
        }

        public App()
        {
            InitializeComponent();

            // Deferred execution until used. Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            await ActivationService.ActivateAsync(args);
#pragma warning restore IDE0022 // Use expression body for methods
        }

        private ActivationService CreateActivationService()
        {
#pragma warning disable IDE0022 // Use expression body for methods
            return new ActivationService(this, typeof(Views.MainPage));
#pragma warning restore IDE0022 // Use expression body for methods
        }
    }
}
