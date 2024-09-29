namespace GetYourPlaceApp.Models
{
    public class Propertie
    {
        public int Id { get; set; }
        public int GYPUserProfileId { get; set; }
        public GYPTypeOfRent TypeOfRent { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } // Use decimal for currency
        public bool IsVisibleForUsers { get; set; }
        public bool IsAvailable { get; set; }
        public string ImageUrl { get; set; }
        public int Star { get; set; }
        public List<GYPPropertyInfoItem> PropertyInformations { get; set; }
    }
}
