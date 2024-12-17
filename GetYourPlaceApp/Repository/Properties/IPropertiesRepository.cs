using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Repository.Properties
{
    internal interface IPropertiesRepository
    {
        Task<List<Property>> GetProperties();

        Task<Property> GetPropertyInfo(Property property);

        Task<List<Property>> GetLikedProperties();
    }
}
