using System;
using System.Collections.Generic;

namespace Product_Management_Service.Context
{
    public partial class Category
    {
        public Category()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDelete { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
