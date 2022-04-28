using CoreTweet;
using Prism.Commands;
using Prism.Mvvm;
using SUUUM_CLIENT.Item;
using SUUUM_CLIENT.Service;
using System;
using System.Collections.ObjectModel;

namespace SUUUM_CLIENT.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly TweetAccessor TweetAccessor = new TweetAccessor();


        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand Reload { get; set; }

        public DelegateCommand AddTweets { get; set; }

        private int page;

        private string _showImageURL = "";
        public string ShowImageURL
        {
            get { return _showImageURL; }
            set { SetProperty(ref _showImageURL, value); }
        }


        public MainWindowViewModel()
        {
            TweetAccessor.ViewModel = this;

            LoadTimeLine();

            Reload = new DelegateCommand(() =>
            {
                Items.Clear();
                Items.AddRange(TweetAccessor.getHomeTimeline(100, 1, true));
                page = 1;
            },
            () => true);

            AddTweets = new DelegateCommand(() =>
            {
                Items.AddRange(TweetAccessor.getHomeTimeline(100, page + 1, true));
                page++;
            },
            () => true);

        }


        public ObservableCollection<Tweet> Items { get; set; }



        private void LoadTimeLine()
        {
            // タイムラインを取得
            Items = (ObservableCollection<Tweet>)new ObservableCollection<Tweet>().AddRange(TweetAccessor.getHomeTimeline(100, 1, true));
            page = 1;
        }
    }
}
