namespace MT.OnlineRestaurant.DataLayer.DataEntity
{
    public class RestaurantSearchDetails
    {
        public int restauran_ID { get; set; }
        public string restaurant_Name { get; set; }
        public string restaurant_Address { get; set; }
        public string restaurant_ContactNo { get; set; }
        public string website { get; set; }
        public string opening_Time { get; set; }
        public string closing_Time { get; set; }
        public double xaxis { get; set; }
        public double yaxis { get; set; }
        public decimal rating { get; set; }

        public decimal budget { get; set; }


    }
}
