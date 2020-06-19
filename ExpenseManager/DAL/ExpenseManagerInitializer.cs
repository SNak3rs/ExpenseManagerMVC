using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ExpenseManager.DAL
{
    public class ExpenseManagerInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);

            List<Category> Categories = new List<Category>
            {
                new Category{ CategoryName = "Category_1" },
                new Category{ CategoryName = "Category_2" },
                new Category{ CategoryName = "Category_3" },
            };

            context.Categories.AddRange(Categories);

            context.SaveChanges();
        }
    }
}