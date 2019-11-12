using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S.Models
{
    public class GetProducts
    {
        public List<Product> Products { get; set; }
        public PageInfo pageInfo { get; set; }
    }
}