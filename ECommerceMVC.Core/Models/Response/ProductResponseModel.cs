namespace ECommerceMVC.Core.Models.Response
{
    public class ProductResponseModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public decimal UnitPrice { get; set; }
        public int TotalSold { get; set; }
    }
}
