using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SUUUM_CLIENT.Item;
using SUUUM_CLIENT.Service;
using SUUUM_CLIENT.Views;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SUUUM_CLIENT.ViewModels
{
    public class TweetDocViewModel : BindableBase
    {
        private readonly TweetAccessor TweetAccessor;

        private readonly WindowAgent Agent;

        public static TweetList Home = new TweetList(0, "ホーム");

        public TweetDoc View { get; set; }

        public string Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }
        private string id = "Twitter";

        public DelegateCommand ReloadOn { get; set; }

        public DelegateCommand ReloadOff { get; set; }

        public DelegateCommand AddTweets { get; set; }

        private Visibility offModeVisibility;
        public Visibility OffModeVisibility
        {
            get { return offModeVisibility; }
            set { this.SetProperty(ref this.offModeVisibility, value); }

        }

        private Visibility onModeVisibility;
        public Visibility OnModeVisibility
        {
            get { return onModeVisibility; }
            set { this.SetProperty(ref this.onModeVisibility, value); }

        }


        private Timer ReloadTimer { get; set; }

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
            set
            {
                SetProperty(ref _selectedValue, value);
            }
        }
        private long _selectedValue;

        public TweetList SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);

            }
        }
        private TweetList _selectedItem;

        private readonly IEventAggregator _eventAggregator;




        public TweetDocViewModel(IEventAggregator eventAggregator, TweetAccessor accessor, IDialogService dialog, WindowAgent agent)
        {
            TweetAccessor = accessor;
            TweetAccessor.ViewModel = this;
            Agent = agent;
            OnModeVisibility = Visibility.Collapsed;
            OffModeVisibility = Visibility.Visible;
            bool isSetUp = false;
            _eventAggregator = eventAggregator;
            while (!TweetAccessor.IsAuthorized)
            {
                try
                {
                    if (TweetAccessor.AccessToken == null)
                        TweetAccessor.CreateToken(
                            ConfigurationManager.AppSettings["AccessToken"],
                            ConfigurationManager.AppSettings["AccessTokenSecret"],
                            ConfigurationManager.AppSettings["UserId"],
                            ConfigurationManager.AppSettings["ScreenName"]);
                    LoadLists();
                    LoadTimeLine();
                    isSetUp = true;
                    TweetAccessor.IsAuthorized = true;
                }
                catch (Exception)
                {
                    TweetAccessor.StartAuthorize();
                    string pin = "";
                    dialog.ShowDialog("AuthorizeDialog", result =>
                    {
                        pin = result.Parameters.GetValue<string>("Authorize");
                    });
                    TweetAccessor.SetToken(pin);
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["AccessToken"].Value = TweetAccessor.AccessToken.AccessToken;
                    config.AppSettings.Settings["AccessTokenSecret"].Value = TweetAccessor.AccessToken.AccessTokenSecret;
                    config.AppSettings.Settings["UserId"].Value = TweetAccessor.AccessToken.UserId.ToString();
                    config.AppSettings.Settings["ScreenName"].Value = TweetAccessor.AccessToken.ScreenName;
                    config.Save();
                }

            }


            if (!isSetUp)
            {
                LoadLists();
                LoadTimeLine();
            }

            ReloadTimer = new Timer(120000);
            ReloadTimer.Elapsed += (sender, e) =>
            {
                View.Dispatcher.Invoke(() =>
                Reload());
            };

            ReloadOn = new DelegateCommand(() =>
            {
                OnModeVisibility = Visibility.Visible;
                OffModeVisibility = Visibility.Collapsed;
                Reload();
                ReloadTimer.Start();
            },
            () => true);

            ReloadOff = new DelegateCommand(() =>
            {
                OnModeVisibility = Visibility.Collapsed;
                OffModeVisibility = Visibility.Visible;
                ReloadTimer.Stop();
            }, () => true);

            AddTweets = new DelegateCommand(() =>
            {
                Items.AddRange(TweetAccessor.getListTimeline(SelectedValue, 100, page + 1, true));
                page++;
            },
            () => true);
        }


        public ObservableCollection<Tweet> Items { get; set; }

        private void Reload()
        {
            Items.Clear();
            Items.AddRange(TweetAccessor.getListTimeline(SelectedValue, 100, 1, true));
            page = 1;
            Agent.SetTweetDocTitle(Id, SelectedItem.Name);
        }

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

        public void PublishEvent(ImageInformation imageInfo)
        {
            _eventAggregator.GetEvent<ShowImageEvent<ImageInformation>>().Publish(imageInfo);
        }
    }

    public class ShowImageEvent<ImageInformation> : PubSubEvent<ImageInformation>
    {

    }
}
