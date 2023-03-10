using SUUUM_CLIENT.Item;
using SUUUM_CLIENT.ViewModels;
using System;
using System.Collections.ObjectModel;
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

        public ObservableCollection<Tweet> ExtraColumns
        {
            get { return (ObservableCollection<Tweet>)GetValue(ExtraColumnsProperty); }
            set { SetValue(ExtraColumnsProperty, value); }
        }

        public static readonly DependencyProperty ExtraColumnsProperty =
            DependencyProperty.Register("ExtraColumns", typeof(ObservableCollection<Tweet>), typeof(TimeLineControl),
                                        new PropertyMetadata(new ObservableCollection<Tweet>(), OnChanged));

        static void OnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as TimeLineControl).OnChanged();

        }

        void OnChanged()
        {
            if (ExtraColumns != null)
                ExtraColumns.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(ExtraColumns_CollectionChanged);
        }

        void ExtraColumns_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Method intentionally left empty.
        }


        private void Hyperlink_RequestNavigate_1(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
    }

}
