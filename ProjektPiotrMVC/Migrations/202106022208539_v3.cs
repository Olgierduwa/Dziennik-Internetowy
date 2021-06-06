namespace ProjektPiotrMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Wiadomoscs",
                c => new
                    {
                        WiadomoscID = c.Int(nullable: false, identity: true),
                        Tresc = c.String(),
                        DataDodania = c.DateTime(nullable: false),
                        NadawcaID = c.String(maxLength: 128),
                        OdbiorcaID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.WiadomoscID)
                .ForeignKey("dbo.AspNetUsers", t => t.NadawcaID)
                .ForeignKey("dbo.AspNetUsers", t => t.OdbiorcaID)
                .Index(t => t.NadawcaID)
                .Index(t => t.OdbiorcaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wiadomoscs", "OdbiorcaID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Wiadomoscs", "NadawcaID", "dbo.AspNetUsers");
            DropIndex("dbo.Wiadomoscs", new[] { "OdbiorcaID" });
            DropIndex("dbo.Wiadomoscs", new[] { "NadawcaID" });
            DropTable("dbo.Wiadomoscs");
        }
    }
}
