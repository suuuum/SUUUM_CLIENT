using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SUUUM_CLIENT.Item;
using SUUUM_CLIENT.Service;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SUUUM_CLIENT.ViewModels
{
    public class TweetDocViewModel : BindableBase
    {
        private readonly TweetAccessor TweetAccessor;

        public static TweetList Home = new TweetList(0, "ホーム");


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

        public ObservableCollection<TweetList> Lists
        {
            get { return _lists; }
            private set { SetProperty(ref _lists, value); }
        }
        private ObservableCollection<TweetList> _lists = new ObservableCollection<TweetList>();

        public long SelectedValue
        {
            get { return _selectedValue; }
            set { SetProperty(ref _selectedValue, value); }
        }
        private long _selectedValue;




        public TweetDocViewModel(TweetAccessor accessor, IDialogService dialog)
        {
            TweetAccessor = accessor;
            if (!TweetAccessor.IsAuthorized)
            {
                TweetAccessor.StartAuthorize();
                string pin="";

                dialog.ShowDialog("AuthorizeDialog", result =>
                 {
                     pin = result.Parameters.GetValue<string>("Authorize");
                 });

                TweetAccessor.SetToken(pin);
            }


            TweetAccessor.ViewModel = this;
            LoadLists();
            LoadTimeLine();

            Reload = new DelegateCommand(() =>
            {
                Items.Clear();
                Items.AddRange(TweetAccessor.getListTimeline(SelectedValue, 100, 1, true));
                page = 1;
            },
            () => true);

            AddTweets = new DelegateCommand(() =>
            {
                Items.AddRange(TweetAccessor.getListTimeline(SelectedValue, 100, page + 1, true));
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

        private void LoadLists()
        {
            var lists = TweetAccessor.GetLists();

            Lists = new ObservableCollection<TweetList>();
            Lists.Add(Home);
            foreach (var item in lists)
            {
                Lists.Add(new TweetList(item.Id, item.Name));
            }
        }
    }
}
