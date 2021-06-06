namespace ProjektPiotrMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Klasas",
                c => new
                    {
                        KlasaID = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        Rocznik = c.Int(nullable: false),
                        WychowawcaID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.KlasaID)
                .ForeignKey("dbo.AspNetUsers", t => t.WychowawcaID)
                .Index(t => t.WychowawcaID);
            
            CreateTable(
                "dbo.KlasaPrzedmiots",
                c => new
                    {
                        KlasaPrzedmiotID = c.Int(nullable: false, identity: true),
                        KlasaID = c.Int(nullable: false),
                        PrzedmiotID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KlasaPrzedmiotID)
                .ForeignKey("dbo.Klasas", t => t.KlasaID, cascadeDelete: true)
                .ForeignKey("dbo.Przedmiots", t => t.PrzedmiotID, cascadeDelete: true)
                .Index(t => t.KlasaID)
                .Index(t => t.PrzedmiotID);
            
            CreateTable(
                "dbo.Przedmiots",
                c => new
                    {
                        PrzedmiotID = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        ProwadzacyID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PrzedmiotID)
                .ForeignKey("dbo.AspNetUsers", t => t.ProwadzacyID)
                .Index(t => t.ProwadzacyID);
            
            CreateTable(
                "dbo.Pliks",
                c => new
                    {
                        PlikID = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        Lokalizacja = c.String(),
                        PrzedmiotID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlikID)
                .ForeignKey("dbo.Przedmiots", t => t.PrzedmiotID, cascadeDelete: true)
                .Index(t => t.PrzedmiotID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Imie = c.String(),
                        Nazwisko = c.String(),
                        PESEL = c.String(),
                        Mail = c.String(),
                        KlasaID = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Klasa_KlasaID = c.Int(),
                        Klasa_KlasaID1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Klasas", t => t.Klasa_KlasaID)
                .ForeignKey("dbo.Klasas", t => t.Klasa_KlasaID1)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Klasa_KlasaID)
                .Index(t => t.Klasa_KlasaID1);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Ocenas",
                c => new
                    {
                        OcenaID = c.Int(nullable: false, identity: true),
                        Wartosc = c.Double(nullable: false),
                        UczenID = c.String(maxLength: 128),
                        PrzedmiotID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OcenaID)
                .ForeignKey("dbo.Przedmiots", t => t.PrzedmiotID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UczenID)
                .Index(t => t.UczenID)
                .Index(t => t.PrzedmiotID);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UczenRodzics",
                c => new
                    {
                        UczenRodzicID = c.Int(nullable: false, identity: true),
                        UczenID = c.String(maxLength: 128),
                        RodzicID = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UczenRodzicID)
                .ForeignKey("dbo.AspNetUsers", t => t.RodzicID)
                .ForeignKey("dbo.AspNetUsers", t => t.UczenID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.UczenID)
                .Index(t => t.RodzicID)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Ogloszenies",
                c => new
                    {
                        OgloszenieID = c.Int(nullable: false, identity: true),
                        Tytul = c.String(),
                        Kontent = c.String(),
                        DataDodania = c.DateTime(nullable: false),
                        AutorID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OgloszenieID)
                .ForeignKey("dbo.AspNetUsers", t => t.AutorID)
                .Index(t => t.AutorID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Ogloszenies", "AutorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Klasas", "WychowawcaID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Klasa_KlasaID1", "dbo.Klasas");
            DropForeignKey("dbo.Przedmiots", "ProwadzacyID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UczenRodzics", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UczenRodzics", "UczenID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UczenRodzics", "RodzicID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ocenas", "UczenID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ocenas", "PrzedmiotID", "dbo.Przedmiots");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Klasa_KlasaID", "dbo.Klasas");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pliks", "PrzedmiotID", "dbo.Przedmiots");
            DropForeignKey("dbo.KlasaPrzedmiots", "PrzedmiotID", "dbo.Przedmiots");
            DropForeignKey("dbo.KlasaPrzedmiots", "KlasaID", "dbo.Klasas");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Ogloszenies", new[] { "AutorID" });
            DropIndex("dbo.UczenRodzics", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UczenRodzics", new[] { "RodzicID" });
            DropIndex("dbo.UczenRodzics", new[] { "UczenID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Ocenas", new[] { "PrzedmiotID" });
            DropIndex("dbo.Ocenas", new[] { "UczenID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Klasa_KlasaID1" });
            DropIndex("dbo.AspNetUsers", new[] { "Klasa_KlasaID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Pliks", new[] { "PrzedmiotID" });
            DropIndex("dbo.Przedmiots", new[] { "ProwadzacyID" });
            DropIndex("dbo.KlasaPrzedmiots", new[] { "PrzedmiotID" });
            DropIndex("dbo.KlasaPrzedmiots", new[] { "KlasaID" });
            DropIndex("dbo.Klasas", new[] { "WychowawcaID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Ogloszenies");
            DropTable("dbo.UczenRodzics");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Ocenas");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Pliks");
            DropTable("dbo.Przedmiots");
            DropTable("dbo.KlasaPrzedmiots");
            DropTable("dbo.Klasas");
        }
    }
}
