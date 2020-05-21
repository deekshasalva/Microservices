namespace MT.OnlineRestaurant.BusinessEntity
{
    public class RestaurantMenu
    {
        public int menu_ID { get; set; }
        public string dish_Name { get; set; }
        public decimal price { get; set; }
        public int running_Offer { get; set; }
        public string cuisine { get; set; }
        public int quantity { get; set; }
    }
    
}
