using GetYourPlaceApp.Managers;
using GetYourPlaceApp.Models;
using GetYourPlaceApp.Models.Enums;

namespace GetYourPlaceApp.Repository.Properties
{
    public class PropertiesRepository : IPropertiesRepository
    {
        public async Task<List<Propertie>> GetProperties()
        {
			try
			{
                // Mocking the TypeOfRent objects
                var typeOfRentRental = new GYPTypeOfRent
                {
                    Id = 1,
                    Description = "Aluguel"
                };

                var typeOfRentSell = new GYPTypeOfRent
                {
                    Id = 2,
                    Description = "Venda"
                };

                // Mocking the properties data
                var propertiesData = new List<Propertie>
                     {
                         new Propertie
                {
                    Id = 1,
                    GYPUserProfileId = 101,
                    Description = "Cozy apartment in the city",
                    TypeOfRent = typeOfRentRental,
                    Price = 120000, // Note: Adjusting decimal values
                    IsVisibleForUsers = true,
                    IsAvailable = true,
                    ImageUrl = "assets/propertie1.jpg",
                    Address = "Rua 20 de Setembro, Rio de Janeiro",
                    Star = 4,
                    PropertyInformations = new List<GYPPropertyInfoItem>
                    {
                        new GYPPropertyInfoItem { Id = 1, Description = "Apartment" , GYPPropertyInfo = GYPPropertyInfo.PropertyType,},
                                        new GYPPropertyInfoItem { Id = 2, Description = "Rio de Janeiro" ,  GYPPropertyInfo = GYPPropertyInfo.City },
                                        new GYPPropertyInfoItem { Id = 3, Description = "3 bedrooms" , GYPPropertyInfo = GYPPropertyInfo.Bedrooms },
                                        new GYPPropertyInfoItem { Id = 1, Description = "1 bathroom" , GYPPropertyInfo = GYPPropertyInfo.Bathrooms },
                                        new GYPPropertyInfoItem { Id = 2, Description = "2 spaces" , GYPPropertyInfo = GYPPropertyInfo.Garage  },
                                        new GYPPropertyInfoItem { Id = 2, Description = "Used" , GYPPropertyInfo = GYPPropertyInfo.PropertyStatus},
                                        new GYPPropertyInfoItem { Id = 2, Description = "Not Furnished" , GYPPropertyInfo = GYPPropertyInfo.Furnished}
                    }                },
                         new Propertie
                {
                    Id = 2,
                    GYPUserProfileId = 102,
                    Description = "Spacious beach house",
                    TypeOfRent = typeOfRentSell,
                    Address = "Rua 20 de Setembro, Rio de Janeiro",
                    Price = 250000,
                    IsVisibleForUsers = true,
                    IsAvailable = false,
                    ImageUrl = "assets/propertie2.jpg",
                    Star = 5,
                    PropertyInformations = new List<GYPPropertyInfoItem>
                    {
                        new GYPPropertyInfoItem { Id = 1, Description = "Apartment" , GYPPropertyInfo = GYPPropertyInfo.PropertyType,},
                                        new GYPPropertyInfoItem { Id = 2, Description = "Rio de Janeiro" ,  GYPPropertyInfo = GYPPropertyInfo.City },
                                        new GYPPropertyInfoItem { Id = 2, Description = "2 bedrooms" , GYPPropertyInfo = GYPPropertyInfo.Bedrooms },
                                        new GYPPropertyInfoItem { Id = 2, Description = "2 bathroom" , GYPPropertyInfo = GYPPropertyInfo.Bathrooms },
                                        new GYPPropertyInfoItem { Id = 3, Description = "3 spaces" , GYPPropertyInfo = GYPPropertyInfo.Garage  },
                                        new GYPPropertyInfoItem { Id = 1, Description = "New" , GYPPropertyInfo = GYPPropertyInfo.PropertyStatus},
                                        new GYPPropertyInfoItem { Id = 1, Description = "Furnished" , GYPPropertyInfo = GYPPropertyInfo.Furnished}
                    }    
                },
                         new Propertie
                {
                    Id = 3,
                    GYPUserProfileId = 103,
                    Description = "Modern loft with skyline views",
                    Price = 180000,
                    TypeOfRent = typeOfRentSell,
                    Address = "Zona Norte, São Paulo",
                    IsVisibleForUsers = true,
                    IsAvailable = true,
                    ImageUrl = "assets/propertie3.jpg",
                    Star = 3,
                    PropertyInformations = new List<GYPPropertyInfoItem>
                    {
                        new GYPPropertyInfoItem { Id = 1, Description = "Apartment" , GYPPropertyInfo = GYPPropertyInfo.PropertyType,},
                                        new GYPPropertyInfoItem { Id = 3, Description = "Belo Horizonte" ,  GYPPropertyInfo = GYPPropertyInfo.City },
                                        new GYPPropertyInfoItem { Id = 1, Description = "1 bedrooms" , GYPPropertyInfo = GYPPropertyInfo.Bedrooms },
                                        new GYPPropertyInfoItem { Id = 2, Description = "2 bathroom" , GYPPropertyInfo = GYPPropertyInfo.Bathrooms },
                                        new GYPPropertyInfoItem { Id = 2, Description = "2 spaces" , GYPPropertyInfo = GYPPropertyInfo.Garage  },
                                        new GYPPropertyInfoItem { Id = 2, Description = "Used" , GYPPropertyInfo = GYPPropertyInfo.PropertyStatus},
                                        new GYPPropertyInfoItem { Id = 2, Description = "Not Furnished" , GYPPropertyInfo = GYPPropertyInfo.Furnished}
                    }
                },
                         new Propertie
                {
                    Id = 4,
                    GYPUserProfileId = 104,
                    TypeOfRent = typeOfRentSell,
                    Address = "Charqueadas, São Paulo",
                    Description = "Charming cottage in the countryside",
                    Price = 150000,
                    IsVisibleForUsers = true,
                    IsAvailable = true,
                    ImageUrl = "assets/propertie4.jpg",
                    Star = 1,
                    PropertyInformations = new List<GYPPropertyInfoItem>
                    {
                        new GYPPropertyInfoItem { Id = 1, Description = "Apartment" , GYPPropertyInfo = GYPPropertyInfo.PropertyType,},
                                        new GYPPropertyInfoItem { Id = 2, Description = "São Paulo" ,  GYPPropertyInfo = GYPPropertyInfo.City },
                                        new GYPPropertyInfoItem { Id = 3, Description = "3 bedrooms" , GYPPropertyInfo = GYPPropertyInfo.Bedrooms },
                                        new GYPPropertyInfoItem { Id = 3, Description = "3 bathroom" , GYPPropertyInfo = GYPPropertyInfo.Bathrooms },
                                        new GYPPropertyInfoItem { Id = 3, Description = "3 spaces" , GYPPropertyInfo = GYPPropertyInfo.Garage  },
                                        new GYPPropertyInfoItem { Id = 2, Description = "Used" , GYPPropertyInfo = GYPPropertyInfo.PropertyStatus},
                                        new GYPPropertyInfoItem { Id = 2, Description = "No" , GYPPropertyInfo = GYPPropertyInfo.Furnished}
                    }
                },
                         new Propertie
                {
                    Id = 5,
                    GYPUserProfileId = 105,
                    TypeOfRent = typeOfRentRental,
                    Address = "Zona Norte, São Paulo",
                    Description = "Luxury villa with private pool",
                    Price = 400000,
                    IsVisibleForUsers = true,
                    IsAvailable = true,
                    ImageUrl = "assets/propertie5.jpg",
                    Star = 3,
                    PropertyInformations = new List<GYPPropertyInfoItem>
                    {
                        new GYPPropertyInfoItem { Id = 1, Description = "Apartment" , GYPPropertyInfo = GYPPropertyInfo.PropertyType,},
                                        new GYPPropertyInfoItem { Id = 2, Description = "São Paulo" ,  GYPPropertyInfo = GYPPropertyInfo.City },
                                        new GYPPropertyInfoItem { Id = 3, Description = "3 bedrooms" , GYPPropertyInfo = GYPPropertyInfo.Bedrooms },
                                        new GYPPropertyInfoItem { Id = 2, Description = "2 bathroom" , GYPPropertyInfo = GYPPropertyInfo.Bathrooms },
                                        new GYPPropertyInfoItem { Id = 2, Description = "2 spaces" , GYPPropertyInfo = GYPPropertyInfo.Garage  },
                                        new GYPPropertyInfoItem { Id = 2, Description = "Used" , GYPPropertyInfo = GYPPropertyInfo.PropertyStatus},
                                        new GYPPropertyInfoItem { Id = 2, Description = "No" , GYPPropertyInfo = GYPPropertyInfo.Furnished}
                    }
                },
                         new Propertie
                {
                    Id = 6,
                    GYPUserProfileId = 106,
                    TypeOfRent = typeOfRentRental,
                    Address = "Zona Sul, São Paulo",
                    Description = "Bright studio apartment near downtown",
                    Price = 100000,
                    IsVisibleForUsers = true,
                    IsAvailable = true,
                    ImageUrl = "assets/propertie6.jpg",
                    Star = 4,
                    PropertyInformations = new List<GYPPropertyInfoItem>
                    {
                        new GYPPropertyInfoItem { Id = 1, Description = "Apartment" , GYPPropertyInfo = GYPPropertyInfo.PropertyType,},
                                        new GYPPropertyInfoItem { Id = 2, Description = "Belo Horizonte" ,  GYPPropertyInfo = GYPPropertyInfo.City },
                                        new GYPPropertyInfoItem { Id = 1, Description = "1 bedrooms" , GYPPropertyInfo = GYPPropertyInfo.Bedrooms },
                                        new GYPPropertyInfoItem { Id = 1, Description = "1 bathroom" , GYPPropertyInfo = GYPPropertyInfo.Bathrooms },
                                        new GYPPropertyInfoItem { Id = 1, Description = "1 spaces" , GYPPropertyInfo = GYPPropertyInfo.Garage  },
                                        new GYPPropertyInfoItem { Id = 1, Description = "New" , GYPPropertyInfo = GYPPropertyInfo.PropertyStatus},
                                        new GYPPropertyInfoItem { Id = 1, Description = "Furnished" , GYPPropertyInfo = GYPPropertyInfo.Furnished}
                    }
                }
                     };

                foreach(var filter in FilterManager.Instance.GetFilters())
                {
                   foreach(var propertie in propertiesData)
                    {
                        var found = propertie.PropertyInformations.FirstOrDefault(
                            p => p.GYPPropertyInfo == filter.GYPPropertyInfo &&
                            p.Description == filter.Description);

                        if(found is null)
                        {
                            propertiesData.Remove(propertie);
                            continue;
                        }
                    }
                }

                return propertiesData;

            }
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
        }
    }
}
