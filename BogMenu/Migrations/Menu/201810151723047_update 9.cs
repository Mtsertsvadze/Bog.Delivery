namespace BogMenu.Migrations.Menu
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Companies", "Name", c => c.String(maxLength: 450));
            CreateIndex("dbo.Companies", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Companies", new[] { "Name" });
            AlterColumn("dbo.Companies", "Name", c => c.String());
        }
    }
}
