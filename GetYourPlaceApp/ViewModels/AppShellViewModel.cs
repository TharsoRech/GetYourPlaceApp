using GetYourPlaceApp.Helpers;
using Microsoft.Extensions.Logging;

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
            SessionHelper.Instance.SessionChanged += SessionChanged;
            IsLogged = SessionHelper.Instance.User is null ? false : SessionHelper.Instance.User.IsLogged;
        }

        private void SessionChanged(object? sender, bool e)
        {
            IsLogged = e;
        }

        [RelayCommand]
        public async Task Logoff()
        {
            SessionHelper.Instance.LogoffAsync();
        }

        public void Dispose()
        {
            SessionHelper.Instance.SessionChanged -= SessionChanged;
        }
    }
}
