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
    public class OrdersController : Controller
    {
        private MenuDbContext db = new MenuDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                var dbOrder = db.Orders.Where(o => o.OrderStatus == OrderStatusEnum.PENDING).FirstOrDefault();
                if (dbOrder == null)
                {
                    dbOrder = db.Orders.Create();
                    dbOrder.ApplicationUserId = User.Identity.GetUserId();
                    dbOrder.orderDate = DateTime.Now;
                    dbOrder.OrderProducts = new List<MenuProduct>();
                    dbOrder.OrderStatus = OrderStatusEnum.PENDING;
                    db.Orders.Add(dbOrder);
                    db.SaveChanges();
                }
                id = dbOrder.OrderId;
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Where(o => o.OrderId == id).Include(o => o.OrderProducts)
                .Include("OrderProducts.Product")
                .Include("OrderProducts.Menu.Company").FirstOrDefault();
            
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        public ActionResult Order(int? id)
        {
            var dbOrder = db.Orders.Where(o => o.OrderStatus == OrderStatusEnum.PENDING).Include(o => o.OrderProducts).Include("Companies").FirstOrDefault();
            if (dbOrder == null)
            {
                dbOrder = db.Orders.Create();
                dbOrder.ApplicationUserId = User.Identity.GetUserId();
                dbOrder.orderDate = DateTime.Now;
                dbOrder.OrderProducts = new List<MenuProduct>();
                dbOrder.OrderStatus = OrderStatusEnum.PENDING;
                dbOrder.Companies = new List<Company>();
                db.Orders.Add(dbOrder);
                db.SaveChanges();
            }
            dbOrder.OrderProducts.Add(db.MenuProducts.Find(id));
            foreach (var orderProduct in dbOrder.OrderProducts.Distinct())
            {
                orderProduct.Menu = db.Menus.Find(orderProduct.MenuId);

                Company company= db.Companies.Find(orderProduct.Menu.CompanyId);
                dbOrder.Companies.Add(company);

            }
            db.SaveChanges();
            return RedirectToAction("Details", new { id = dbOrder.OrderId });
        }

        public ActionResult Pay(int id)
        {
            ViewBag.orderId = id;
            return View();
        }

        [HttpPost]
        public ActionResult Pay([Bind(Include = "CardNumber,ValidationYear,ValidationMonth,OrderId")] Card card)
        {
            var order = db.Orders.Where(o => o.OrderId == card.OrderId).Include(o => o.OrderProducts).FirstOrDefault();
            ViewBag.orderId = order.OrderId;
            if (order.OrderProducts.Count > 0)
            {
                if(card.CardNumber == null || card.ValidationMonth == 0 || card.ValidationYear == 0)
                {
                    ViewBag.ErrorMessage = "შეიყვანეთ ყველა ველი";
                }
                else if(card.ValidationYear < DateTime.Now.Year || (card.ValidationYear == DateTime.Now.Year && card.ValidationMonth < DateTime.Now.Month))
                {
                    ViewBag.ErrorMessage = "თქვენი ბარათის მოქმედების ვადა ამოწურულია, გთხოვთ სცადოთ სხვა ბარათი";
                }
                else if(order.OrderProducts.Sum(p => p.ProductPrice) > 24.24)
                {
                    ViewBag.ErrorMessage = "ბარათზე არ არის საკმარისი თანხა";
                }
                else if (!_CheckCardNumber(card.CardNumber))
                {
                    ViewBag.ErrorMessage = "ბარათი არ არის ვალიდური";
                }
                else
                {
                    order.OrderStatus = OrderStatusEnum.NEW;
                    order.orderDate = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = card.OrderId });
                }
            }
            return View();
        }

        public ActionResult CancelOrder(int id)
        {
            var order = db.Orders.Where(o => o.OrderId == id).Include(o => o.OrderProducts).FirstOrDefault();
            if (DateTime.Now - order.orderDate <= TimeSpan.FromMinutes(10))
            {
                order.OrderStatus = OrderStatusEnum.CANCELED;
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id });
        }


        public ActionResult Rate(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult Rate(Order order)
        {
            if ((int)order.Stars <= 2 && (order.Comment == null || order.CommentCategory == CommentCategoryEnum.NULL))
            {
                ViewBag.ErrorMessage = "გთხოვთ დაწეროთ კომენტარი";
            }
            else
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = order.OrderId });
            }
            return View();
        }

        private bool _CheckCardNumber(string digits)
        {
            return digits.All(char.IsDigit) && digits.Reverse()
                .Select(c => c - 48)
                .Select((thisNum, i) => i % 2 == 0
                    ? thisNum
                    : ((thisNum *= 2) > 9 ? thisNum - 9 : thisNum)
                ).Sum() % 10 == 0;
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
