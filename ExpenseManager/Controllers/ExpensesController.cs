using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExpenseManager.DAL;
using ExpenseManager.Models;
using Microsoft.AspNet.Identity;

namespace ExpenseManager.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Expenses
        public ActionResult Index(DateTime? expenseStartDate, DateTime? expenseEndDate, string expenseCategory, string sortOrder)
        {
            string loggedUserID = User.Identity.GetUserId(); // Get Logged UserID
            var expenses = db.Expenses.Include(e => e.ExpenseCategory).Include(e => e.User).Where(e => e.UserID.Equals(loggedUserID)); // Get expenses for User
            ViewBag.CategoriesList = expenses.Select(i => i.ExpenseCategory.CategoryName).Distinct(); // Get User Categories

            if (expenseStartDate != null && expenseEndDate != null) // Filter by date
            {
                expenses = expenses.Where(i => i.ExpenseDate >= expenseStartDate && i.ExpenseDate <= expenseEndDate);
            }

            if (!string.IsNullOrEmpty(expenseCategory)) // Filter by category
            {
                expenses = expenses.Where(i => i.ExpenseCategory.CategoryName.Equals(expenseCategory));
            }

            if (!string.IsNullOrEmpty(sortOrder)) // Sorting Order
            {
                if (sortOrder.Equals("ASC"))
                {
                    expenses = expenses.OrderBy(i => i.ExpenseValue);
                }
                else if (sortOrder.Equals("DESC"))
                {
                    expenses = expenses.OrderByDescending(i => i.ExpenseValue);
                }
            }

            return View(expenses.ToList());
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string loggedUserID = User.Identity.GetUserId();
            Expense expense = db.Expenses.SingleOrDefault(x => x.ExpenseID == id && x.UserID.Equals(loggedUserID));

            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpenseID,UserID,ExpenseName,ExpenseValue,ExpenseDate,CategoryID")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                expense.UserID = User.Identity.GetUserId();
                db.Expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", expense.CategoryID);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string loggedUserID = User.Identity.GetUserId();
            Expense expense = db.Expenses.SingleOrDefault(x => x.ExpenseID == id && x.UserID.Equals(loggedUserID));

            if (expense == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", expense.CategoryID);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpenseID,UserID,ExpenseName,ExpenseValue,ExpenseDate,CategoryID")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                expense.UserID = User.Identity.GetUserId();
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", expense.CategoryID);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string loggedUserID = User.Identity.GetUserId();
            Expense expense = db.Expenses.SingleOrDefault(x => x.ExpenseID == id && x.UserID.Equals(loggedUserID));

            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeletedEntries de = new DeletedEntries();
            Expense expense = db.Expenses.Find(id);
            de.DeletedExpenses.Add(new DeletedExpense(expense));
            de.SaveChanges();
            db.Expenses.Remove(expense);
            db.SaveChanges();
            de.Dispose();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
