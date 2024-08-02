using Prism.Commands;
using Prism.Mvvm;

namespace BlankApp2.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Chatting";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private DelegateCommand _TCPIP;
        public DelegateCommand TCPIP =>
            _TCPIP ?? (_TCPIP = new DelegateCommand(ExecuteCommandName));

        void ExecuteCommandName()
        {

        }

        public MainWindowViewModel()
        {

        }
    }
}
