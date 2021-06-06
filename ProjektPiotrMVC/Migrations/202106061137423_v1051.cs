namespace ProjektPiotrMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1051 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UczenRodzics", "RodzicID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UczenRodzics", "UczenID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UczenRodzics", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UczenRodzics", new[] { "UczenID" });
            DropIndex("dbo.UczenRodzics", new[] { "RodzicID" });
            DropIndex("dbo.UczenRodzics", new[] { "ApplicationUser_Id" });
            DropTable("dbo.UczenRodzics");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.UczenRodzicID);
            
            CreateIndex("dbo.UczenRodzics", "ApplicationUser_Id");
            CreateIndex("dbo.UczenRodzics", "RodzicID");
            CreateIndex("dbo.UczenRodzics", "UczenID");
            AddForeignKey("dbo.UczenRodzics", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.UczenRodzics", "UczenID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.UczenRodzics", "RodzicID", "dbo.AspNetUsers", "Id");
        }
    }
}
