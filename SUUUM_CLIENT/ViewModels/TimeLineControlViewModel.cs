using CoreTweet;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using SUUUM_CLIENT.Item;
using SUUUM_CLIENT.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SUUUM_CLIENT.ViewModels
{
    public class TimeLineControlViewModel : BindableBase
    {
        public DelegateCommand<string> ShowImage { get; set; }
        public DelegateCommand<string> ClickLink { get; set; }
        private readonly IEventAggregator _eventAggregator;
        private readonly WindowAgent _agent;
        public TimeLineControlViewModel(IEventAggregator eventAggregator,WindowAgent agent)
        {
            _eventAggregator = eventAggregator;
            _agent = agent;
            ShowImage = new DelegateCommand<string>(ShowImageCommand);
            ClickLink = new DelegateCommand<string>(ClickUrlCommand);
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

        private void ClickUrlCommand(string url)
        {
            _agent.AddWebBrouserDoc(url);
        }

    }
    public class ShowImageEvent<ImageInformation> : PubSubEvent<ImageInformation>
    {
    }
}
