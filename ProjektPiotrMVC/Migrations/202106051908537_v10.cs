namespace ProjektPiotrMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ogloszenies", "Tytul", c => c.String(nullable: false));
            AlterColumn("dbo.Ogloszenies", "Kontent", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ogloszenies", "Kontent", c => c.String());
            AlterColumn("dbo.Ogloszenies", "Tytul", c => c.String());
        }
    }
}
