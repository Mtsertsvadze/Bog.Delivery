namespace BogMenu.Migrations.Menu
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update16 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Company_CompanyId", "dbo.Companies");
            DropIndex("dbo.Orders", new[] { "Company_CompanyId" });
            RenameColumn(table: "dbo.Orders", name: "Company_CompanyId", newName: "CompanyId");
            AlterColumn("dbo.Orders", "CompanyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "CompanyId");
            AddForeignKey("dbo.Orders", "CompanyId", "dbo.Companies", "CompanyId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Orders", new[] { "CompanyId" });
            AlterColumn("dbo.Orders", "CompanyId", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "CompanyId", newName: "Company_CompanyId");
            CreateIndex("dbo.Orders", "Company_CompanyId");
            AddForeignKey("dbo.Orders", "Company_CompanyId", "dbo.Companies", "CompanyId");
        }
    }
}
