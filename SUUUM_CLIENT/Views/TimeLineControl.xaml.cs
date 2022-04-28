using SUUUM_CLIENT.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SUUUM_CLIENT.Views
{
    /// <summary>
    /// Interaction logic for TweetControl
    /// </summary>
    public partial class TimeLineControl : UserControl
    {

        public TimeLineControl()
        {
            InitializeComponent();
        }

        //public static readonly DependencyProperty TextProperty = DependencyProperty.Register("TweetText", typeof(string), typeof(TweetControl), new FrameworkPropertyMetadata("TweetText"));
        //public string TweetText
        //{
        //    get { return (string)GetValue(TextProperty); }
        //    set { SetValue(TextProperty, value); }
        //}

        //public static readonly DependencyProperty ImageURLProperty = DependencyProperty.Register("TweetImageURL", typeof(string), typeof(TweetControl), new FrameworkPropertyMetadata("ImageURL"));
        //public string TweetImageURL
        //{
        //    get { return (string)GetValue(ImageURLProperty); }
        //    set { SetValue(ImageURLProperty, value); }
        //}
    }

}
