﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using MT.OnlineRestaurant.DataLayer.DataEntity;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;

namespace MT.OnlineRestaurant.DataLayer.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly RestaurantManagementContext db;
        public SearchRepository(RestaurantManagementContext connection)
        {
            db = connection;
        }

        #region Interface Methods
        public IQueryable<MenuDetails> GetRestaurantMenu(int restaurantID)
        {
            List<MenuDetails> menudetails = new List<MenuDetails>();
            try
            {
                if (db != null)
                {
                    var menudetail = (from offer in db.TblOffer
                                      join menu in db.TblMenu
                                      on offer.TblMenuId equals menu.Id into TableMenu
                                      from menu in TableMenu.ToList()
                                      join cuisine in db.TblCuisine on menu.TblCuisineId equals cuisine.Id
                                      where offer.TblRestaurantId == restaurantID
                                      select new MenuDetails
                                      {
                                          tbl_Offer = offer,
                                          tbl_Cuisine = cuisine,
                                          tbl_Menu = menu

                                      }).ToList();
                    foreach (var item in menudetail)
                    {
                        MenuDetails menuitem = new MenuDetails
                        {
                            tbl_Cuisine = item.tbl_Cuisine,
                            tbl_Menu = item.tbl_Menu,
                            tbl_Offer = item.tbl_Offer
                        };
                        menudetails.Add(menuitem);
                    }
                }
                return menudetails.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TblRating> GetRestaurantRating(int restaurantID)
        {
            // List<TblRating> restaurant_Rating = new List<TblRating>();
            try
            {
                if (db != null)
                {
                    return (from rating in db.TblRating
                            join restaurant in db.TblRestaurant on
                            rating.TblRestaurantId equals restaurant.Id
                            where rating.TblRestaurantId == restaurantID
                            select new TblRating
                            {
                                Rating = rating.Rating,
                                Comments = rating.Comments,
                                TblRestaurant = restaurant,
                            }).AsQueryable();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TblRestaurant GetResturantDetails(int restaurantID)
        {
            TblRestaurant resturantInformation = new TblRestaurant();

            try
            {
                if (db != null)
                {
                    resturantInformation = (from restaurant in db.TblRestaurant
                                            join location in db.TblLocation on restaurant.TblLocationId equals location.Id
                                            where restaurant.Id == restaurantID
                                            select new TblRestaurant
                                            {
                                                Id = restaurant.Id,
                                                Name = restaurant.Name,
                                                Address = restaurant.Address,
                                                ContactNo = restaurant.ContactNo,
                                                TblLocation = location,
                                                CloseTime = restaurant.CloseTime,
                                                OpeningTime = restaurant.OpeningTime,
                                                Website = restaurant.Website
                                            }).FirstOrDefault();

                }

                return resturantInformation;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public IQueryable<TblRestaurantDetails> GetTableDetails(int restaurantID)
        {
            try
            {
                if (db != null)
                {
                    return (from restaurantDetails in db.TblRestaurantDetails
                            join restaurant in db.TblRestaurant
                            on restaurantDetails.TblRestaurantId equals restaurant.Id
                            where restaurantDetails.TblRestaurantId == restaurantID
                            select new TblRestaurantDetails
                            {
                                TableCapacity = restaurantDetails.TableCapacity,
                                TableCount = restaurantDetails.TableCount,
                                TblRestaurant = restaurant
                            }).AsQueryable();

                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<RestaurantSearchDetails> GetRestaurantsBasedOnLocation(LocationDetails location_Details)
        {
            List<RestaurantSearchDetails> restaurants = new List<RestaurantSearchDetails>();
            try
            {
                restaurants = GetRetaurantBasedOnLocationAndName(location_Details);
                return restaurants.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IQueryable<RestaurantSearchDetails> GetRestaurantsBasedOnMenu(AddtitionalFeatureForSearch searchDetails)
        {
            List<RestaurantSearchDetails> restaurants = new List<RestaurantSearchDetails>();
            try
            {
                restaurants = GetRestaurantDetailsBasedOnRating(searchDetails);
                return restaurants.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IQueryable<RestaurantSearchDetails> SearchForRestaurant(SearchForRestautrant searchDetails)
        {
            List<RestaurantSearchDetails> searchedRestaurantBasedOnRating = new List<RestaurantSearchDetails>();
            List<RestaurantSearchDetails> restaurantsBasedOnLocation = new List<RestaurantSearchDetails>();

            TotalSearchForRestaurant(searchDetails);
            if (searchDetails.search != null)
            {
                searchedRestaurantBasedOnRating = GetRestaurantDetailsBasedOnRating(searchDetails.search);
            }
            if (searchDetails.location != null)
            {

                restaurantsBasedOnLocation = GetRetaurantBasedOnLocationAndName(searchDetails.location);
            }
            List<RestaurantSearchDetails> restaurantInfo = new List<RestaurantSearchDetails>();
            restaurantInfo = restaurantsBasedOnLocation.Union(searchedRestaurantBasedOnRating).ToList<RestaurantSearchDetails>();

            return restaurantInfo.AsQueryable();
        }

        public IQueryable<RestaurantSearchDetails> TotalSearchForRestaurant(SearchForRestautrant searchDetails)
        {
            List<RestaurantSearchDetails> restaurants = new List<RestaurantSearchDetails>();
            try
            {
                var res = db.TblRestaurant.Include(x => x.TblRating);

                var restaurantFilter = (from restaurant in res
                                        join location in db.TblLocation on restaurant.TblLocationId equals location.Id
                                        select new { TblRestaurant = restaurant, TblLocation = location });


                if (searchDetails.search != null)
                {
                    if (!string.IsNullOrEmpty(searchDetails.search.cuisine))
                    {
                        restaurantFilter = (from filteredRestaurant in restaurantFilter
                                            join offer in db.TblOffer on filteredRestaurant.TblRestaurant.Id equals offer.TblRestaurantId
                                            join menu in db.TblMenu on offer.TblMenuId equals menu.Id
                                            join cuisine in db.TblCuisine on menu.TblCuisineId equals cuisine.Id
                                            where cuisine.Cuisine.Contains(searchDetails.search.cuisine)
                                            select filteredRestaurant).Distinct();
                    }
                    if (!string.IsNullOrEmpty(searchDetails.search.Menu))
                    {
                        restaurantFilter = (from filteredRestaurant in restaurantFilter
                                            join offer in db.TblOffer on filteredRestaurant.TblRestaurant.Id equals offer.TblRestaurantId
                                            join menu in db.TblMenu on offer.TblMenuId equals menu.Id
                                            where menu.Item.Contains(searchDetails.search.Menu) 
                                            select filteredRestaurant).Distinct();
                    }

                    if (searchDetails.search.budget>0)
                        
                    {
                        restaurantFilter=(from filteredRestaurant in restaurantFilter
                                            join offer in db.TblOffer on filteredRestaurant.TblRestaurant.Id equals offer.TblRestaurantId
                                            join menu in db.TblMenu on offer.TblMenuId equals menu.Id
                                            where offer.Price>searchDetails.search.budget
                                            select filteredRestaurant).Distinct();
                    }
                    if (searchDetails.search.rating > 0)
                    {
                        restaurantFilter = (from filteredRestaurant in restaurantFilter
                                            join rating in db.TblRating on filteredRestaurant.TblRestaurant.Id equals rating.TblRestaurantId
                                            where rating.Rating.Contains(searchDetails.search.rating.ToString())
                                            select filteredRestaurant).Distinct();
                    }
                }
                if (searchDetails.location != null)
                {
                    if (!string.IsNullOrEmpty(searchDetails.location.restaurant_Name))
                    {
                        restaurantFilter = restaurantFilter.Where(a => a.TblRestaurant.Name.Contains(searchDetails.location.restaurant_Name));

                    }

                    if (!(searchDetails.location.xaxis <= 0) || (searchDetails.location.yaxis < 0))
                    {
                        foreach (var place in restaurantFilter)
                        {
                            double distance = Distance(searchDetails.location.xaxis, searchDetails.location.yaxis, (double)place.TblLocation.X, (double)place.TblLocation.Y);
                            if (distance < int.Parse(searchDetails.location.distance.ToString()))
                            {
                                RestaurantSearchDetails tblRestaurant = new RestaurantSearchDetails
                                {
                                    restauran_ID = place.TblRestaurant.Id,
                                    restaurant_Name = place.TblRestaurant.Name,
                                    restaurant_Address = place.TblRestaurant.Address,
                                    restaurant_ContactNo = place.TblRestaurant.ContactNo,
                                    website = place.TblRestaurant.Website,
                                    closing_Time = place.TblRestaurant.CloseTime,
                                    opening_Time = place.TblRestaurant.OpeningTime,
                                    xaxis = (double)place.TblLocation.X,
                                    yaxis = (double)place.TblLocation.Y
                                };
                                try
                                {
                                    tblRestaurant.rating = place.TblRestaurant.TblRating.Average(x => Convert.ToDecimal(x.Rating));
                                }
                                catch
                                {
                                    tblRestaurant.rating = 0;
                                }
                                restaurants.Add(tblRestaurant);
                            }
                        }

                    }
                    else
                    {
                        foreach (var item in restaurantFilter)
                        {
                            RestaurantSearchDetails tblRestaurant = new RestaurantSearchDetails
                            {
                                restauran_ID = item.TblRestaurant.Id,
                                restaurant_Name = item.TblRestaurant.Name,
                                restaurant_Address = item.TblRestaurant.Address,
                                restaurant_ContactNo = item.TblRestaurant.ContactNo,
                                website = item.TblRestaurant.Website,
                                closing_Time = item.TblRestaurant.CloseTime,
                                opening_Time = item.TblRestaurant.OpeningTime,
                                xaxis = (double)item.TblLocation.X,
                                yaxis = (double)item.TblLocation.Y

                            };

                            try
                            {
                                tblRestaurant.rating = item.TblRestaurant.TblRating.Average(x => Convert.ToDecimal(x.Rating));
                            }
                            catch
                            {
                                tblRestaurant.rating = 0;
                            }
                            restaurants.Add(tblRestaurant);
                        }
                    }

                }
                return restaurants.DistinctBy(x => x.restaurant_Name).AsQueryable().OrderByDescending(x=>x.rating);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Recording the customer rating the restaurants
        /// </summary>
        /// <param name="tblRating"></param>
        public void RestaurantRating(TblRating tblRating)
        {
            //tblRating.UserCreated = ,
            //tblRating.UserModified=,
            tblRating.RecordTimeStampCreated = DateTime.Now;

            db.Set<TblRating>().Add(tblRating);
            db.SaveChanges();

        }
        public TblMenu ItemInStock(int restaurantID, int menuID)
        {
            try
            {
                TblMenu menuObj = new TblMenu();
                if (db != null)
                {
                    //    menuObj = (from m in db.TblMenu
                    //               join offer in db.TblOffer on m.Id equals offer.TblMenuId
                    //               join restaurant in db.TblRestaurantDetails on offer.TblRestaurantId equals restaurant.TblRestaurantId
                    //               where restaurant.TblRestaurantId == restaurantID && m.Id == menuID
                    //               select new TblMenu
                    //               {
                    //                   quantity = m.quantity
                    //               }).FirstOrDefault();                   
                    //}
                    menuObj = (from offer in db.TblOffer
                               join menu in db.TblMenu
                               on offer.TblMenuId equals menu.Id
                               join rest in db.TblRestaurantDetails
                               on offer.TblRestaurantId equals rest.TblRestaurantId
                               where rest.TblRestaurantId == restaurantID && menu.Id == menuID
                               select new TblMenu
                               {
                                   quantity = menu.quantity
                               }).FirstOrDefault();
                }
                return menuObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region private methods
        private List<RestaurantSearchDetails> GetRestaurantDetailsBasedOnRating(AddtitionalFeatureForSearch searchList)
        {
            List<RestaurantSearchDetails> restaurants = new List<RestaurantSearchDetails>();
            try
            {

                var restaurantFilter = (from restaurant in db.TblRestaurant
                                        join location in db.TblLocation on restaurant.TblLocationId equals location.Id
                                        select new { TblRestaurant = restaurant, TblLocation = location });
                restaurantFilter = (from filteredRestaurant in restaurantFilter
                                    join rating in db.TblRating on filteredRestaurant.TblRestaurant.Id equals rating.TblRestaurantId
                                    orderby rating descending
                                    select filteredRestaurant);


                if (!string.IsNullOrEmpty(searchList.cuisine))
                {
                    restaurantFilter = (from filteredRestaurant in restaurantFilter
                                        join offer in db.TblOffer on filteredRestaurant.TblRestaurant.Id equals offer.TblRestaurantId
                                        join menu in db.TblMenu on offer.TblMenuId equals menu.Id
                                        join cuisine in db.TblCuisine on menu.TblCuisineId equals cuisine.Id
                                        where cuisine.Cuisine.Contains(searchList.cuisine)
                                        select filteredRestaurant).Distinct();
                }
                if (!string.IsNullOrEmpty(searchList.Menu))
                {
                    restaurantFilter = (from filteredRestaurant in restaurantFilter
                                        join offer in db.TblOffer on filteredRestaurant.TblRestaurant.Id equals offer.TblRestaurantId
                                        join menu in db.TblMenu on offer.TblMenuId equals menu.Id
                                        where menu.Item.Contains(searchList.Menu)
                                        select filteredRestaurant).Distinct();
                }

                if (searchList.rating > 0)
                {
                    restaurantFilter = (from filteredRestaurant in restaurantFilter
                                        join rating in db.TblRating on filteredRestaurant.TblRestaurant.Id equals rating.TblRestaurantId
                                        where rating.Rating.Contains(searchList.rating.ToString())
                                        select filteredRestaurant).Distinct();
                }
                foreach (var item in restaurantFilter)
                {
                    RestaurantSearchDetails restaurant = new RestaurantSearchDetails
                    {
                        restauran_ID = item.TblRestaurant.Id,
                        restaurant_Name = item.TblRestaurant.Name,
                        restaurant_Address = item.TblRestaurant.Address,
                        restaurant_ContactNo = item.TblRestaurant.ContactNo,
                        website = item.TblRestaurant.Website,
                        closing_Time = item.TblRestaurant.CloseTime,
                        opening_Time = item.TblRestaurant.OpeningTime,
                        xaxis = (double)item.TblLocation.X,
                        yaxis = (double)item.TblLocation.Y
                    };
                    restaurants.Add(restaurant);
                }

                return restaurants;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<RestaurantSearchDetails> GetRetaurantBasedOnLocationAndName(LocationDetails location_Details)
        {
            List<RestaurantSearchDetails> restaurants = new List<RestaurantSearchDetails>();
            try
            {

                var restaurantInfo = (from restaurant in db.TblRestaurant
                                      join location in db.TblLocation on restaurant.TblLocationId equals location.Id
                                      select new { TblRestaurant = restaurant, TblLocation = location });
                restaurantInfo = (from filteredRestaurant in restaurantInfo
                                  join rating in db.TblRating on filteredRestaurant.TblRestaurant.Id equals rating.TblRestaurantId
                                  orderby rating descending
                                  select filteredRestaurant);

                if (!string.IsNullOrEmpty(location_Details.restaurant_Name))
                {
                    restaurantInfo = restaurantInfo.Where(a => a.TblRestaurant.Name.Contains(location_Details.restaurant_Name));

                }

                if (!(location_Details.xaxis <= 0) || (location_Details.yaxis < 0))
                {
                    foreach (var place in restaurantInfo)
                    {
                        double distance = Distance(location_Details.xaxis, location_Details.yaxis, (double)place.TblLocation.X, (double)place.TblLocation.Y);
                        if (distance < int.Parse(location_Details.distance.ToString()))
                        {
                            RestaurantSearchDetails tblRestaurant = new RestaurantSearchDetails
                            {
                                restauran_ID = place.TblRestaurant.Id,
                                restaurant_Name = place.TblRestaurant.Name,
                                restaurant_Address = place.TblRestaurant.Address,
                                restaurant_ContactNo = place.TblRestaurant.ContactNo,
                                website = place.TblRestaurant.Website,
                                closing_Time = place.TblRestaurant.CloseTime,
                                opening_Time = place.TblRestaurant.OpeningTime,
                                xaxis = (double)place.TblLocation.X,
                                yaxis = (double)place.TblLocation.Y
                            };
                            restaurants.Add(tblRestaurant);
                        }
                    }

                }
                else
                {
                    foreach (var item in restaurantInfo)
                    {
                        RestaurantSearchDetails tblRestaurant = new RestaurantSearchDetails
                        {
                            restauran_ID = item.TblRestaurant.Id,
                            restaurant_Name = item.TblRestaurant.Name,
                            restaurant_Address = item.TblRestaurant.Address,
                            restaurant_ContactNo = item.TblRestaurant.ContactNo,
                            website = item.TblRestaurant.Website,
                            closing_Time = item.TblRestaurant.CloseTime,
                            opening_Time = item.TblRestaurant.OpeningTime,
                            xaxis = (double)item.TblLocation.X,
                            yaxis = (double)item.TblLocation.Y

                        };
                        restaurants.Add(tblRestaurant);
                    }
                }
                return restaurants;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private double Distance(double currentLatitude, double currentLongitude, double latitude, double longitude)
        {
            if (currentLatitude == latitude && currentLongitude == longitude) { return 0; }
            double theta = currentLongitude - longitude;
            double dist = Math.Sin(deg2rad(currentLatitude)) * Math.Sin(deg2rad(latitude)) + Math.Cos(deg2rad(currentLatitude)) * Math.Cos(deg2rad(latitude)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = (dist * 60 * 1.1515) / 0.6213711922;          //miles to kms
            return (dist);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }


        //public IEnumerable<TblRestaurant> SearchRest(string name, string cuisine, string rating, string X, string Y, string ditance, string menu)
        //{


        //}
        #endregion
    }
}
