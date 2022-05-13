using SUUUM_CLIENT.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using SUUUM_CLIENT.ViewModels;
using SUUUM_CLIENT.Service;

namespace SUUUM_CLIENT
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TweetDoc, TweetDocViewModel>();
            containerRegistry.RegisterSingleton<TweetAccessor>();
            containerRegistry.RegisterSingleton<WindowAgent>();

            containerRegistry.RegisterDialog<AuthorizeDialog>();

        }
    }
}
