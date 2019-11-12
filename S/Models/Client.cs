using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S.Models
{
    public class Client:IdentityUser
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }

        public string City { get; set; }
        public string Street { get; set; }

        public Bucket Bucket { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}