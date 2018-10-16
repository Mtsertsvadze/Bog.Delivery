namespace BogMenu.Migrations.Menu
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MenuProducts", "OrderProduct_OrderProductId", "dbo.OrderProducts");
            DropForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Ratings", "OrderId", "dbo.Orders");
            DropIndex("dbo.MenuProducts", new[] { "OrderProduct_OrderProductId" });
            DropIndex("dbo.OrderProducts", new[] { "OrderId" });
            DropIndex("dbo.Ratings", new[] { "OrderId" });
            DropPrimaryKey("dbo.Orders");
            CreateTable(
                "dbo.OrderMenuProducts",
                c => new
                    {
                        Order_OrderId = c.Int(nullable: false),
                        MenuProduct_MenuProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_OrderId, t.MenuProduct_MenuProductId })
                .ForeignKey("dbo.Orders", t => t.Order_OrderId, cascadeDelete: true)
                .ForeignKey("dbo.MenuProducts", t => t.MenuProduct_MenuProductId, cascadeDelete: true)
                .Index(t => t.Order_OrderId)
                .Index(t => t.MenuProduct_MenuProductId);
            
            AddColumn("dbo.Companies", "RegisterDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "OrderStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "CommentCateroty", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "Stars", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Orders", "OrderId");
            CreateIndex("dbo.Orders", "OrderId");
            AddForeignKey("dbo.Orders", "OrderId", "dbo.Ratings", "RatingId");
            DropColumn("dbo.MenuProducts", "OrderProduct_OrderProductId");
            DropColumn("dbo.Orders", "_OrderStatus");
            DropColumn("dbo.Ratings", "_CommentCateroty");
            DropColumn("dbo.Ratings", "_stars");
            DropTable("dbo.OrderProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        OrderProductId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderProductId);
            
            AddColumn("dbo.Ratings", "_stars", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "_CommentCateroty", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "_OrderStatus", c => c.Int(nullable: false));
            AddColumn("dbo.MenuProducts", "OrderProduct_OrderProductId", c => c.Int());
            DropForeignKey("dbo.Orders", "OrderId", "dbo.Ratings");
            DropForeignKey("dbo.OrderMenuProducts", "MenuProduct_MenuProductId", "dbo.MenuProducts");
            DropForeignKey("dbo.OrderMenuProducts", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.OrderMenuProducts", new[] { "MenuProduct_MenuProductId" });
            DropIndex("dbo.OrderMenuProducts", new[] { "Order_OrderId" });
            DropIndex("dbo.Orders", new[] { "OrderId" });
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Ratings", "Stars");
            DropColumn("dbo.Ratings", "CommentCateroty");
            DropColumn("dbo.Orders", "OrderStatus");
            DropColumn("dbo.Companies", "RegisterDate");
            DropTable("dbo.OrderMenuProducts");
            AddPrimaryKey("dbo.Orders", "OrderId");
            CreateIndex("dbo.Ratings", "OrderId");
            CreateIndex("dbo.OrderProducts", "OrderId");
            CreateIndex("dbo.MenuProducts", "OrderProduct_OrderProductId");
            AddForeignKey("dbo.Ratings", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
            AddForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
            AddForeignKey("dbo.MenuProducts", "OrderProduct_OrderProductId", "dbo.OrderProducts", "OrderProductId");
        }
    }
}
