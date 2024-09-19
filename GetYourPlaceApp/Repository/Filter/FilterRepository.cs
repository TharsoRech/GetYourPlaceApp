using GetYourPlaceApp.Models;
using GetYourPlaceApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetYourPlaceApp.Repository.Filter
{
    public class FilterRepository:IFilterRepository
    {
        public async Task<List<GYPFilter>> GetFilters()
        {
            return new List<GYPFilter>
    {
        new GYPFilter
        {
            GYPFilterType = GYPFilterType.PropertyType,
            Description = "Property Type",
            Items = new List<GYPFilterItem>
            {
                new GYPFilterItem { Id = 1, Description = "Apartment" },
                new GYPFilterItem { Id = 2, Description = "House" },
                new GYPFilterItem { Id = 3, Description = "Site" }
            }
        },
        new GYPFilter
        {
            GYPFilterType = GYPFilterType.City,
            Description = "City",
            Items = new List<GYPFilterItem>
            {
                new GYPFilterItem { Id = 1, Description = "São Paulo" },
                new GYPFilterItem { Id = 2, Description = "Rio de Janeiro" },
                new GYPFilterItem { Id = 3, Description = "Belo Horizonte" }
            }
        },
        new GYPFilter
        {
            GYPFilterType = GYPFilterType.Code,
            Description = "Code",
            Items = new List<GYPFilterItem>() // No items for this filter
        },
        new GYPFilter
        {
            GYPFilterType = GYPFilterType.Bedrooms,
            Description = "Bedrooms",
            Items = new List<GYPFilterItem>
            {
                new GYPFilterItem { Id = 1, Description = "1 bedroom" },
                new GYPFilterItem { Id = 2, Description = "2 bedrooms" },
                new GYPFilterItem { Id = 3, Description = "3 bedrooms" }
            }
        },
        new GYPFilter
        {
            GYPFilterType = GYPFilterType.Bathrooms,
            Description = "Bathrooms",
            Items = new List<GYPFilterItem>
            {
                new GYPFilterItem { Id = 1, Description = "1 bathroom" },
                new GYPFilterItem { Id = 2, Description = "2 bathrooms" },
                new GYPFilterItem { Id = 3, Description = "3 bathrooms" }
            }
        },
        new GYPFilter
        {
            GYPFilterType = GYPFilterType.Garage,
            Description = "Garage",
            Items = new List<GYPFilterItem>
            {
                new GYPFilterItem { Id = 1, Description = "1 space" },
                new GYPFilterItem { Id = 2, Description = "2 spaces" },
                new GYPFilterItem { Id = 3, Description = "3 spaces" }
            }
        },
        new GYPFilter
        {
            GYPFilterType = GYPFilterType.PropertyStatus,
            Description = "Property Status",
            Items = new List<GYPFilterItem>
            {
                new GYPFilterItem { Id = 1, Description = "New" },
                new GYPFilterItem { Id = 2, Description = "Used" }
            }
        },
        new GYPFilter
        {
            GYPFilterType = GYPFilterType.Furnished,
            Description = "Furnished",
            Items = new List<GYPFilterItem>
            {
                new GYPFilterItem { Id = 1, Description = "Yes" },
                new GYPFilterItem { Id = 2, Description = "No" }
            }
        },
    };
        }
    }
}
