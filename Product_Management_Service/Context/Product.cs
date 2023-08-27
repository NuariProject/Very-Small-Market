using System;
using System.Collections.Generic;

namespace Product_Management_Service.Context
{
    public partial class Product
    {
        public Product()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
