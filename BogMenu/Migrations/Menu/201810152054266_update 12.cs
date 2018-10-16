namespace BogMenu.Migrations.Menu
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "OrderId", "dbo.Ratings");
            DropForeignKey("dbo.MenuProducts", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.Orders", new[] { "OrderId" });
            DropPrimaryKey("dbo.Orders");
            AddColumn("dbo.Orders", "Comment", c => c.String());
            AddColumn("dbo.Orders", "CommentCategory", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Stars", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Orders", "OrderId");
            AddForeignKey("dbo.MenuProducts", "Order_OrderId", "dbo.Orders", "OrderId");
            DropTable("dbo.Ratings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ApplicationUserId = c.String(),
                        Comment = c.String(),
                        CommentCategory = c.Int(nullable: false),
                        Stars = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RatingId);
            
            DropForeignKey("dbo.MenuProducts", "Order_OrderId", "dbo.Orders");
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "Stars");
            DropColumn("dbo.Orders", "CommentCategory");
            DropColumn("dbo.Orders", "Comment");
            AddPrimaryKey("dbo.Orders", "OrderId");
            CreateIndex("dbo.Orders", "OrderId");
            AddForeignKey("dbo.MenuProducts", "Order_OrderId", "dbo.Orders", "OrderId");
            AddForeignKey("dbo.Orders", "OrderId", "dbo.Ratings", "RatingId");
        }
    }
}
