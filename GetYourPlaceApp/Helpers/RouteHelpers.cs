namespace GetYourPlaceApp.Helpers
{
    public static class RouteHelpers
    {
        public static async Task GoToPage(string page)
        {
            await Shell.Current.GoToAsync(page);
        }
    }
}
