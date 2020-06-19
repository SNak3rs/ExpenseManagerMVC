using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpenseManager.Models
{
    public class DeletedExpense
    {
        [Key]
        public int ExpenseID { get; set; }
        public string UserID { get; set; }
        public string ExpenseName { get; set; }
        public float ExpenseValue { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int CategoryID { get; set; }

        public DeletedExpense(Expense ex)
        {
            ExpenseID = ex.ExpenseID;
            UserID = ex.UserID;
            ExpenseName = ex.ExpenseName;
            ExpenseValue = ex.ExpenseValue;
            ExpenseDate = ex.ExpenseDate;
            CategoryID = ex.CategoryID;
        }
    }
}