namespace GetYourPlaceApp.Models
{
    public class Country
    {
        public string name { get; set; }
        public string iso3 { get; set; }
        public List<State> states { get; set; }
    }
}
