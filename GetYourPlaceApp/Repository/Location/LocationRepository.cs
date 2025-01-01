using GetYourPlaceApp.Models.Requests;
using GetYourPlaceApp.Models.Responses;
using RestSharp;

namespace GetYourPlaceApp.Repository.Location
{
    public class LocationRepository: ILocationRepository
    {

        public async Task<List<CountryAndStateResponse>> GetCountriesAndStates()
        {
			try
			{
                var options = new RestClientOptions("https://countriesnow.space/api/v0.1/countries/states");
                var client = new RestClient(options);
                var request = new RestRequest("");
                var response = await client.GetAsync(request);

                Console.WriteLine("{0}", response.Content);

                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<CountryAndStateResponse>>(response.Content);
            }
			catch (Exception ex)
			{
                Console.WriteLine("{0}", ex.Message);
                return null;
            }
        }


        public async Task<List<CityResponse>> GetCityByState(CityRequest cityRequest)
        {
            try
            {
                var options = new RestClientOptions("https://countriesnow.space/api/v0.1/countries/state/cities");
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.AddBody(cityRequest);
                var response = await client.PostAsync(request);

                Console.WriteLine("{0}", response.Content);

                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<CityResponse>>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
                return null;
            }
        }

    }
}
