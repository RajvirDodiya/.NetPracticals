namespace Product_Catelog.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ProductCategory { get; set; }
        public string ImageUrl { get; set; }
    }
}