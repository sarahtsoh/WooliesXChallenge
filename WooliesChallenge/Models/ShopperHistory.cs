using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesChallenge.Models
{
    public class ShopperHistory
    {
        public string customerId { get; set; }
        public List<Product> products { get; set; }

    }
}
