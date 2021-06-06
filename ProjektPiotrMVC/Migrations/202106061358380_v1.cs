namespace ProjektPiotrMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ocenas", "Kategoria", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ocenas", "Kategoria");
        }
    }
}
