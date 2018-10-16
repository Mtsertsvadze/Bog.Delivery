namespace BogMenu.Migrations.Menu
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderMenuProducts", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderMenuProducts", "MenuProduct_MenuProductId", "dbo.MenuProducts");
            DropIndex("dbo.OrderMenuProducts", new[] { "Order_OrderId" });
            DropIndex("dbo.OrderMenuProducts", new[] { "MenuProduct_MenuProductId" });
            AddColumn("dbo.MenuProducts", "ProductPrice", c => c.Int(nullable: false));
            AddColumn("dbo.MenuProducts", "Order_OrderId", c => c.Int());
            AddColumn("dbo.Orders", "Company_CompanyId", c => c.Int());
            CreateIndex("dbo.MenuProducts", "Order_OrderId");
            CreateIndex("dbo.Orders", "Company_CompanyId");
            AddForeignKey("dbo.MenuProducts", "Order_OrderId", "dbo.Orders", "OrderId");
            AddForeignKey("dbo.Orders", "Company_CompanyId", "dbo.Companies", "CompanyId");
            DropTable("dbo.OrderMenuProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderMenuProducts",
                c => new
                    {
                        Order_OrderId = c.Int(nullable: false),
                        MenuProduct_MenuProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_OrderId, t.MenuProduct_MenuProductId });
            
            DropForeignKey("dbo.Orders", "Company_CompanyId", "dbo.Companies");
            DropForeignKey("dbo.MenuProducts", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.Orders", new[] { "Company_CompanyId" });
            DropIndex("dbo.MenuProducts", new[] { "Order_OrderId" });
            DropColumn("dbo.Orders", "Company_CompanyId");
            DropColumn("dbo.MenuProducts", "Order_OrderId");
            DropColumn("dbo.MenuProducts", "ProductPrice");
            CreateIndex("dbo.OrderMenuProducts", "MenuProduct_MenuProductId");
            CreateIndex("dbo.OrderMenuProducts", "Order_OrderId");
            AddForeignKey("dbo.OrderMenuProducts", "MenuProduct_MenuProductId", "dbo.MenuProducts", "MenuProductId", cascadeDelete: true);
            AddForeignKey("dbo.OrderMenuProducts", "Order_OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
        }
    }
}
