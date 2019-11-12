using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S.Models
{
    public class GetBucket
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public List<ProductDisplay> Products { get; set; } 
    }
}