using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BogMenu.Data;
using BogMenu.Models;

namespace BogMenu.Controllers
{
    public class MenusController : Controller
    {
        private MenuDbContext db = new MenuDbContext();

        // GET: Menus/5
        public ActionResult Index(int? id)
        {
            return RedirectToAction("Details", new { id });
        }

        // GET: Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            menu.Company = db.Companies.Find(menu.CompanyId);
            db.Entry(menu).Collection(dbMenu => dbMenu.MenuProducts).Load();
            foreach (var menuProduct in menu.MenuProducts)
            {
                menuProduct.Menu = menu;
                menuProduct.Product = db.Products.Find(menuProduct.ProductId);
            }
            return View(menu);
        }

        // GET: Menus/Create
        public ActionResult Create(int? id)
        {
            ViewBag.Company = db.Companies.Find(id);
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenuId,Name,MenuType,CompanyId")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Details", "Companies", new { Id = menu.CompanyId });
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", menu.CompanyId);
            return View(menu);
        }

        // GET: Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            menu.Company = db.Companies.Find(menu.CompanyId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", menu.CompanyId);
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuId,Name,MenuType,CompanyId")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Companies", new { Id = menu.CompanyId });
            }
            menu.Company = db.Companies.Find(menu.CompanyId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", menu.CompanyId);
            return View(menu);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            menu.Company = db.Companies.Find(menu.CompanyId);
            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
            db.SaveChanges();
            return RedirectToAction("Details", "Companies", new { Id = menu.CompanyId });
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
