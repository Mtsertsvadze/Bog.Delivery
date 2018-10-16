namespace BogMenu.Migrations.Menu
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "MenuType", c => c.Int(nullable: false));
            DropColumn("dbo.Menus", "_menuType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Menus", "_menuType", c => c.Int(nullable: false));
            DropColumn("dbo.Menus", "MenuType");
        }
    }
}
