using System;
using System.Threading.Tasks;

using Controls14.Services;

using Windows.ApplicationModel.Activation;

namespace Controls14.Activation
{
    internal class DefaultLaunchActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
    {
        private readonly Type _navElement;

        public DefaultLaunchActivationHandler(Type navElement)
        {
#pragma warning disable IDE0021 // Use expression body for constructors
            _navElement = navElement;
#pragma warning restore IDE0021 // Use expression body for constructors
        }

        protected override async Task HandleInternalAsync(LaunchActivatedEventArgs args)
        {
            // When the navigation stack isn't restored, navigate to the first page and configure
            // the new page by passing required information in the navigation parameter
            NavigationService.Navigate(_navElement, args.Arguments);

            await Task.CompletedTask;
        }

        protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
        {
            // None of the ActivationHandlers has handled the app activation
#pragma warning disable IDE0022 // Use expression body for methods
            return NavigationService.Frame.Content == null;
#pragma warning restore IDE0022 // Use expression body for methods
        }
    }
}
