namespace GetYourPlaceApp.Helpers
{
    public static class RouteHelpers
    {
        public static async Task GoToPage(string page, Dictionary<string, object> parameters = null)
        {
            if (parameters != null)
                await Shell.Current.GoToAsync(page, parameters);
            else
                await Shell.Current.GoToAsync(page);
 
        }
    }
}
