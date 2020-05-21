namespace MT.OnlineRestaurant.BusinessEntity
{
    public class RestaurantRating
    {
        public int RestaurantId { get; set; }
        public string rating { get; set; }
        public string user_Comments { get; set; }
        public int customerId { get; set; }

    }
}
