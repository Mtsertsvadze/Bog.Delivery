namespace BogMenu.Migrations.Menu
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        CardId = c.Int(nullable: false, identity: true),
                        CardNumber = c.String(maxLength: 16),
                        ValidationYear = c.Int(nullable: false),
                        ValidationMonth = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CardId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cards");
        }
    }
}
