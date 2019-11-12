using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using S.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace S.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        [AllowAnonymous]
        public ActionResult Index(string SearchString, int page = 1)
        {
            string userid = HttpContext.User.Identity.GetUserId();
            Bucket bucket = db.Buckets.FirstOrDefault(p => p.UserId == userid);
            if (bucket == null)
            {
                bucket = new Bucket() { UserId = userid };
                db.Buckets.Add(bucket);
            }
            GetProducts get;
            int pageSize = 7;
            if (SearchString != null)
            {
                var pr = db.Products.Where(p => p.Name.Contains(SearchString));
                var products = pr.OrderBy(p=>p.Name).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.Products.Count() };
                get = new GetProducts() { Products = products, pageInfo = pageInfo };
                ViewBag.Products = products;
            }
            else
            {
                var products=db.Products.OrderBy(p=>p.Name).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.Products.Count() };
                get = new GetProducts() { Products = products, pageInfo = pageInfo };
                ViewBag.Products = products;
            }
            db.SaveChanges();
            return View(get);
        }
        [HttpGet]
        public async Task<ActionResult> Add(int id)
        {
            string userid = HttpContext.User.Identity.GetUserId();
            Bucket bucket = db.Buckets.First(p => p.UserId == userid);
            Product product = await db.Products.FindAsync(id);
            bucket.Products.Add(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Bucket()
        {
            string userid = HttpContext.User.Identity.GetUserId();
            Bucket bucket = (from b in db.Buckets
                             where b.UserId.Equals(userid)
                             select b).ToList<Bucket>().First();
            var products = (from p in db.Products
                            where bucket.Id == p.BucketId
                            select new ProductDisplay()
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Category = p.Category,
                                Country = p.Country,
                                Strenght = p.Strenght,
                                Size = p.Size,
                                Price = p.Price,
                                NewPrice = p.NewPrice
                            }).ToList();
            bucket.TotalPrice = products.Sum(p => p.NewPrice != 0 ? p.NewPrice : p.Price);
            GetBucket getBucket = new GetBucket() { Id = bucket.Id, Products = products, TotalPrice = bucket.TotalPrice };

            return View(getBucket);
        }
        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BucketId = id;
            return View();
        }
        [HttpPost]
        public ActionResult Buy(Order o)
        {
            string userid = HttpContext.User.Identity.GetUserId();
            Bucket bucket = (from b in db.Buckets
                             where b.UserId.Equals(userid)
                             select b).ToList<Bucket>().First();
            o.BucketId = bucket.Id;
            o.UserId = userid;
            o.DateTime = DateTime.Now;
            bucket.TotalPrice = 0;

            db.Orders.Add(o);
            var products = (from p in db.Products
                            where bucket.Id == p.BucketId
                            select p).ToList();
            products.ForEach(p =>
            {
                p.BucketId = null;
                p.Bucket = null;
                p.OrderId = o.OrderId;
                p.Order = o;
            });
            db.SaveChanges();
            return View("Order");
        }
        [HttpGet]
        public ActionResult History()
        {
            string userid = HttpContext.User.Identity.GetUserId();
            var orders = (from o in db.Orders
                          where o.UserId == userid
                          select new GetOrder()
                          {
                              Id = o.OrderId,
                              PostAddress = o.Address,
                              DateTime = o.DateTime
                          }).ToList();
            return View(orders);
        }
        [HttpGet]
        public async Task<ActionResult> Cancel(int id)
        {
            string userid = HttpContext.User.Identity.GetUserId();
            var bucket = db.Buckets.FirstOrDefault(p => p.UserId == userid);

            Product product = await db.Products.FindAsync(id);
            bucket.TotalPrice -= product.Price;
            product.BucketId = null;
            await db.SaveChangesAsync();
            return RedirectToAction("Bucket");
        }
        //-----------admin--------------
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddDiscount(int id)
        {
            var product = await db.Products.FindAsync(id);
            return View(product);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddDiscount(Product newProduct)
        {
            var product = await db.Products.FindAsync(newProduct.Id);
            product.Discount = newProduct.Discount;
            product.NewPrice = product.Price - product.Price * product.Discount / 100.0;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CancelDiscount(int id)
        {
            Product product = await db.Products.FindAsync(id);
            product.Discount = 0;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddProduct(Product newProduct)
        {
            Product product = new Product()
            {
                Name = newProduct.Name,
                Category = newProduct.Category,
                Country = newProduct.Country,
                Strenght = newProduct.Strenght,
                Size = newProduct.Size,
                Price = newProduct.Price
            };

            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var productToDelete = await db.Products.FindAsync(id);
            db.Products.Remove(productToDelete);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int id)
        {
            Product product = await db.Products.FindAsync(id);
            Product edit = new Product()
            {
                Id = id,
                Name = product.Name,
                Category = product.Category,
                Country = product.Country,
                Strenght = product.Strenght,
                Size = product.Size,
                Discount=product.Discount,
                Price = product.Price
            };
            return View(edit);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(Product productEdit)
        {
            Product product = await db.Products.FindAsync(productEdit.Id);
            product.Name = productEdit.Name;
            product.Category = productEdit.Category;
            product.Country = productEdit.Country;
            product.Strenght = productEdit.Strenght;
            product.Size = productEdit.Size;
            product.Discount = productEdit.Discount;
            product.Price = productEdit.Price;
            product.NewPrice = product.Price - product.Price * product.Discount / 100.0;

            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}