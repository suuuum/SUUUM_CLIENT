using CoreTweet;
using SUUUM_CLIENT.Item;
using SUUUM_CLIENT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUUUM_CLIENT.Service
{
    /// <summary>
    /// Tweetにアクセスします。
    /// </summary>
    public class TweetAccessor
    {
        /// <summary>アクセストークン</summary>
        private readonly Tokens AccessToken = Tokens.Create("ama6btJ08FK1egHYz2MntFCTF",        // API key
                "bDi3Q3vIy2EqotLsDD92CEqDRRyLrzufAYgx5oB0vdrrsL0T2o",                    // API secret key
                "1289495716541292548-ENhOyR8vI0EOSRzricMDZfW6cnkb2r",                  // Access token
                "ssZe3EcivC6ztTyAZytAfcCwL3MBZtnu9z7EFK628ek1N");          // Access token secret

        public MainWindowViewModel ViewModel { get; set; }

      


        /// <summary>
        /// ホームタイムラインを取得します。
        /// </summary>
        /// <param name="count">tweetの数</param>
        /// <param name="page">ページ</param>
        /// <param name="exclude_replies">リプライを除くか</param>
        /// <returns></returns>
        public List<Tweet> getHomeTimeline(int c,int p,bool e_r)
        {
            var homeTimeline = new List<Tweet>();

            // タイムラインを取得
            var tl = AccessToken.Statuses.HomeTimeline(count => c, page => p, exclude_replies => e_r);

            // リストへの書き込み
            foreach (var value in tl)
            {
                var tweet = value.RetweetedStatus ?? value;


                switch (tweet.ExtendedEntities?.Media?.Length??0)
                {
                    case 1:
                        homeTimeline.Add(new Tweet(ViewModel,tweet.User.ScreenName, tweet.User.Name, tweet.User.ProfileImageUrl, tweet.Text, tweet.ExtendedEntities.Media[0].MediaUrl));
                        break;
                    case 2:
                        homeTimeline.Add(new Tweet(ViewModel, tweet.User.ScreenName, tweet.User.Name, tweet.User.ProfileImageUrl, tweet.Text, tweet.ExtendedEntities.Media[0].MediaUrl,tweet.ExtendedEntities.Media[1].MediaUrl));
                        break;

                    case 3:
                        homeTimeline.Add(new Tweet(ViewModel,tweet.User.ScreenName, tweet.User.Name, tweet.User.ProfileImageUrl, tweet.Text, tweet.ExtendedEntities.Media[0].MediaUrl, tweet.ExtendedEntities.Media[1].MediaUrl, tweet.ExtendedEntities.Media[2].MediaUrl));
                        break;
                    case 4:
                        homeTimeline.Add(new Tweet(ViewModel, tweet.User.ScreenName, tweet.User.Name, tweet.User.ProfileImageUrl, tweet.Text, tweet.ExtendedEntities.Media[0].MediaUrl, tweet.ExtendedEntities.Media[1].MediaUrl, tweet.ExtendedEntities.Media[2].MediaUrl, tweet.ExtendedEntities.Media[3].MediaUrl));
                        break;
                    default:
                        homeTimeline.Add(new Tweet(ViewModel, tweet.User.ScreenName, tweet.User.Name, tweet.User.ProfileImageUrl, tweet.Text));
                        break;
                }


               

            }

            return homeTimeline;
        }


    }
}
