using GetYourPlaceApp.Models.Enums;

namespace GetYourPlaceApp.Models
{
    public class GYPFilter
    {
       public string Description { get; set; }

        public GYPPropertyInfo GYPPropertyInfo { get; set; }

        public List<GYPPropertyInfoItem> Items { get; set; }

        public GYPFilter SelectedGYPFilter { get; set; } // Add this property

        public int? SelectedIndexFilter { get; set; }

    }
}
