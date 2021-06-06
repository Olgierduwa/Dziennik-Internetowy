namespace ProjektPiotrMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ogloszenies", "Prywatne", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ogloszenies", "Prywatne");
        }
    }
}
