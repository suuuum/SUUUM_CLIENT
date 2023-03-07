using Prism.Mvvm;
using SUUUM_CLIENT.Service;

namespace SUUUM_CLIENT.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "SUUUUM_CLIENT";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
