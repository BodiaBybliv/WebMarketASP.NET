using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Country { get; set; }
        public double Size { get; set; }
        public double Strenght { get; set; }
        public double Price { get; set; }
        public double NewPrice { get; set; }
        public double Discount { get; set; }
        public int? BucketId { get; set; }
        public Bucket Bucket { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }
    }
}