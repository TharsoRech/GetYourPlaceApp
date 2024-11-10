namespace GetYourPlaceApp.Models
{
    public class GYPReview
    {
        public int Id { get; set; }
        public string GYPUserName { get; set; }
        public int GYPUserProfileId { get; set; }
        public string GYPUserDetails { get; set; }
        public int GYPPropertyId { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
    }
}
