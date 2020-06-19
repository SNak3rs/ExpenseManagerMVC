using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpenseManager.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        public virtual List<Income> Incomes { get; set; }
        public virtual List<Expense> Expenses { get; set; }
    }
}