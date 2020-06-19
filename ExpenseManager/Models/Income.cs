using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExpenseManager.Models
{
    public class Income
    {
        public int IncomeID { get; set; }

        public string UserID { get; set; }

        [Required()]
        [StringLength(200, MinimumLength = 2)]
        [Display(Name = "Income Name")]
        public string IncomeName { get; set; }

        [Required()]
        [DataType(DataType.Currency)]
        [Display(Name = "Income Amount")]
        public float IncomeValue { get; set; }

        [Required()]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime IncomeDate { get; set; }

        [Required()]
        public int CategoryID { get; set; }

        public virtual Category IncomeCategory { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
    }
}