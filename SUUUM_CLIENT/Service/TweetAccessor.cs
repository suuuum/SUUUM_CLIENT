using CoreTweet;
using CoreTweet.Core;
using SUUUM_CLIENT.Item;
using SUUUM_CLIENT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CoreTweet.OAuth;

namespace SUUUM_CLIENT.Service
{
    /// <summary>
    /// Tweetにアクセスします。
    /// </summary>
    public class TweetAccessor
    {
        /// <summary>アクセストークン</summary>
        private Tokens AccessToken;
        /// <summary>
        /// 認証セッション
        /// </summary>
        private OAuthSession Session;

        public bool IsAuthorized { get; set; }

        public TweetDocViewModel ViewModel { get; set; }

        public TweetImageViewerViewModel ImageViewerViewModel { get; set; }

        public static TweetAccessor Instance = new TweetAccessor();

        /// <summary>
        /// ホームタイムラインを取得します。
        /// </summary>
        /// <param name="count">tweetの数</param>
        /// <param name="page">ページ</param>
        /// <param name="exclude_replies">リプライを除くか</param>
        /// <returns></returns>
        public List<Tweet> getHomeTimeline(int c, int p, bool e_r)
        {

            // タイムラインを取得
            var tl = AccessToken.Statuses.HomeTimeline(count => c, page => p, exclude_replies => e_r);

            return CreateTimeLine(tl);
        }


        public List<Tweet> getListTimeline(long id, int c, int p, bool e_r)
        {
            if (id == 0)
                return getHomeTimeline(c, p, e_r);

            var tl = AccessToken.Lists.Statuses(list_id => id, count => c, page => p, exclude_replies => e_r);

            return CreateTimeLine(tl);
        }

        private List<Tweet> CreateTimeLine(ListedResponse<Status> tl)
        {
            var TimeLine = new List<Tweet>();

            // リストへの書き込み
            foreach (var value in tl)
            {
                var tweet = value.RetweetedStatus ?? value;


                switch (tweet.ExtendedEntities?.Media?.Length ?? 0)
                {
                    case 1:
                        TimeLine.Add(new Tweet(ViewModel, ImageViewerViewModel, tweet.User.ScreenName, tweet.User.Name, tweet.User.ProfileImageUrl, tweet.Text, tweet.ExtendedEntities.Media[0].MediaUrl));
                        break;
                    case 2:
                        TimeLine.Add(new Tweet(ViewModel, ImageViewerViewModel, tweet.User.ScreenName, tweet.User.Name, tweet.User.ProfileImageUrl, tweet.Text, tweet.ExtendedEntities.Media[0].MediaUrl, tweet.ExtendedEntities.Media[1].MediaUrl));
                        break;

                    case 3:
                        TimeLine.Add(new Tweet(ViewModel, ImageViewerViewModel, tweet.User.ScreenName, tweet.User.Name, tweet.User.ProfileImageUrl, tweet.Text, tweet.ExtendedEntities.Media[0].MediaUrl, tweet.ExtendedEntities.Media[1].MediaUrl, tweet.ExtendedEntities.Media[2].MediaUrl));
                        break;
                    case 4:
                        TimeLine.Add(new Tweet(ViewModel, ImageViewerViewModel, tweet.User.ScreenName, tweet.User.Name, tweet.User.ProfileImageUrl, tweet.Text, tweet.ExtendedEntities.Media[0].MediaUrl, tweet.ExtendedEntities.Media[1].MediaUrl, tweet.ExtendedEntities.Media[2].MediaUrl, tweet.ExtendedEntities.Media[3].MediaUrl));
                        break;
                    default:
                        TimeLine.Add(new Tweet(ViewModel, tweet.User.ScreenName, tweet.User.Name, tweet.User.ProfileImageUrl, tweet.Text));
                        break;
                }
            }

            return TimeLine;
        }


        public ListedResponse<List> GetLists()
        {
            return AccessToken.Lists.List();
        }

        public void StartAuthorize()
        {
            Session = OAuth.Authorize("ama6btJ08FK1egHYz2MntFCTF", "bDi3Q3vIy2EqotLsDD92CEqDRRyLrzufAYgx5oB0vdrrsL0T2o");
            System.Diagnostics.Process.Start(Session.AuthorizeUri.AbsoluteUri);
        }

        public void SetToken(string pin)
        {
            try
            {
                AccessToken = OAuth.GetTokens(Session, pin);
                IsAuthorized = true;
            }
            catch (Exception e)
            {
                AccessToken = Tokens.Create("ama6btJ08FK1egHYz2MntFCTF",        // API key
                "bDi3Q3vIy2EqotLsDD92CEqDRRyLrzufAYgx5oB0vdrrsL0T2o",                    // API secret key
                "1289495716541292548-ENhOyR8vI0EOSRzricMDZfW6cnkb2r",                  // Access token
                "ssZe3EcivC6ztTyAZytAfcCwL3MBZtnu9z7EFK628ek1N");          // Access token secret

                IsAuthorized = true;
            }
        }

    }
}
