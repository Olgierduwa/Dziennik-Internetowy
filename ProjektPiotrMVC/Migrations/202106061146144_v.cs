namespace ProjektPiotrMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UczenRodzics",
                c => new
                    {
                        UczenRodzicID = c.Int(nullable: false, identity: true),
                        UczenID = c.String(maxLength: 128),
                        RodzicID = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UczenRodzicID)
                .ForeignKey("dbo.AspNetUsers", t => t.RodzicID)
                .ForeignKey("dbo.AspNetUsers", t => t.UczenID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.UczenID)
                .Index(t => t.RodzicID)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UczenRodzics", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UczenRodzics", "UczenID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UczenRodzics", "RodzicID", "dbo.AspNetUsers");
            DropIndex("dbo.UczenRodzics", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UczenRodzics", new[] { "RodzicID" });
            DropIndex("dbo.UczenRodzics", new[] { "UczenID" });
            DropTable("dbo.UczenRodzics");
        }
    }
}
