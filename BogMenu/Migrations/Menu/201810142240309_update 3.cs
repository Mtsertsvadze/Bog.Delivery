namespace BogMenu.Migrations.Menu
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Companies", "Cost", c => c.Int(nullable: false));
            DropColumn("dbo.Companies", "RegisterDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "RegisterDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Companies", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
