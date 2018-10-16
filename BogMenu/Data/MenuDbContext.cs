using BogMenu.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BogMenu.Data
{
    public class MenuDbContext : DbContext
    {
        public MenuDbContext() : base("DefaultConnection") { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuProduct> MenuProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}