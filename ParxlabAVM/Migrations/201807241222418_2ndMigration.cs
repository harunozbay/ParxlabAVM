namespace ParxlabAVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2ndMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetRoles", newName: "yetki");
            RenameTable(name: "dbo.AspNetUserRoles", newName: "kullaniciyetkisi");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.kullaniciyetkisi", newName: "AspNetUserRoles");
            RenameTable(name: "dbo.yetki", newName: "AspNetRoles");
        }
    }
}
