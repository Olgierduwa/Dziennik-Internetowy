namespace ProjektPiotrMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pliks", "Format", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pliks", "Format");
        }
    }
}
