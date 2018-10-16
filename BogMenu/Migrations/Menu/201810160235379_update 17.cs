namespace BogMenu.Migrations.Menu
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Orders", new[] { "CompanyId" });
            CreateTable(
                "dbo.CompanyOrders",
                c => new
                    {
                        Company_CompanyId = c.Int(nullable: false),
                        Order_OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Company_CompanyId, t.Order_OrderId })
                .ForeignKey("dbo.Companies", t => t.Company_CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId, cascadeDelete: true)
                .Index(t => t.Company_CompanyId)
                .Index(t => t.Order_OrderId);
            
            DropColumn("dbo.Orders", "CompanyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "CompanyId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CompanyOrders", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.CompanyOrders", "Company_CompanyId", "dbo.Companies");
            DropIndex("dbo.CompanyOrders", new[] { "Order_OrderId" });
            DropIndex("dbo.CompanyOrders", new[] { "Company_CompanyId" });
            DropTable("dbo.CompanyOrders");
            CreateIndex("dbo.Orders", "CompanyId");
            AddForeignKey("dbo.Orders", "CompanyId", "dbo.Companies", "CompanyId", cascadeDelete: true);
        }
    }
}
