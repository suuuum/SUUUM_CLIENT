using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using SUUUM_CLIENT.Item;
using SUUUM_CLIENT.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SUUUM_CLIENT.ViewModels
{
    public class TweetImageViewerViewModel : BindableBase
    {
        private readonly TweetAccessor TweetAccessor;

        private string _title = "TweetImageViewer";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _showImageURL = "";
        public string ShowImageURL
        {
            get { return _showImageURL; }
            set { SetProperty(ref _showImageURL, value); }
        }
        public TweetImageViewerViewModel(IEventAggregator eventAggregator, TweetAccessor accessor)
        {
            TweetAccessor = accessor;
            eventAggregator.GetEvent<ShowImageEvent<ImageInformation>>().Subscribe(HandleShowImageEvent);
        }

        private void HandleShowImageEvent(ImageInformation info)
        {
            ShowImageURL = info.ImageUrl;
        }
    }
}
