using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }
        public int BucketId { get; set; }
        public string Address { get; set; }
    }
}