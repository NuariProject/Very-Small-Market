namespace Product_Management_Service.Models.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public List<CategoryDTO> CategoryList { get; set; }

    }
}
