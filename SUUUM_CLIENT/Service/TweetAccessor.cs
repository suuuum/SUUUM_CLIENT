using CoreTweet;
using CoreTweet.Core;
using SUUUM_CLIENT.Item;
using SUUUM_CLIENT.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using static CoreTweet.OAuth;

namespace SUUUM_CLIENT.Service
{
    /// <summary>
    /// Tweetにアクセスします。
    /// </summary>
    public class TweetAccessor
    {
        /// <summary>アクセストークン</summary>
        internal static Tokens AccessToken { get; private set; }
        /// <summary>
        /// 認証セッション
        /// </summary>
        private OAuthSession Session;

        public bool IsAuthorized { get; set; }

        public TweetDocViewModel ViewModel { get; set; }


        public readonly static TweetAccessor Instance = new TweetAccessor();

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
                TimeLine.Add(new Tweet(tweet));
            }

            return TimeLine;
        }

        public void AddFavorid(long statusId)
        {
            AccessToken.Favorites.Create(id => statusId);
        }

        public void RemoveFavorid(long statusId)
        {
            AccessToken.Favorites.Destroy(id => statusId);
        }

        public ListedResponse<List> GetLists()
        {
            return AccessToken.Lists.List();
        }

        public void StartAuthorize()
        {
            Session = OAuth.Authorize(ConfigurationManager.AppSettings["APIKey"], ConfigurationManager.AppSettings["APIKeySecret"]);
            System.Diagnostics.Process.Start(Session.AuthorizeUri.AbsoluteUri);
        }

        public void SetToken(string pin)
        {
            AccessToken = OAuth.GetTokens(Session, pin);
        }

        public void CreateToken(string accessToken, string accessTokenSecret, string userId, string screenName)
        {
            AccessToken = Tokens.Create(ConfigurationManager.AppSettings["APIKey"], ConfigurationManager.AppSettings["APIKeySecret"], accessToken, accessTokenSecret, Int64.Parse(userId), screenName);
        }

    }
}
