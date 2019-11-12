using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace S.Models
{
    public class StoreDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com" };
            string password = "Admin_admin7";
            var result = userManager.Create(admin, password);
            var user = new ApplicationUser { Email = "bodia@gmail.com", UserName = "bodia@gmail.com" };
            password = "B_b123";
            userManager.Create(user, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(user.Id, role2.Name);
            }
            context.Products.Add(new Product() { Name = "Jack Daniel's Tennessee Honey", Category = "Whiskey", Country = "USA", Size = 0.7, Strenght = 40, Price = 716 });
            context.Products.Add(new Product() { Name = "Johnnie Walker Red label", Category = "Whiskey", Country = "Scotland", Size = 0.7, Strenght = 40, Price = 505 });
            context.Products.Add(new Product() { Name = "Jameson Irish", Category = "Whiskey", Country = "Ireland", Size = 1, Strenght = 40, Price = 810 });
            context.Products.Add(new Product() { Name = "Chivas Regal", Category = "Whiskey", Country = "Scotland", Size = 0.7, Strenght = 40, Price = 1090 });
            context.Products.Add(new Product() { Name = "Jack Daniel's Old No.71", Category = "Whiskey", Country = "USA", Size = 1, Strenght = 40, Price = 735 });
            context.Products.Add(new Product() { Name = "Chivas Regal 25 Years", Category = "Whiskey", Country = "Scotland", Size = 0.7, Strenght = 40, Price = 10800 });
            context.Products.Add(new Product() { Name = "Chivas Regal Extra", Category = "Whiskey", Country = "Scotland", Size = 0.7, Strenght = 40, Price = 1490 });

            context.Products.Add(new Product() { Name = "Zubrowka Bison Grass", Category = "Vodka", Country = "Poland", Size = 1, Strenght = 37.5, Price = 184 });
            context.Products.Add(new Product() { Name = "Nemiroff Premium", Category = "Vodka", Country = "Ukraine", Size = 0.7, Strenght = 40, Price = 177 });
            context.Products.Add(new Product() { Name = "Finlandia Redberry", Category = "Vodka", Country = "Finland", Size = 1, Strenght = 37.5, Price = 383 });
            context.Products.Add(new Product() { Name = "Nemiroff Lex Ultra", Category = "Vodka", Country = "Ukraine", Size = 0.7, Strenght = 40, Price = 321 });
            context.Products.Add(new Product() { Name = "Zubrowka Biala", Category = "Vodka", Country = "Poland", Size = 1, Strenght = 40, Price = 189 });
            context.Products.Add(new Product() { Name = "Finlandia Lime", Category = "Vodka", Country = "Finland", Size = 1, Strenght = 37.5, Price = 403 });
            context.Products.Add(new Product() { Name = "Green Day Discovery", Category = "Vodka", Country = "Ukraine", Size = 0.7, Strenght = 40, Price = 283 });

            context.Products.Add(new Product() { Name = "Hennessy XO 20 Years", Category = "Cognac", Country = "France", Size = 0.7, Strenght = 40, Price = 6350 });
            context.Products.Add(new Product() { Name = "Hennessy VSOP 60 Years", Category = "Cognac", Country = "France", Size = 1, Strenght = 40, Price = 1990 });
            context.Products.Add(new Product() { Name = "Martell V.S.O.P", Category = "Cognac", Country = "France", Size = 0.5, Strenght = 40, Price = 1350 });
            context.Products.Add(new Product() { Name = "Martell Creation Grand Extra", Category = "Cognac", Country = "France", Size = 0.7, Strenght = 40, Price = 13100 });
            context.Products.Add(new Product() { Name = "Жан-Жак Медовий", Category = "Cognac", Country = "Ukraine", Size = 0.5, Strenght = 37.5, Price = 146 });
            context.Products.Add(new Product() { Name = "Shabo Grand Reserve", Category = "Cognac", Country = "Ukraine", Size = 0.5, Strenght = 40, Price = 163 });
            context.Products.Add(new Product() { Name = "Shabo Grand Reserve V.S.O.P 5 Years", Category = "Cognac", Country = "Ukraine", Size = 0.5, Strenght = 40, Price = 189 });

            context.Products.Add(new Product() { Name = "Martini Prosecco", Category = "Champagne", Country = "Italy", Size = 0.75, Strenght = 11.5, Price = 373 });
            context.Products.Add(new Product() { Name = "Cava Jaume Serra Brut", Category = "Champagne", Country = "Spain", Size = 0.75, Strenght = 12, Price = 240 });
            context.Products.Add(new Product() { Name = "Asti Cante", Category = "Champagne", Country = "Italy", Size = 0.75, Strenght = 7, Price = 295 });
            context.Products.Add(new Product() { Name = "Grandes Caves St Roch 1/2 Sec Touraine Mousseux", Category = "Champagne", Country = "France", Size = 0.75, Strenght = 12, Price = 365 });
            context.Products.Add(new Product() { Name = "Grandes Caves St Roch Brut Touraine Mousseux Rose", Category = "Champagne", Country = "France", Size = 0.75, Strenght = 12, Price = 365 });
            context.Products.Add(new Product() { Name = "Maison Bouey Gremillet Selection", Category = "Champagne", Country = "France", Size = 0.75, Strenght = 12.5, Price = 1190 });
            context.Products.Add(new Product() { Name = "Moet & Chandon Brut Imperial", Category = "Champagne", Country = "France", Size = 0.75, Strenght = 12, Price = 1890 });

            base.Seed(context);
        }
    }
}