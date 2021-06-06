namespace ProjektPiotrMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v91 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pliks", "Nazwa", c => c.String(nullable: false));
            AlterColumn("dbo.Pliks", "Lokalizacja", c => c.String(nullable: false));
            AlterColumn("dbo.Pliks", "Format", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pliks", "Format", c => c.String());
            AlterColumn("dbo.Pliks", "Lokalizacja", c => c.String());
            AlterColumn("dbo.Pliks", "Nazwa", c => c.String());
        }
    }
}
