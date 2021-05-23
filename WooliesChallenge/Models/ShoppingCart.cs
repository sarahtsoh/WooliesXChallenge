using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesChallenge.Models
{
    public class ShoppingCart
    {
        public List<RegularPriceProduct> products { get; set; }
        public List<SpecialPriceProductDeal> specials { get; set; }
        public List<ProductQuantity> quantities { get; set; }

    }

    public class RegularPriceProduct 
    {
        public string name { get; set; }
        public decimal price { get; set; }
    }

    public class SpecialPriceProduct
    {
        public string name { get; set; }
        public int quantity { get; set; }
    }

    public class SpecialPriceProductDeal
    {
        public List<ProductQuantity> quantities { get; set; }
        public decimal total { get; set; }
    }

    public class ProductQuantity
    {
        public string name { get; set; }
        public int quantity { get; set; }
    }

}
