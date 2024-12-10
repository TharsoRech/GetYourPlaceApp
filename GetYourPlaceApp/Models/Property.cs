using GetYourPlaceApp.Helpers;

namespace GetYourPlaceApp.Models
{
    public class Property
    {
        public int Id { get; set; }
        public int GYPUserProfileId { get; set; }
        public GYPTypeOfRent TypeOfRent { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } // Use decimal for currency
        public bool IsVisibleForUsers { get; set; }
        public bool IsAvailable { get; set; }
        public string Base64Image { get; set; }
        public int Star { get; set; }
        public DateTime PuplishedAt { get; set; }
        public List<GYPPropertyInfoItem> PropertyInformations { get; set; }
        public ImageSource ImageSource => Base64Image.Base64ToImageSource();
        public List<string> Base64Images { get; set; }
        public bool UnderAnalysis { get;set; }
        public List<ImageSource> Images
        {
            get
            {
                if (Base64Images != null && Base64Images.Count > 0)
                {
                    List<ImageSource> images = new List<ImageSource>();
                    Base64Images.ForEach(im => images.Add(im.Base64ToImageSource()));
                    return images;
                }
                return new List<ImageSource>();
            }
        }
        public ObservableCollection<GYPReview> Reviews{ get; set; }
        public string PriceFormated => $"Price: {String.Format("{0:C}", Price)}";
        public string TypeOFRentFormated => $"type of acquisition: {TypeOfRent?.Description}";
        public string PublishedAtFormated => $"Published At: {PuplishedAt.ToShortDateString()}";
        public string AddressFormated => $"Address: {Address}";
        public string Type => $"Type: {PropertyInformations.FirstOrDefault(p => p.GYPPropertyInfo 
        == Enums.GYPPropertyInfo.PropertyType)?.Description}";

    }
}
