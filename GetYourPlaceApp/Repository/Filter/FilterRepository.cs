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
                new GYPFilterItem { Id = 1, Description = "Apartment" , GYPFilterType = GYPFilterType.PropertyType,},
                new GYPFilterItem { Id = 2, Description = "House" , GYPFilterType = GYPFilterType.PropertyType },
                new GYPFilterItem { Id = 3, Description = "Site" , GYPFilterType = GYPFilterType.PropertyType}
            }
        },
        new GYPFilter
        {
            GYPFilterType = GYPFilterType.City,
            Description = "City",
            Items = new List<GYPFilterItem>
            {
                new GYPFilterItem { Id = 1, Description = "São Paulo" ,  GYPFilterType = GYPFilterType.City },
                new GYPFilterItem { Id = 2, Description = "Rio de Janeiro" ,  GYPFilterType = GYPFilterType.City },
                new GYPFilterItem { Id = 3, Description = "Belo Horizonte" ,  GYPFilterType = GYPFilterType.City }
            }
        },
        new GYPFilter
        {
            GYPFilterType = GYPFilterType.Bedrooms,
            Description = "Bedrooms",
            Items = new List<GYPFilterItem>
            {
                new GYPFilterItem { Id = 1, Description = "1 bedroom", GYPFilterType = GYPFilterType.Bedrooms },
                new GYPFilterItem { Id = 2, Description = "2 bedrooms", GYPFilterType = GYPFilterType.Bedrooms },
                new GYPFilterItem { Id = 3, Description = "3 bedrooms" , GYPFilterType = GYPFilterType.Bedrooms }
            }
        },
        new GYPFilter
        {
            GYPFilterType = GYPFilterType.Bathrooms,
            Description = "Bathrooms",
            Items = new List<GYPFilterItem>
            {
                new GYPFilterItem { Id = 1, Description = "1 bathroom" , GYPFilterType = GYPFilterType.Bathrooms },
                new GYPFilterItem { Id = 2, Description = "2 bathrooms" , GYPFilterType = GYPFilterType.Bathrooms},
                new GYPFilterItem { Id = 3, Description = "3 bathrooms" , GYPFilterType = GYPFilterType.Bathrooms }
            }
        },
        new GYPFilter
        {
            GYPFilterType = GYPFilterType.Garage,
            Description = "Garage",
            Items = new List<GYPFilterItem>
            {
                new GYPFilterItem { Id = 1, Description = "1 space" , GYPFilterType = GYPFilterType.Garage  },
                new GYPFilterItem { Id = 2, Description = "2 spaces" , GYPFilterType = GYPFilterType.Garage  },
                new GYPFilterItem { Id = 3, Description = "3 spaces" , GYPFilterType = GYPFilterType.Garage   }
            }
        },
        new GYPFilter
        {
            GYPFilterType = GYPFilterType.PropertyStatus,
            Description = "Property Status",
            Items = new List<GYPFilterItem>
            {
                new GYPFilterItem { Id = 1, Description = "New" , GYPFilterType = GYPFilterType.PropertyStatus },
                new GYPFilterItem { Id = 2, Description = "Used" , GYPFilterType = GYPFilterType.PropertyStatus}
            }
        },
        new GYPFilter
        {
            GYPFilterType = GYPFilterType.Furnished,
            Description = "Furnished",
            Items = new List<GYPFilterItem>
            {
                new GYPFilterItem { Id = 1, Description = "Yes" , GYPFilterType = GYPFilterType.Furnished },
                new GYPFilterItem { Id = 2, Description = "No" , GYPFilterType = GYPFilterType.Furnished}
            }
        },
    };
        }
    }
}
