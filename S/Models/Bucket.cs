using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S.Models
{
    public class Bucket
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public double TotalPrice { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}