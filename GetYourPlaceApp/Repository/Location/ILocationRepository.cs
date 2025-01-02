using GetYourPlaceApp.Models;
using GetYourPlaceApp.Models.Requests;
using GetYourPlaceApp.Models.Responses;

namespace GetYourPlaceApp.Repository.Location
{
    public interface ILocationRepository
    {
        Task<List<Country>> GetCountriesAndStates();

        Task<List<string>> GetCityByState(CityRequest cityRequest);
    }
}
