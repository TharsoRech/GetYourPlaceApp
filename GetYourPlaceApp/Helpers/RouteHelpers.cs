namespace GetYourPlaceApp.Helpers
{
    public static class RouteHelpers
    {
        public static async Task LogoffAsync()
        {
            try
            {
                SessionHelper.ResetToken();
                await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage(new MainViewModel()));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
