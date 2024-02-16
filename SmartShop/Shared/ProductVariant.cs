using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Shared
{
    public class ProductVariant
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductTypeId {  get; set; }

        public decimal Price {  get; set; }
        public decimal OriginalPrice { get; set;}
    }
}
