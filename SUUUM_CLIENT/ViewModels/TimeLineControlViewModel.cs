using CoreTweet;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using SUUUM_CLIENT.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SUUUM_CLIENT.ViewModels
{
    public class TimeLineControlViewModel : BindableBase
    {
        public DelegateCommand<string> ShowImage { get; set; }
        private readonly IEventAggregator _eventAggregator;
        public TimeLineControlViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            ShowImage = new DelegateCommand<string>(ShowImageCommand);
        }
        private void PublishEvent(ImageInformation imageInfo)
        {
            _eventAggregator.GetEvent<ShowImageEvent<ImageInformation>>().Publish(imageInfo);
        }

        private void ShowImageCommand(string imageUrl)
        {
            ImageInformation info = new ImageInformation();
            info.ImageUrl = imageUrl;
            PublishEvent(info);
        }

    }
    public class ShowImageEvent<ImageInformation> : PubSubEvent<ImageInformation>
    {
    }
}
