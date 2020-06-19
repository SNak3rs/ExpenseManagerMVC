using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ExpenseManager.DAL
{
    public class DeletedEntries : DbContext
    {
        public DbSet<DeletedExpense> DeletedExpenses { get; set; } 

        public DeletedEntries() : base("DeletedEntries")
        {
        }
    }
}