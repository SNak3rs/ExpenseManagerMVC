using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExpenseManager.Models
{
    public class Expense
    {
        public int ExpenseID { get; set; }
        public string UserID { get; set; }

        [Required()]
        [StringLength(200, MinimumLength = 2)]
        [Display(Name = "Expense Name")]
        public string ExpenseName { get; set; }

        [Required()]
        [DataType(DataType.Currency)]
        [Display(Name = "Expense Amount")]
        public float ExpenseValue { get; set; }

        [Required()]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime ExpenseDate { get; set; }

        [Required()]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        public virtual Category ExpenseCategory { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
    }
}