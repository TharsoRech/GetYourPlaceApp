using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Repository.Properties
{
    internal interface IPropertiesRepository
    {
        Task<List<Propertie>> GetProperties();
    }
}
