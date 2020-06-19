using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ExpenseManager.Models;
using Microsoft.AspNet.Identity;

namespace ExpenseManager.Controllers
{
    [Authorize]
    public class IncomesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Incomes
        public ActionResult Index(DateTime? incomeStartDate, DateTime? incomeEndDate, string incomeCategory, string sortOrder)
        {
            string loggedUserID = User.Identity.GetUserId(); // Get Logged UserID
            var incomes = db.Incomes.Include(i => i.IncomeCategory).Include(i => i.User).Where(i => i.UserID.Equals(loggedUserID)); // Get incomes for User
            ViewBag.CategoriesList = incomes.Select( i => i.IncomeCategory.CategoryName).Distinct(); // Get User Categories

            if (incomeStartDate != null && incomeEndDate != null) // Filter by date
            {
                incomes = incomes.Where(i => i.IncomeDate >= incomeStartDate && i.IncomeDate <= incomeEndDate);
            }

            if (!string.IsNullOrEmpty(incomeCategory)) // Filter by category
            {
                incomes = incomes.Where(i => i.IncomeCategory.CategoryName.Equals(incomeCategory));
            }

            if (!string.IsNullOrEmpty(sortOrder)) // Sorting Order
            { 
                if (sortOrder.Equals("ASC")) {
                    incomes = incomes.OrderBy(i => i.IncomeValue);
                }
                else if (sortOrder.Equals("DESC"))
                {
                    incomes = incomes.OrderByDescending(i => i.IncomeValue);
                }
            }

            return View(incomes.ToList());
        }

        // GET: Incomes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string loggedUserID = User.Identity.GetUserId();
            Income income = db.Incomes.SingleOrDefault(s => s.IncomeID == id && s.UserID.Equals(loggedUserID));

            if (income == null)
            {
                return HttpNotFound();
            }
            return View(income);
        }

        // GET: Incomes/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Incomes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncomeID,UserID,IncomeName,IncomeValue,IncomeDate,CategoryID")] Income income)
        {
            if (ModelState.IsValid)
            {
                income.UserID = User.Identity.GetUserId();
                db.Incomes.Add(income);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", income.CategoryID);
            return View(income);
        }

        // GET: Incomes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string loggedUserID = User.Identity.GetUserId();
            Income income = db.Incomes.SingleOrDefault(s => s.IncomeID == id && s.UserID.Equals(loggedUserID));

            if (income == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", income.CategoryID);
            return View(income);
        }

        // POST: Incomes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncomeID,UserID,IncomeName,IncomeValue,IncomeDate,CategoryID")] Income income)
        {
            if (ModelState.IsValid)
            {
                income.UserID = User.Identity.GetUserId();
                db.Entry(income).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", income.CategoryID);
            return View(income);
        }

        /*
        // GET: Incomes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string loggedUserID = User.Identity.GetUserId();
            Income income = db.Incomes.SingleOrDefault(s => s.IncomeID == id && s.UserID.Equals(loggedUserID));

            if (income == null)
            {
                return HttpNotFound();
            }
            return View(income);
        }

        // POST: Incomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Income income = db.Incomes.Find(id);
            db.Incomes.Remove(income);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        */

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
