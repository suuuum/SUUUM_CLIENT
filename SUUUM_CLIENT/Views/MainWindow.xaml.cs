using AvalonDock.Layout;
using SUUUM_CLIENT.Service;
using System.Windows;

namespace SUUUM_CLIENT.Views
{
    /// <summary>
    /// Interaction logic for tweet.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public LayoutPanel LayoutPanel { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            WindowAgent.MainWindow = this;
            LayoutPanel = MainPanel;
        }
    }
}
