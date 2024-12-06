using System.Windows.Input;

namespace GetYourPlaceApp.Components.ViewModels
{
    public partial class MessagePopUpComponentViewModel:BaseViewModel
    {
        private ICommand _messageClickOkCommand;

        #region Properties
        [ObservableProperty]
        public string title;

        [ObservableProperty]
        public string subTitle;

        [ObservableProperty]
        public string message;
        #endregion
        public MessagePopUpComponentViewModel(string title, string subTitle, string message,ICommand messageClickOkCommand)
        {
            Title = title;
            SubTitle = subTitle;
            Message = message;
            _messageClickOkCommand = messageClickOkCommand;
        }

        public void ExecuteClickOkCommand()
        {
            _messageClickOkCommand?.Execute(null);
        }
    }
}
