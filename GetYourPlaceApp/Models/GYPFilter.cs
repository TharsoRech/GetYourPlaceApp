using GetYourPlaceApp.Models.Enums;

namespace GetYourPlaceApp.Models
{
    public class GYPFilter
    {
       public string Description { get; set; }

        public GYPFilterType GYPFilterType { get; set; }

        public List<GYPFilterItem> Items { get; set; }

    }
}
