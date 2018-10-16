namespace BogMenu.Migrations.Menu
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "CommentCategory", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "ApplicationUserId", c => c.String());
            AlterColumn("dbo.Ratings", "ApplicationUserId", c => c.String());
            DropColumn("dbo.Ratings", "CommentCateroty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "CommentCateroty", c => c.Int(nullable: false));
            AlterColumn("dbo.Ratings", "ApplicationUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "ApplicationUserId", c => c.Int(nullable: false));
            DropColumn("dbo.Ratings", "CommentCategory");
        }
    }
}
