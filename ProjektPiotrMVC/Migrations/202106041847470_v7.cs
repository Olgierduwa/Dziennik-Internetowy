namespace ProjektPiotrMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Przedmiots", "ProwadzacyID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Przedmiots", "ProwadzacyID");
            AddForeignKey("dbo.Przedmiots", "ProwadzacyID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Przedmiots", "ProwadzacyID", "dbo.AspNetUsers");
            DropIndex("dbo.Przedmiots", new[] { "ProwadzacyID" });
            AlterColumn("dbo.Przedmiots", "ProwadzacyID", c => c.String());
        }
    }
}
