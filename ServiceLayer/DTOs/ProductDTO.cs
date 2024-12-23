namespace ServiceLayer.DTOs
{
    public class ProductDTO
    {
        public string ProductArticleNumber { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public string ProductDescription { get; set; } = null!;

        public string ProductCategory { get; set; } = null!;

        public string? ProductPhoto { get; set; }

        public decimal ProductCost { get; set; }

        public byte? ProductDiscountAmount { get; set; }

        public int ProductQuantityInStock { get; set; }

        public string ProductStatus { get; set; } = null!;

        public string ProductManufacturer { get; set; } = null!;

        public decimal? TotalCost { get; set; }

        public int ProductCountInOrder { get; set; }
    }
}
