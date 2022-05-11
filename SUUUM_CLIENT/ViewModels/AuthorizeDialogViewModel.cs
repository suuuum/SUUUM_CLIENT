using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SUUUM_CLIENT.ViewModels
{
    public class AuthorizeDialogViewModel : BindableBase,IDialogAware
    {

        private string _Message;
        public string Message { get => this._Message; set => base.SetProperty(ref this._Message, value); }

        public DelegateCommand OkCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public AuthorizeDialogViewModel()
        {
            this.OkCommand = new DelegateCommand(this.Ok);
        }
       

        public void Ok()
        {
            var dialogParameters = new DialogParameters();
            dialogParameters.Add("Authorize",Message);
            this.RequestClose.Invoke(new DialogResult(ButtonResult.OK,dialogParameters));
        }

       

        // ↓ IDialogAware の実装

        public string Title => "認証ダイアログ";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
