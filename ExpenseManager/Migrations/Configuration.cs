namespace ExpenseManager.Migrations
{
    using ExpenseManager.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ExpenseManager.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ExpenseManager.Models.ApplicationDbContext";
        }

        protected override void Seed(ExpenseManager.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            List<Category> Categories = new List<Category>
            {
                new Category{ CategoryName = "Category 1" },
                new Category{ CategoryName = "Category 2" },
                new Category{ CategoryName = "Category 3" },
            };

            context.Categories.AddRange(Categories);

            context.SaveChanges();
        }
    }
}
