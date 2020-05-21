﻿using System.Linq;
using MT.OnlineRestaurant.DataLayer.DataEntity;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;

namespace MT.OnlineRestaurant.DataLayer.Repository
{
    public interface ISearchRepository
    {
        TblRestaurant GetResturantDetails(int restaurantID);
        IQueryable<TblRating> GetRestaurantRating(int restaurantID);

        IQueryable<MenuDetails> GetRestaurantMenu(int restaurantID);

        IQueryable<TblRestaurantDetails> GetTableDetails(int restaurantID);
        IQueryable<RestaurantSearchDetails> GetRestaurantsBasedOnLocation(LocationDetails location_Details);
        IQueryable<RestaurantSearchDetails> GetRestaurantsBasedOnMenu(AddtitionalFeatureForSearch searchDetails);
        IQueryable<RestaurantSearchDetails> SearchForRestaurant(SearchForRestautrant searchDetails);

        public IQueryable<RestaurantSearchDetails> TotalSearchForRestaurant(SearchForRestautrant searchDetails);

        /// <summary>
        /// Recording the customer rating the restaurants
        /// </summary>
        /// <param name="tblRating"></param>
        void RestaurantRating(TblRating tblRating);
        TblMenu ItemInStock(int restaurantID,int MenuID);

    }
}
