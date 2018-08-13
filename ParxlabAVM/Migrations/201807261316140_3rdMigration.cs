namespace ParxlabAVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3rdMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.kullanici", name: "Id", newName: "kullaniciid");
            RenameColumn(table: "dbo.kullanici", name: "PasswordHash", newName: "sifre");
            RenameColumn(table: "dbo.kullanici", name: "PhoneNumber", newName: "telefonnumarasi");
            RenameColumn(table: "dbo.kullanici", name: "UserName", newName: "kullaniciadi");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.kullanici", name: "kullaniciadi", newName: "UserName");
            RenameColumn(table: "dbo.kullanici", name: "telefonnumarasi", newName: "PhoneNumber");
            RenameColumn(table: "dbo.kullanici", name: "sifre", newName: "PasswordHash");
            RenameColumn(table: "dbo.kullanici", name: "kullaniciid", newName: "Id");
        }
    }
}
