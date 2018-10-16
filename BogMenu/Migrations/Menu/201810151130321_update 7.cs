namespace BogMenu.Migrations.Menu
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menus", "Name");
        }
    }
}
