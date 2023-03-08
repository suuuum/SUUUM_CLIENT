using Microsoft.Web.WebView2.Core;
using SUUUM_CLIENT.ViewModels;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

namespace SUUUM_CLIENT.Views
{
    /// <summary>
    /// Interaction logic for WebBrowser
    /// </summary>
    public partial class WebBrowser : UserControl
    {
        private WebBrowserViewModel VM;

        public WebBrowser()
        {
            InitializeComponent();
            InitializeAsync();
            VM = this.DataContext as WebBrowserViewModel;
        }

        async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.WebMessageReceived += UpdateAddressBar;
            await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.chrome.webview.postMessage(window.document.URL);");
           // await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.chrome.webview.addEventListener(\'message\', event => alert(event.data));");
        }

        void UpdateAddressBar(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            string uri = args.TryGetWebMessageAsString();
            addressBar.Text = uri;
            webView.CoreWebView2.PostWebMessageAsString(uri);
        }
        
        private void ReloadButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            webView.Reload();
        }

        private void addressBar_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                VM.DisplayURL =addressBar.Text;
                VM.MoveURLCommand();
            }
        }
    }
}
