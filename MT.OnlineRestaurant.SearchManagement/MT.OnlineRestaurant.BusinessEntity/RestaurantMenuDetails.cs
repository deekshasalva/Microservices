using System.Collections.Generic;

namespace MT.OnlineRestaurant.BusinessEntity
{
    public class RestaurantMenuDetails
    {
        public string cuisine { get; set; }
        public List<RestaurantMenu> menuList { get; set; }
    }
}
