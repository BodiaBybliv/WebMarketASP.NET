using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace S.Models
{
    public class StoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Bucket> Buckets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public StoreContext() : base("DefaultConnection")
        {

        }
    }
}