namespace BogMenu.Migrations.Menu
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ProductName", c => c.String(maxLength: 450));
            CreateIndex("dbo.Products", "ProductName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", new[] { "ProductName" });
            AlterColumn("dbo.Products", "ProductName", c => c.String());
        }
    }
}
