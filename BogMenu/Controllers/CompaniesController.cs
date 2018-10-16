using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BogMenu.Data;
using BogMenu.Models;

namespace BogMenu.Controllers
{
    public class CompaniesController : Controller
    {
        private MenuDbContext db = new MenuDbContext();
        private string[] validImageTypes = new string[]
        {
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
        };

        // GET: Companies
        public ActionResult Index()
        {
            return View(db.Companies.ToList());
        }

        public ActionResult FindSimilar(int id)
        {
            List<MenuProduct> products = db.Orders.Where(o => o.OrderId == id).Include(o => o.OrderProducts).FirstOrDefault().OrderProducts.ToList();
            var companies = db.Companies.Include("Menus").Include("Menus.MenuProducts").ToList();
            companies = companies.Where(c => CheckCompany(c, products) != 0).ToList();
            companies.Sort((comp1, comp2) => CheckCompany(comp1, products).CompareTo(CheckCompany(comp2, products)));
            return View(companies);
        }

        private int CheckCompany(Company c, List<MenuProduct> products)
        {
            MenuProduct[] myProducts = new MenuProduct[products.Count];
            products.CopyTo(myProducts);
            var myProdsList = myProducts.ToList();
            int sum = 0;
            foreach (var menu in c.Menus)
            {
                for(var i = myProdsList.Count - 1; i >= 0; i--)
                {
                    var p = myProdsList[i];
                    if (menu.MenuProducts.Any(innerP => innerP.ProductId == p.ProductId))
                    {
                        myProdsList.Remove(p);
                        sum += menu.MenuProducts.Where(innerP => innerP.ProductId == p.ProductId).FirstOrDefault().ProductPrice;
                    }
                }
            }
            return sum;
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            db.Entry(company).Collection(dbCmpany => dbCmpany.Menus).Load();
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyId,Name,Description,CompanyType,ImageUpload,Cost")] Company company)
        {
            if (company.ImageUpload == null || company.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "This field is required");
            }
            else if (!validImageTypes.Contains(company.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }
            if (ModelState.IsValid)
            {
                if (company.ImageUpload != null && company.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/Content/uploads";
                    var logoName = Path.ChangeExtension(Guid.NewGuid().ToString(), Path.GetExtension(company.ImageUpload.FileName));
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), logoName);
                    var imageUrl = Path.Combine(uploadDir, logoName);
                    company.ImageUpload.SaveAs(imagePath);
                    company.Logo = imageUrl;
                }
                db.Companies.Add(company);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Same company name", e);
                    return View(company);
                }
                return RedirectToAction("Index");
            }

            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyId,Name,Description,CompanyType,ImageUpload,Cost")] Company company)
        {
            if (company.ImageUpload != null && company.ImageUpload.ContentLength > 0 && !validImageTypes.Contains(company.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }
            if (ModelState.IsValid)
            {
                var dbCompany = db.Companies.AsNoTracking().Where(comp => comp.CompanyId == company.CompanyId).FirstOrDefault();
                var imageUrl = dbCompany.Logo;
                if (company.ImageUpload != null && company.ImageUpload.ContentLength > 0)
                {
                    company.ImageUpload.SaveAs(Server.MapPath(imageUrl));
                }
                company.Logo = imageUrl;
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
            db.SaveChanges();
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
