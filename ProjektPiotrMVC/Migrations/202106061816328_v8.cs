namespace ProjektPiotrMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ocenas", "DataDodania", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ocenas", "DataDodania");
        }
    }
}
