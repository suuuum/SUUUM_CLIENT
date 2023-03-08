using SUUUM_CLIENT.ViewModels;
using System.Windows.Controls;

namespace SUUUM_CLIENT.Views
{
    /// <summary>
    /// TweetDoc.xaml の相互作用ロジック
    /// </summary>
    public partial class TweetDoc : UserControl
    {

        private TweetDocViewModel ViewModel;
        public TweetDoc()
        {
            InitializeComponent();
            ViewModel = (TweetDocViewModel)this.DataContext;
            ViewModel.View = this;
        }
    }
}
