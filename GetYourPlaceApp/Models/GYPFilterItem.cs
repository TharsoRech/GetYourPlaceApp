using GetYourPlaceApp.Models.Enums;

namespace GetYourPlaceApp.Models
{
    public class GYPFilterItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public GYPFilterType GYPFilterType { get; set; }
        public string FullDescription => $"{GYPFilterType} : {Description}";
    }
}
