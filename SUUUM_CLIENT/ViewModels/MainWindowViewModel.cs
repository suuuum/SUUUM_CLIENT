using Prism.Mvvm;

namespace SUUUM_CLIENT.ViewModels
{
    public class tweetViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

    }
}
