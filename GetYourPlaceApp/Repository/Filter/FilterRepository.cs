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
            GYPPropertyInfo = GYPPropertyInfo.PropertyType,
            Description = "Property Type",
            Items = new List<GYPPropertyInfoItem>
            {
                new GYPPropertyInfoItem { Id = 1, Description = "Apartment" , GYPPropertyInfo = GYPPropertyInfo.PropertyType,},
                new GYPPropertyInfoItem { Id = 2, Description = "House" , GYPPropertyInfo = GYPPropertyInfo.PropertyType },
                new GYPPropertyInfoItem { Id = 3, Description = "Site" , GYPPropertyInfo = GYPPropertyInfo.PropertyType}
            }
        },
        new GYPFilter
        {
            GYPPropertyInfo = GYPPropertyInfo.City,
            Description = "City",
            Items = new List<GYPPropertyInfoItem>
            {
                new GYPPropertyInfoItem { Id = 1, Description = "São Paulo" ,  GYPPropertyInfo = GYPPropertyInfo.City },
                new GYPPropertyInfoItem { Id = 2, Description = "Rio de Janeiro" ,  GYPPropertyInfo = GYPPropertyInfo.City },
                new GYPPropertyInfoItem { Id = 3, Description = "Belo Horizonte" ,  GYPPropertyInfo = GYPPropertyInfo.City }
            }
        },
        new GYPFilter
        {
            GYPPropertyInfo = GYPPropertyInfo.Bedrooms,
            Description = "Bedrooms",
            Items = new List<GYPPropertyInfoItem>
            {
                new GYPPropertyInfoItem { Id = 1, Description = "1 bedroom", GYPPropertyInfo = GYPPropertyInfo.Bedrooms },
                new GYPPropertyInfoItem { Id = 2, Description = "2 bedrooms", GYPPropertyInfo = GYPPropertyInfo.Bedrooms },
                new GYPPropertyInfoItem { Id = 3, Description = "3 bedrooms" , GYPPropertyInfo = GYPPropertyInfo.Bedrooms }
            }
        },
        new GYPFilter
        {
            GYPPropertyInfo = GYPPropertyInfo.Bathrooms,
            Description = "Bathrooms",
            Items = new List<GYPPropertyInfoItem>
            {
                new GYPPropertyInfoItem { Id = 1, Description = "1 bathroom" , GYPPropertyInfo = GYPPropertyInfo.Bathrooms },
                new GYPPropertyInfoItem { Id = 2, Description = "2 bathrooms" , GYPPropertyInfo = GYPPropertyInfo.Bathrooms},
                new GYPPropertyInfoItem { Id = 3, Description = "3 bathrooms" , GYPPropertyInfo = GYPPropertyInfo.Bathrooms }
            }
        },
        new GYPFilter
        {
            GYPPropertyInfo = GYPPropertyInfo.Garage,
            Description = "Garage",
            Items = new List<GYPPropertyInfoItem>
            {
                new GYPPropertyInfoItem { Id = 1, Description = "1 space" , GYPPropertyInfo = GYPPropertyInfo.Garage  },
                new GYPPropertyInfoItem { Id = 2, Description = "2 spaces" , GYPPropertyInfo = GYPPropertyInfo.Garage  },
                new GYPPropertyInfoItem { Id = 3, Description = "3 spaces" , GYPPropertyInfo = GYPPropertyInfo.Garage   }
            }
        },
        new GYPFilter
        {
            GYPPropertyInfo = GYPPropertyInfo.PropertyStatus,
            Description = "Property Status",
            Items = new List<GYPPropertyInfoItem>
            {
                new GYPPropertyInfoItem { Id = 1, Description = "New" , GYPPropertyInfo = GYPPropertyInfo.PropertyStatus },
                new GYPPropertyInfoItem { Id = 2, Description = "Used" , GYPPropertyInfo = GYPPropertyInfo.PropertyStatus}
            }
        },
        new GYPFilter
        {
            GYPPropertyInfo = GYPPropertyInfo.Furnished,
            Description = "Furnished",
            Items = new List<GYPPropertyInfoItem>
            {
                new GYPPropertyInfoItem { Id = 1, Description = "Furnished" , GYPPropertyInfo = GYPPropertyInfo.Furnished },
                new GYPPropertyInfoItem { Id = 2, Description = "Not Furnished" , GYPPropertyInfo = GYPPropertyInfo.Furnished}
            }
        },
    };
        }
    }
}
