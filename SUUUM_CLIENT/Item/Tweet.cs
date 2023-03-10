using CoreTweet;
using Prism.Commands;
using Prism.Mvvm;
using SUUUM_CLIENT.Service;
using SUUUM_CLIENT.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace SUUUM_CLIENT.Item
{
    /// <summary>
    /// Tweetのモデルクラスです。
    /// </summary>
    public class Tweet : BindableBase
    {
        /// <summary>
        /// ツイート識別ID
        /// </summary>
        public long StatusId { get; private set; }

        /// <summary>
        /// ユーザープロフィール画像
        /// </summary>
        public string UserImageURL { get; set; }

        /// <summary>
        /// ユーザー名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// ツイート本文
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// メディアの有無を判定します。(バインディングに利用)
        /// </summary>
        public bool HaveMedia1 { get => !string.IsNullOrEmpty(ImageURL1); }
        /// <summary>
        /// メディア（画像）のURL
        /// </summary>
        public string ImageURL1 { get; set; }
        public bool HaveMedia2 { get => !string.IsNullOrEmpty(ImageURL2); }
        public string ImageURL2 { get; set; }
        public bool HaveMedia3 { get => !string.IsNullOrEmpty(ImageURL3); }
        public string ImageURL3 { get; set; }
        public bool HaveMedia4 { get => !string.IsNullOrEmpty(ImageURL4); }
        public string ImageURL4 { get; set; }

        /// <summary>
        /// ツイートがお気に入りされているか
        /// </summary>
        private bool isFavorite;
        public bool IsFavorite
        {
            get { return isFavorite; }
            set
            {
                isFavorite = value;
                FavoriteVisibility = isFavorite ? Visibility.Visible : Visibility.Collapsed;
                NotFavoriteVisibility = isFavorite ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        /// <summary>
        /// いいねによる可視性制御用のプロパティ(バインディングに利用)
        /// </summary>
        private Visibility favoriteVisibility;
        public Visibility FavoriteVisibility
        {
            get { return favoriteVisibility; }
            set { this.SetProperty(ref this.favoriteVisibility, value); }

        }
        private Visibility notFavoriteVisibility;
        public Visibility NotFavoriteVisibility
        {
            get { return notFavoriteVisibility; }
            set { this.SetProperty(ref this.notFavoriteVisibility, value); }
        }

        /// <summary>
        /// いいねをするコマンド
        /// </summary>
        public DelegateCommand AddFavorite { get; set; }
        /// <summary>
        /// いいね解除のコマンド
        /// </summary>
        public DelegateCommand RemoveFavorite { get; set; }

        public ObservableCollection<UrlInformation> UrlCollection { get; set; } = new ObservableCollection<UrlInformation>();


        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="status"></param>
        public Tweet(Status status)
        {
            SetBaseStatus(status.User.ScreenName, status.User.Name, status.User.ProfileImageUrl, status.Text, status.Id, status.IsFavorited ?? false);

            if (status.ExtendedEntities?.Media?.Length > 0)
            {
                ImageURL1 = status.ExtendedEntities.Media[0].MediaUrl;
                Text = Text.Replace(status.ExtendedEntities.Media[0].Url, "");
            }
            if (status.ExtendedEntities?.Media?.Length > 1)
            {
                ImageURL2 = status.ExtendedEntities.Media[1].MediaUrl;
                Text = Text.Replace(status.ExtendedEntities.Media[1].Url, "");
            }
            if (status.ExtendedEntities?.Media?.Length > 2)
            {
                ImageURL3 = status.ExtendedEntities.Media[2].MediaUrl;
                Text = Text.Replace(status.ExtendedEntities.Media[2].Url, "");
            }
            if (status.ExtendedEntities?.Media?.Length > 3)
            {
                ImageURL4 = status.ExtendedEntities.Media[3].MediaUrl;
                Text = Text.Replace(status.ExtendedEntities.Media[3].Url, "");
            }

            //TODO Urlはこんな感じでとれるので、これをもとにリンクを作る
            //今は文末にくっつけてるが、文中に埋め込めるようにしたいところ
            if (status.Entities?.Urls?.Length > 0)
            {
                foreach (var url in status.Entities?.Urls)
                {
                    //使うurl合ってる？
                    UrlCollection.Add(new UrlInformation(url.ExpandedUrl, url.Url));
                    Text = Text.Replace(url.Url, "");
                }
            }
        }

        /// <summary>
        /// 基本のセットアップ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="userImageURL"></param>
        /// <param name="text"></param>
        /// <param name="statusId"></param>
        /// <param name="isFavorite"></param>
        private void SetBaseStatus(string id, string userName, string userImageURL, string text, long statusId, bool isFavorite)
        {
            UserID = id;
            UserName = userName;
            UserImageURL = userImageURL;
            Text = text;
            StatusId = statusId;
            IsFavorite = isFavorite;
            if (IsFavorite)
                FavoriteVisibility = Visibility.Visible;
            else
                FavoriteVisibility = Visibility.Collapsed;


            AddFavorite = new DelegateCommand(() =>
                {
                    TweetAccessor.Instance.AddFavorid(statusId);
                    IsFavorite = true;
                }
            );

            RemoveFavorite = new DelegateCommand(() =>
                {
                    TweetAccessor.Instance.RemoveFavorid(statusId);
                    IsFavorite = false;
                }
            );
        }
    }

    public class UrlInformation
    {

        public string Url { get; set; }
        public string DisplayText { get; set; }

        public UrlInformation()
        {
        }

        public UrlInformation(string url, string displayText)
        {
            Url = url;
            DisplayText = displayText;
        }
    }
}
