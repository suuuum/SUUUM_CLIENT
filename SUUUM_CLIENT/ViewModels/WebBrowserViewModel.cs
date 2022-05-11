using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SUUUM_CLIENT.ViewModels
{
    public class WebBrowserViewModel : BindableBase
    {
        private const string HomeURL = "https://www.google.com/?hl=ja";

        public string URL
        {
            get { return _URL; }
            set { SetProperty(ref _URL, value); }
        }
        private string _URL = HomeURL;

        public string DisplayURL
        {
            get { return _displayURL; }
            set { SetProperty(ref _displayURL, value); }
        }
        private string _displayURL = HomeURL;

        public DelegateCommand MoveURL { get; set; }

        public WebBrowserViewModel()
        {
            MoveURL = new DelegateCommand(() =>
            MoveURLCommand(),
           () => true);
        }

        public void MoveURLCommand()
        {
            if (DisplayURL.StartsWith("https://"))
                URL = DisplayURL;
            else
            {
                URL = "https://www.google.com/search?q=" + DisplayURL
                + "&oq=" + DisplayURL +
                "&aqs=chrome..69i57j0i4i433i512l2j0i4i512j0i4i433i512j0i4i512l3j0i4i131i433i512j0i4i512.1086j0j7&sourceid=chrome&ie=UTF-8";
            }
        }
    }
}
