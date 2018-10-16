using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BogMenu.Models
{
    public class Company
    {
        public Company()
        {
            RegisterDate = DateTime.Now;
        }
        public int CompanyId { get; set; }
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Company Type")]
        public CompanyTypeEnum CompanyType { get; set; }
        public string Logo { get; set; }
        public CompanyCostEnum Cost { get; set; }
        public DateTime RegisterDate { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Menu> Menus { get; set; }
        [NotMapped]
        [DataType(DataType.Upload)]
        [Display(Name = "Upload Image")]
        public HttpPostedFileBase ImageUpload { get; set; }
        [NotMapped]
        public decimal Rating {
            get
            {
                using (BogMenu.Data.MenuDbContext db = new Data.MenuDbContext())
                {
                  
                    var orders = db.Orders.Include("Companies").Where(o => o.Companies.Any(x => x.CompanyId == CompanyId));
                    if (orders.Count(order => order.Stars > 0) > 0)
                    {
                        var sum = (orders.Sum(order => (int)order.Stars));
                        return (decimal)sum / orders.Count(order => order.Stars > 0);
                    }
                    return 0;
                }
            }
        }
    }
    public class Menu
    {
        [Display(Name = "Menu")]
        public int MenuId { get; set; }
        public string Name { get; set; }
        [Display(Name = "Menu Type")]
        public MenuTypeEnum MenuType { get; set; }
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<MenuProduct> MenuProducts { get; set; }
    }

    public class MenuProduct
    {
        public int MenuProductId { get; set; }
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Display(Name = "Cooking Time")]
        public int cookingTime { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        [Display(Name = "Product Price")]
        public int ProductPrice { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public string ApplicationUserId { get; set; }
        [Display(Name = "Order Status")]
        public OrderStatusEnum OrderStatus { get; set; }
        [Display(Name = "Order Date")]
        public DateTime orderDate { get; set; }
        public ICollection<MenuProduct> OrderProducts { get; set; }
        public string Comment { get; set; }
        [Display(Name = "Comment Category")]
        public CommentCategoryEnum CommentCategory { get; set; }
        public StarsEnum Stars { get; set; }
        public List<Company> Companies { get; set; }
        // public int CompanyId { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        [Display(Name = "Product")]
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string ProductName { get; set; }
    }

    public class Card
    {
        public int CardId { get; set; }
        [StringLength(16)]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }
        [Display(Name = "Validation Year")]
        public int ValidationYear { get; set; }
        [Display(Name = "Validation Month")]
        public int ValidationMonth { get; set; }
        [Display(Name = "Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}