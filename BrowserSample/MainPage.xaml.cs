using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BrowserSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;

            if (frame.CanGoBack || webview.CanGoBack)
            {
                e.Handled = true;

                if (webview.CanGoBack)
                    webview.GoBack();


                if (frame.CanGoBack)
                    frame.GoBack();
            }
        }

        private void addressBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                string url = addressBox.Text.ToLower();

                if (!url.Contains("http://") || !url.Contains("https://"))
                {
                    url = "http://" + url;
                }

                webview.Navigate(new Uri(url));
            }
        }

        private void webview_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            addressBox.Text = webview.Source.ToString();

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    webview.CanGoBack ?
                    AppViewBackButtonVisibility.Visible :
                    AppViewBackButtonVisibility.Collapsed;
        }
    }
}
