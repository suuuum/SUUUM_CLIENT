using Prism.Commands;
using Prism.Mvvm;
using SUUUM_CLIENT.Service;
using SUUUM_CLIENT.ViewModels;
using System.Windows;

namespace SUUUM_CLIENT.Item
{
    public class Tweet : BindableBase
    {
        public DelegateCommand ShowImage1 { get; set; }

        public DelegateCommand ShowImage2 { get; set; }

        public DelegateCommand ShowImage3 { get; set; }

        public DelegateCommand ShowImage4 { get; set; }

        public DelegateCommand AddFavorite { get; set; }

        public DelegateCommand RemoveFavorite { get; set; }

        public TweetDocViewModel ViewModel { get; set; }

        public string Text { get; set; }

        public bool HaveMedia1 { get => !string.IsNullOrEmpty(ImageURL1); }
        public string ImageURL1 { get; set; }

        public bool HaveMedia2 { get => !string.IsNullOrEmpty(ImageURL2); }
        public string ImageURL2 { get; set; }

        public bool HaveMedia3 { get => !string.IsNullOrEmpty(ImageURL3); }
        public string ImageURL3 { get; set; }

        public bool HaveMedia4 { get => !string.IsNullOrEmpty(ImageURL4); }
        public string ImageURL4 { get; set; }

        public string UserImageURL { get; set; }

        public string UserName { get; set; }

        public string UserID { get; set; }
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
        public long StatusId { get; private set; }

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
        /// mediaがある場合
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="userImageURL"></param>
        /// <param name="text"></param>
        /// <param name="imageUrl"></param>
        public Tweet(TweetDocViewModel tweetViewModel, string id, string userName, string userImageURL, string text, string imageUrl1, string imageUrl2, string imageUrl3, string imageUrl4, long statusId, bool isFavorite)
        {
            SetBaseStatus(tweetViewModel, id, userName, userImageURL, text, statusId, isFavorite);
            SetMediaStatus(imageUrl1, imageUrl2, imageUrl3, imageUrl4);

        }

        /// <summary>
        /// mediaがない場合
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="userImageURL"></param>
        /// <param name="text"></param>
        /// <param name="imageUrl"></param>
        public Tweet(TweetDocViewModel tweetViewModel, string id, string userName, string userImageURL, string text, long statusId, bool isFavorite)
        {
            SetBaseStatus(tweetViewModel, id, userName, userImageURL, text, statusId, isFavorite);
        }


        private void SetBaseStatus(TweetDocViewModel tweetViewModel, string id, string userName, string userImageURL, string text, long statusId, bool isFavorite)
        {
            ViewModel = tweetViewModel;
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

        private void SetMediaStatus(string imageUrl1, string imageUrl2, string imageUrl3, string imageUrl4)
        {
            ImageURL1 = imageUrl1;
            ImageURL2 = imageUrl2;
            ImageURL3 = imageUrl3;
            ImageURL4 = imageUrl4;

            ShowImage1 = new DelegateCommand(() =>
            {
                ViewModel.PublishEvent(new ImageInformation { ImageUrl = ImageURL1 });
            },
           () => true);

            ShowImage2 = new DelegateCommand(() =>
            {
                ViewModel.PublishEvent(new ImageInformation { ImageUrl = ImageURL2 });
            },
           () => true);

            ShowImage3 = new DelegateCommand(() =>
            {
                ViewModel.PublishEvent(new ImageInformation { ImageUrl = ImageURL3 });
            },
           () => true);

            ShowImage4 = new DelegateCommand(() =>
            {
                ViewModel.PublishEvent(new ImageInformation { ImageUrl = ImageURL4 });
            },
           () => true);

        }
    }
}
