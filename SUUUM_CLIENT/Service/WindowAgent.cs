using AvalonDock.Layout;
using SUUUM_CLIENT.ViewModels;
using SUUUM_CLIENT.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WebBrowser = SUUUM_CLIENT.Views.WebBrowser;

namespace SUUUM_CLIENT.Service
{
    /// <summary>
    /// ウィンドウ管理用のクラス
    /// </summary>
    public class WindowAgent
    {
        /// <summary>
        /// メインウィンドウ
        /// </summary>
        public static MainWindow MainWindow { get; set; }

        /// <summary>
        /// 設定されている各ウィンドウIDの最大値(ウィンドウが追加されると増える)
        /// </summary>
        private long TweetDocIdMax { get; set; } = 1;

        private long BrowserIdMax { get; set; } = 1;

        /// <summary>
        /// Twitter画面(TweetDoc)をメインウインドウに追加します。
        /// </summary>
        public void AddTweetDoc()
        {

            var newTwitterDocLayout = new LayoutDocument { Title = "Twitter", ContentId = "Twitter" + (TweetDocIdMax + 1), Content = new TweetDoc() };
            TweetDocIdMax++;
            ((TweetDocViewModel)((TweetDoc)newTwitterDocLayout.Content).DataContext).Id = newTwitterDocLayout.ContentId;
            AddDocument<TweetDoc>(newTwitterDocLayout);
        }

        /// <summary>
        /// Twitter画面のタイトルを変更します。
        /// </summary>
        /// <param name="contentId">対象Twitter画面のID</param>
        /// <param name="title">設定したいタイトル</param>
        public void SetTweetDocTitle(string contentId, string title)
        {
            SetTitile<TweetDoc>(contentId, title);
        }

        /// <summary>
        /// WebBrowser画面をメインウインドウに追加します。
        /// </summary>
        public void AddWebBrouserDoc()
        {

            var newBrowserDocLayout = new LayoutDocument { Title = "Browser", ContentId = "Browser" + (BrowserIdMax + 1), Content = new WebBrowser() };
            BrowserIdMax++;
            ((WebBrowserViewModel)((WebBrowser)newBrowserDocLayout.Content).DataContext).Id = newBrowserDocLayout.ContentId;
            AddDocument<WebBrowser>(newBrowserDocLayout);
        }

        /// <summary>
        /// TweetImageBrowser画面をメインウインドウに追加します。
        /// </summary>
        public void AddImageViewerDoc()
        {
            var newImageDocLayout = new LayoutDocument { Title = "ImageViewer", ContentId = "ImageViewer" + (BrowserIdMax + 1), Content = new TweetImageViewer() };
            BrowserIdMax++;
            AddDocument<TweetImageViewer>(newImageDocLayout);
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
        private void SetTitile<T>(string contentId, string title) where T : UserControl
        {
            var docPanels = MainWindow.LayoutPanel.Children;

            foreach (var item in docPanels)
            {
                LayoutContent layout = SearchContent<T>(contentId, title, item);

                if (layout != null)
                {
                    layout.Title = title;
                    return;
                }
            }
        }

        /// <summary>
        /// レイアウトコンテンツを再帰的に検索します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentId"></param>
        /// <param name="title"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private LayoutContent SearchContent<T>(string contentId, string title, ILayoutElement element)
        {
            LayoutContent layout = null;
            if (element is LayoutDocumentPane pane)
            {
                layout = pane.Children.
               FirstOrDefault(target => target.Content.GetType() == typeof(T) && target.ContentId == contentId);

            }

            //再帰関数にする
            if (element is LayoutDocumentPaneGroup group)
            {
                var layouts = group.Children;
                foreach (var content in layouts)
                {
                    layout = SearchContent<T>(contentId, title, content);

                }
            }

            return layout;

        }
    }
}
