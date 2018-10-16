namespace BogMenu.Migrations.Menu
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        _companyType = c.Int(nullable: false),
                        Logo = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RegisterDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        MenuId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        _menuType = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MenuId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.MenuProducts",
                c => new
                    {
                        MenuProductId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        cookingTime = c.Int(nullable: false),
                        MenuId = c.Int(nullable: false),
                        OrderProduct_OrderProductId = c.Int(),
                    })
                .PrimaryKey(t => t.MenuProductId)
                .ForeignKey("dbo.Menus", t => t.MenuId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.OrderProducts", t => t.OrderProduct_OrderProductId)
                .Index(t => t.ProductId)
                .Index(t => t.MenuId)
                .Index(t => t.OrderProduct_OrderProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        OrderProductId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderProductId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.Int(nullable: false),
                        _OrderStatus = c.Int(nullable: false),
                        orderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ApplicationUserId = c.Int(nullable: false),
                        Comment = c.String(),
                        _CommentCateroty = c.Int(nullable: false),
                        _stars = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.MenuProducts", "OrderProduct_OrderProductId", "dbo.OrderProducts");
            DropForeignKey("dbo.MenuProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.MenuProducts", "MenuId", "dbo.Menus");
            DropForeignKey("dbo.Menus", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Ratings", new[] { "OrderId" });
            DropIndex("dbo.OrderProducts", new[] { "OrderId" });
            DropIndex("dbo.MenuProducts", new[] { "OrderProduct_OrderProductId" });
            DropIndex("dbo.MenuProducts", new[] { "MenuId" });
            DropIndex("dbo.MenuProducts", new[] { "ProductId" });
            DropIndex("dbo.Menus", new[] { "CompanyId" });
            DropTable("dbo.Ratings");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderProducts");
            DropTable("dbo.Products");
            DropTable("dbo.MenuProducts");
            DropTable("dbo.Menus");
            DropTable("dbo.Companies");
        }
    }
}
