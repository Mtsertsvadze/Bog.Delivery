namespace BogMenu.Migrations.Menu
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Menus", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Menus", "Name", c => c.String());
        }
    }
}
