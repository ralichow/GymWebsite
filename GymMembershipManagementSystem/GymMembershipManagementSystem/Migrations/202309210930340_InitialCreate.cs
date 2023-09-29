namespace GymMembershipManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        IsStaff = c.Boolean(nullable: true),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.ClassEnrollments",
                c => new
                    {
                        EnrollmentId = c.Int(nullable: false, identity: true),
                        ScheduleID = c.Int(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.EnrollmentId)
                .ForeignKey("dbo.ClassSchedules", t => t.ScheduleID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.ScheduleID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.ClassSchedules",
                c => new
                    {
                        ScheduleID = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ClassDuration = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                        ClassID = c.Int(nullable: false),
                        UserRoleID = c.Int(),
                    })
                .PrimaryKey(t => t.ScheduleID)
                .ForeignKey("dbo.ClassSessions", t => t.ClassID, cascadeDelete: true)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleID)
                .Index(t => t.ClassID)
                .Index(t => t.UserRoleID);
            
            CreateTable(
                "dbo.ClassSessions",
                c => new
                    {
                        ClassID = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.ClassID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.UserRoleID)
                .ForeignKey("dbo.Roles", t => t.RoleID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.RoleID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        MembershipId = c.Int(nullable: false, identity: true),
                        MembershipName = c.String(nullable: false, maxLength: 100),
                        MembershipDuration = c.Int(nullable: false),
                        MembershipDescription = c.String(maxLength: 1000),
                        Fee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.MembershipId)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .Index(t => t.User_UserID);
            
            CreateTable(
                "dbo.MembershipRegistrations",
                c => new
                    {
                        RegistrationID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(),
                        MembershipTypeID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ActiveStatus = c.Boolean(nullable: false),
                        Note = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.RegistrationID)
                .ForeignKey("dbo.MembershipTypes", t => t.MembershipTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.MembershipTypeID);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        UserInfoID = c.Int(nullable: false),
                        FirstName = c.String(maxLength: 75),
                        LastName = c.String(maxLength: 75),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 200),
                        DOB = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserInfoID)
                .ForeignKey("dbo.Users", t => t.UserInfoID)
                .Index(t => t.UserInfoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInfoes", "UserInfoID", "dbo.Users");
            DropForeignKey("dbo.MembershipTypes", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.MembershipRegistrations", "UserID", "dbo.Users");
            DropForeignKey("dbo.MembershipRegistrations", "MembershipTypeID", "dbo.MembershipTypes");
            DropForeignKey("dbo.ClassEnrollments", "UserID", "dbo.Users");
            DropForeignKey("dbo.ClassEnrollments", "ScheduleID", "dbo.ClassSchedules");
            DropForeignKey("dbo.ClassSchedules", "UserRoleID", "dbo.UserRoles");
            DropForeignKey("dbo.UserRoles", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.ClassSchedules", "ClassID", "dbo.ClassSessions");
            DropIndex("dbo.UserInfoes", new[] { "UserInfoID" });
            DropIndex("dbo.MembershipRegistrations", new[] { "MembershipTypeID" });
            DropIndex("dbo.MembershipRegistrations", new[] { "UserID" });
            DropIndex("dbo.MembershipTypes", new[] { "User_UserID" });
            DropIndex("dbo.UserRoles", new[] { "UserID" });
            DropIndex("dbo.UserRoles", new[] { "RoleID" });
            DropIndex("dbo.ClassSchedules", new[] { "UserRoleID" });
            DropIndex("dbo.ClassSchedules", new[] { "ClassID" });
            DropIndex("dbo.ClassEnrollments", new[] { "UserID" });
            DropIndex("dbo.ClassEnrollments", new[] { "ScheduleID" });
            DropTable("dbo.UserInfoes");
            DropTable("dbo.MembershipRegistrations");
            DropTable("dbo.MembershipTypes");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.ClassSessions");
            DropTable("dbo.ClassSchedules");
            DropTable("dbo.ClassEnrollments");
            DropTable("dbo.Users");
        }
    }
}
