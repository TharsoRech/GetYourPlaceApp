using GetYourPlaceApp.Helpers;

namespace GetYourPlaceApp.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel, IDisposable
    {

        #region Properties
        [ObservableProperty]
        bool isLogged;
        #endregion

        public AppShellViewModel()
        {
            SessionHelper.SessionChanged += SessionChanged;
        }

        private void SessionChanged(object? sender, bool e)
        {
            IsLogged = e;
        }

        [RelayCommand]
        public async Task Logoff()
        {
            SessionHelper.ResetToken();
        }

        public void Dispose()
        {
            SessionHelper.SessionChanged -= SessionChanged;
        }
    }
}
