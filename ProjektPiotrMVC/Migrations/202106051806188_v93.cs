namespace ProjektPiotrMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v93 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pliks", "Lokalizacja", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pliks", "Lokalizacja", c => c.String(nullable: false));
        }
    }
}
