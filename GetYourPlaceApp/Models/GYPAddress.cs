namespace GetYourPlaceApp.Models
{
    public class GYPAddress
    {
        public int Id { get; set; }
        public int GYPUserProfileId { get; set; }
        public int GYPProfilePlaceHistoryId { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
