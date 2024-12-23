namespace ServiceLayer.DTOs
{
    public class OrderSummaryDTO
    {
        public int? OrderID { get; set; }

        public int? UserID { get; set; }

        public string OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime OrderDeliveryDate { get; set; }

        public int OrderPickupPoint { get; set; }

        public int OrderPickupCode { get; set; }

        public decimal? TotalCost { get; set; }
    }
}
