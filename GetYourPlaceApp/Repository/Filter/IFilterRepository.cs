using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Repository.Filter
{
    public interface IFilterRepository
    {
        Task<List<GYPFilter>> GetFilters();
    }
}
