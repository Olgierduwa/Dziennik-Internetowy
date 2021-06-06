namespace ProjektPiotrMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Klasas", "Nazwa", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Klasas", "Nazwa", c => c.String());
        }
    }
}
