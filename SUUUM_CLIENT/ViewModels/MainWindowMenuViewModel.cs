using AvalonDock.Layout;
using Prism.Commands;
using Prism.Mvvm;
using SUUUM_CLIENT.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SUUUM_CLIENT.ViewModels
{
    public class MainWindowMenuViewModel : BindableBase
    {

        private readonly WindowAgent agent;

        public DelegateCommand OpenTwitter { get; set; }

        public DelegateCommand OpenWebBrowser { get; set; }



        public MainWindowMenuViewModel(WindowAgent agent)
        {
            this.agent = agent;
            OpenTwitter = new DelegateCommand(() =>
            OpenTwitterCommand(),
           () => true);

            OpenWebBrowser = new DelegateCommand(() =>
            OpenBrowserCommand(),
           () => true);
        }

        private void OpenTwitterCommand()
        {
            agent.AddTweetDoc();
        }

        private void OpenBrowserCommand()
        {
            agent.AddWebBrouserDoc();
        }
    }
}
