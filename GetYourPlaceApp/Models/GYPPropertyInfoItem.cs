using GetYourPlaceApp.Models.Enums;

namespace GetYourPlaceApp.Models
{
    public class GYPPropertyInfoItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public GYPPropertyInfo GYPPropertyInfo { get; set; }
        public string FullDescription => $"{GYPPropertyInfo} : {Description}";
    }
}
