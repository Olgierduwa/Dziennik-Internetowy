namespace ProjektPiotrMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ogloszenies", "DataDodania", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "Mail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Mail", c => c.String());
            DropColumn("dbo.Ogloszenies", "DataDodania");
        }
    }
}
