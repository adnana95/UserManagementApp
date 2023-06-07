namespace UserManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredAttributesAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PermissionModels", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.UserModels", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.UserModels", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.UserModels", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.UserModels", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserModels", "Password", c => c.String());
            AlterColumn("dbo.UserModels", "Username", c => c.String());
            AlterColumn("dbo.UserModels", "LastName", c => c.String());
            AlterColumn("dbo.UserModels", "FirstName", c => c.String());
            AlterColumn("dbo.PermissionModels", "Code", c => c.String());
        }
    }
}
