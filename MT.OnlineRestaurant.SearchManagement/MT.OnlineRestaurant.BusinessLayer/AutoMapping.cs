using AutoMapper;
using MT.OnlineRestaurant.BusinessEntity;
using MT.OnlineRestaurant.DataLayer.DataEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<BusinessEntity.LocationDetails, DataLayer.DataEntity.LocationDetails>().ReverseMap();
            CreateMap<BusinessEntity.AdditionalFeatureForSearch, DataLayer.DataEntity.AddtitionalFeatureForSearch>().ReverseMap();
            CreateMap<SearchForRestaurant, DataLayer.DataEntity.SearchForRestautrant>().ReverseMap();
            
            CreateMap<RestaurantInformation, RestaurantSearchDetails>().ReverseMap();
        }
    }
}
