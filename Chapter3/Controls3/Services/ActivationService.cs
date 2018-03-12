using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Controls3.Activation;

using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Controls3.Services
{
    // For more information on application activation see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/activation.md
    internal class ActivationService
    {
        private readonly App _app;
        private readonly Lazy<UIElement> _shell;
        private readonly Type _defaultNavItem;

        public ActivationService(App app, Type defaultNavItem, Lazy<UIElement> shell = null)
        {
            _app = app;
            _shell = shell;
            _defaultNavItem = defaultNavItem;
        }

        public async Task ActivateAsync(object activationArgs)
        {
            if (IsInteractive(activationArgs))
            {
                // Initialize things like registering background task before the app is loaded
                await InitializeAsync();

                // Do not repeat app initialization when the Window already has content,
                // just ensure that the window is active
                if (Window.Current.Content == null)
                {
                    // Create a Frame to act as the navigation context and navigate to the first page
                    Window.Current.Content = _shell?.Value ?? new Frame();
                    NavigationService.NavigationFailed += (sender, e) =>
                    {
                        throw e.Exception;
                    };
                    NavigationService.Navigated += Frame_Navigated;
                    if (SystemNavigationManager.GetForCurrentView() != null)
                    {
                        SystemNavigationManager.GetForCurrentView().BackRequested += ActivationService_BackRequested;
                    }
                }
            }

#pragma warning disable IDE0008 // Use explicit type
            var activationHandler = GetActivationHandlers()
#pragma warning restore IDE0008 // Use explicit type
                                                .FirstOrDefault(h => h.CanHandle(activationArgs));

            if (activationHandler != null)
            {
                await activationHandler.HandleAsync(activationArgs);
            }

            if (IsInteractive(activationArgs))
            {
#pragma warning disable IDE0008 // Use explicit type
                var defaultHandler = new DefaultLaunchActivationHandler(_defaultNavItem);
#pragma warning restore IDE0008 // Use explicit type
                if (defaultHandler.CanHandle(activationArgs))
                {
                    await defaultHandler.HandleAsync(activationArgs);
                }

                // Ensure the current window is active
                Window.Current.Activate();

                // Tasks after activation
                await StartupAsync();
            }
        }

        private async Task InitializeAsync()
        {
#pragma warning disable IDE0022 // Use expression body for methods
            await Task.CompletedTask;
#pragma warning restore IDE0022 // Use expression body for methods
        }

        private async Task StartupAsync()
        {
#pragma warning disable IDE0022 // Use expression body for methods
            await Task.CompletedTask;
#pragma warning restore IDE0022 // Use expression body for methods
        }

        private IEnumerable<ActivationHandler> GetActivationHandlers()
        {
            yield break;
        }

        private bool IsInteractive(object args)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            return args is IActivatedEventArgs;
#pragma warning restore IDE0022 // Use expression body for methods
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = NavigationService.CanGoBack ?
                AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
#pragma warning restore IDE0022 // Use expression body for methods
        }

        private void ActivationService_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
                e.Handled = true;
            }
        }
    }
}
