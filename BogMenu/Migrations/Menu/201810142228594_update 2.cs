namespace BogMenu.Migrations.Menu
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "CompanyType", c => c.Int(nullable: false));
            DropColumn("dbo.Companies", "_companyType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "_companyType", c => c.Int(nullable: false));
            DropColumn("dbo.Companies", "CompanyType");
        }
    }
}
