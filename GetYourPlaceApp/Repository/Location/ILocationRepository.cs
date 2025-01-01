using GetYourPlaceApp.Models.Requests;
using GetYourPlaceApp.Models.Responses;

namespace GetYourPlaceApp.Repository.Location
{
    public interface ILocationRepository
    {
        Task<List<CountryAndStateResponse>> GetCountriesAndStates();

        Task<List<CityResponse>> GetCityByState(CityRequest cityRequest);
    }
}
