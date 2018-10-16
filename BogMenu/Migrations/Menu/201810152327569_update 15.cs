namespace BogMenu.Migrations.Menu
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cards", "OrderId");
            AddForeignKey("dbo.Cards", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "OrderId", "dbo.Orders");
            DropIndex("dbo.Cards", new[] { "OrderId" });
            DropColumn("dbo.Cards", "OrderId");
        }
    }
}
