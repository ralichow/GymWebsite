namespace GymMembershipManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FunctionUpdated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        ReportId = c.Int(nullable: false, identity: true),
                        RegistrationId = c.Int(),
                        UserRoleID = c.Int(),
                        ReportName = c.String(nullable: false, maxLength: 150),
                        Description = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.ReportId)
                .ForeignKey("dbo.MembershipRegistrations", t => t.RegistrationId)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleID)
                .Index(t => t.RegistrationId)
                .Index(t => t.UserRoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "UserRoleID", "dbo.UserRoles");
            DropForeignKey("dbo.Reports", "RegistrationId", "dbo.MembershipRegistrations");
            DropIndex("dbo.Reports", new[] { "UserRoleID" });
            DropIndex("dbo.Reports", new[] { "RegistrationId" });
            DropTable("dbo.Reports");
        }
    }
}
