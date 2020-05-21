using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using MT.OnlineRestaurant.BusinessEntity;
using MT.OnlineRestaurant.BusinessLayer;

namespace MT.OnlineRestaurant.SearchManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : Controller
    {
        private readonly IRestaurantBusiness business_Repo;
        public SearchController(IRestaurantBusiness _business_Repo)
        {
            business_Repo = _business_Repo;
        }
        [HttpGet]
        [Route("ResturantDetail")]
        public RestaurantInformation GetResturantDetail([FromQuery] int RestaurantID)
        {
            RestaurantInformation resturantInformation = new RestaurantInformation();
            resturantInformation = business_Repo.GetResturantDetails(RestaurantID);
            return resturantInformation;
        }

        [HttpGet]
        [Route("ResturantMenuDetail")]
        public IActionResult GetResturantMenuDetail([FromQuery] int RestaurantID)
        {
            IQueryable<RestaurantMenu> restaurantMenuDetails;
            restaurantMenuDetails = business_Repo.GetRestaurantMenus(RestaurantID);
            if (restaurantMenuDetails != null)
            {
                return this.Ok(restaurantMenuDetails);
            }
            return this.StatusCode((int)HttpStatusCode.InternalServerError, string.Empty);
        }

        [HttpGet]
        [Route("ResturantRating")]
        public IActionResult GetResturantRating([FromQuery] int RestaurantID)
        {
            IQueryable<RestaurantRating> restaurantRatings;
            restaurantRatings = business_Repo.GetRestaurantRating(RestaurantID);
            if (restaurantRatings != null)
            {
                return this.Ok(restaurantRatings);
            }

            return this.StatusCode((int)HttpStatusCode.InternalServerError, string.Empty);
        }

        [HttpPost]
        [Route("ResturantRating")]
        public IActionResult ResturantRating([FromQuery] RestaurantRating restaurantRating)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            business_Repo.RestaurantRating(restaurantRating);

            return this.Ok("Submitted the reviewes");
        }

        //[HttpGet]
        //[Route("SearchRestaurant")]

        //public IActionResult RestaurantSearch(SearchForRestaurant search)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return this.BadRequest();
        //    } 
        //    business
        //}



        [HttpGet]
        [Route("ResturantTable")]
        public IActionResult GetResturantTableDetails([FromQuery] int RestaurantID)
        {
            IQueryable<RestaurantTables> restaurant_TableDetails;
            restaurant_TableDetails = business_Repo.GetTableDetails(RestaurantID);
            if (restaurant_TableDetails != null)
            {
                return this.Ok(restaurant_TableDetails);
            }
            return this.StatusCode((int)HttpStatusCode.InternalServerError, string.Empty);
        }

        [HttpPost]
        [Route("SearchRestaurantBasedOnDistance")]
        public IActionResult SearchRestaurantBasedOnDistance([FromBody] LocationDetails locationDetails)
        {
            IQueryable<RestaurantInformation> restaurantDetails;
            restaurantDetails = business_Repo.SearchRestaurantByLocation(locationDetails);
            if (restaurantDetails != null)
            {
                return this.Ok(restaurantDetails);
            }
            return this.StatusCode((int)HttpStatusCode.InternalServerError, string.Empty);

        }

        [HttpPost]
        [Route("SearchRestaurantBasedOnMenu")]
        public IActionResult SearchRestaurantBasedOnMenu([FromBody] AdditionalFeatureForSearch additionalFeatureForSearch)
        {
            IQueryable<RestaurantInformation> restaurantDetails;
            restaurantDetails = business_Repo.GetRestaurantsBasedOnMenu(additionalFeatureForSearch);
            if (restaurantDetails != null)
            {
                return this.Ok(restaurantDetails);
            }
            return this.StatusCode((int)HttpStatusCode.InternalServerError, string.Empty);

        }

        [HttpPost]
        [Route("SearchForRestaurant")]
        public IActionResult SearchForRestaurant([FromBody] SearchForRestaurant searchDetails)
        {
            List<RestaurantInformation> restaurantDetails;
            restaurantDetails = business_Repo.SearchForRestaurant(searchDetails);
            if (restaurantDetails != null)
            {
                return this.Ok(restaurantDetails);
            }
            return this.StatusCode((int)HttpStatusCode.InternalServerError, string.Empty);

        }
        [HttpGet]
        [Route("OrderDetail")]
        public IActionResult OrderDetail([FromQuery]int restaurantID, int menuID)
        {
            int query_result = business_Repo.ItemInStock(restaurantID, menuID);
            if (query_result > 0)
            {
                return Ok(restaurantID);
            }
            return this.StatusCode((int)HttpStatusCode.InternalServerError, "error");
        }
    }

}