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
using Microsoft.AspNet.Identity;

namespace BogMenu.Controllers
{
    public class MenuProductsController : Controller
    {
        private MenuDbContext db = new MenuDbContext();

        // GET: MenuProducts/5
        public ActionResult Index(int? id)
        {
            return RedirectToAction("Details", new { id });
        }

        // GET: MenuProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuProduct menuProduct = db.MenuProducts.Find(id);
            if (menuProduct == null)
            {
                return HttpNotFound();
            }
            menuProduct.Menu = db.Menus.Find(menuProduct.MenuId);
            menuProduct.Menu.Company = db.Companies.Find(menuProduct.Menu.CompanyId);
            menuProduct.Product = db.Products.Find(menuProduct.ProductId);
            return View(menuProduct);
        }

        // GET: MenuProducts/Create
        public ActionResult Create(int ? id)
        {
            ViewBag.Menu = db.Menus.Find(id);
            ViewBag.Menu.Company = db.Companies.Find(ViewBag.Menu.CompanyId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: MenuProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenuProductId,ProductId,cookingTime,MenuId,ProductPrice")] MenuProduct menuProduct)
        {
            if (ModelState.IsValid)
            {
                db.MenuProducts.Add(menuProduct);
                db.SaveChanges();
                return RedirectToAction("Details", "Menus", new { Id = menuProduct.MenuId });
            }

            ViewBag.MenuId = new SelectList(db.Menus, "MenuId", "Name", menuProduct.MenuId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", menuProduct.ProductId);
            return View(menuProduct);
        }

        // GET: MenuProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuProduct menuProduct = db.MenuProducts.Find(id);
            if (menuProduct == null)
            {
                return HttpNotFound();
            }
            menuProduct.Menu = db.Menus.Find(menuProduct.MenuId);
            menuProduct.Menu.Company = db.Companies.Find(menuProduct.Menu.CompanyId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", menuProduct.ProductId);
            return View(menuProduct);
        }

        // POST: MenuProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuProductId,ProductId,cookingTime,MenuId,ProductPrice")] MenuProduct menuProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Menus", new { Id = menuProduct.MenuId });
            }
            menuProduct.Menu = db.Menus.Find(menuProduct.MenuId);
            menuProduct.Menu.Company = db.Companies.Find(menuProduct.Menu.CompanyId);
            ViewBag.MenuId = new SelectList(db.Menus, "MenuId", "Name", menuProduct.MenuId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", menuProduct.ProductId);
            return View(menuProduct);
        }

        // GET: MenuProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuProduct menuProduct = db.MenuProducts.Find(id);
            if (menuProduct == null)
            {
                return HttpNotFound();
            }
            menuProduct.Product = db.Products.Find(menuProduct.ProductId);
            menuProduct.Menu = db.Menus.Find(menuProduct.MenuId);
            menuProduct.Menu.Company = db.Companies.Find(menuProduct.Menu.CompanyId);
            return View(menuProduct);
        }

        // POST: MenuProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuProduct menuProduct = db.MenuProducts.Find(id);
            db.MenuProducts.Remove(menuProduct);
            db.SaveChanges();
            return RedirectToAction("Details", "Menus", new { Id = menuProduct.MenuId });
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
