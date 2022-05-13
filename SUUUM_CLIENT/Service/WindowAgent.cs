using AvalonDock.Layout;
using SUUUM_CLIENT.ViewModels;
using SUUUM_CLIENT.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WebBrowser = SUUUM_CLIENT.Views.WebBrowser;

namespace SUUUM_CLIENT.Service
{
    public class WindowAgent
    {
        public static MainWindow MainWindow { get; set; }

        private long TweetDocIdMax { get; set; } = 1;

        private long BrowserIdMax { get; set; } = 1;

        /// <summary>
        /// Twitter画面(TweetDoc)をメインウインドウに追加します。
        /// </summary>
        public void AddTweetDoc()
        {

            var newTwitterDocLayout = new LayoutDocument {Title="Twitter",ContentId = "Twitter"+(TweetDocIdMax+1) ,Content = new TweetDoc()};
            TweetDocIdMax++;
            ((TweetDocViewModel)((TweetDoc)newTwitterDocLayout.Content).DataContext).Id = newTwitterDocLayout.ContentId;
            AddDocument<TweetDoc>(newTwitterDocLayout);
        }

        /// <summary>
        /// Twitter画面のタイトルを変更します。
        /// </summary>
        /// <param name="contentId">対象Twitter画面のID</param>
        /// <param name="title">設定したいタイトル</param>
        public void SetTweetDocTitle(string contentId,string title)
        {
            SetTitile<TweetDoc>(contentId, title);
        }

        /// <summary>
        /// Twitter画面(TweetDoc)をメインウインドウに追加します。
        /// </summary>
        public void AddWebBrouserDoc()
        {

            var newBrowserDocLayout = new LayoutDocument { Title = "Browser", ContentId = "Browser" + (BrowserIdMax + 1), Content = new WebBrowser() };
            BrowserIdMax++;
            ((WebBrowserViewModel)((WebBrowser)newBrowserDocLayout.Content).DataContext).Id = newBrowserDocLayout.ContentId;
            AddDocument<WebBrowser>(newBrowserDocLayout);
        }

        /// <summary>
        /// 画面をメインウインドウに追加します
        /// </summary>
        /// <typeparam name="T">追加したい画面のクラス</typeparam>
        /// <param name="docLayout">追加したいレイアウトドキュメント</param>
        private void AddDocument<T>(LayoutDocument docLayout) where T : UserControl
        {

            var docPanels = MainWindow.LayoutPanel.Children;
            foreach (var item in docPanels)
            {
                if (item.GetType() != typeof(LayoutDocumentPane))
                    continue;

                var layout = ((LayoutDocumentPane)item).Children.FirstOrDefault(target => target.Content.GetType() == typeof(T));

                if (layout != null)
                {

                    ((LayoutDocumentPane)item).Children.Add(docLayout);
                    return;
                }

            }

            var newDocpane = new LayoutDocumentPane();
            newDocpane.Children.Add(docLayout);
            MainWindow.LayoutPanel.Children.Add(newDocpane);
        }


        /// <summary>
        /// タイトルを設定します。
        /// </summary>
        /// <typeparam name="T">対象画面のクラス</typeparam>
        /// <param name="contentId">ID</param>
        /// <param name="title">設定したいタイトル</param>
        private void SetTitile<T>(string contentId,string title) where T:UserControl
        {
            var docPanels = MainWindow.LayoutPanel.Children;
            foreach (var item in docPanels)
            {
                if (item.GetType() != typeof(LayoutDocumentPane))
                    continue;

                var layout = ((LayoutDocumentPane)item).Children.
                    FirstOrDefault(target => target.Content.GetType() == typeof(T) && target.ContentId == contentId);

                if (layout != null)
                {
                    layout.Title = title;
                }
            }
        }




    }
}
