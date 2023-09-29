using GymMembershipManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GymMembershipManagementSystem.Data
{
    public class GymMembershipManagementSystemContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public GymMembershipManagementSystemContext() : base("name=GymMembershipManagementSystemContext")
        {

        }

        public System.Data.Entity.DbSet<GymMembershipManagementSystem.Models.User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure the one-to-one relationship between User and UserInfo
            modelBuilder.Entity<User>()
                .HasOptional(u => u.UserInfo)  // User is principal
                .WithRequired(ui => ui.User);  // UserInfo is dependent
        }

        public System.Data.Entity.DbSet<GymMembershipManagementSystem.Models.Role> Roles { get; set; }

        public System.Data.Entity.DbSet<GymMembershipManagementSystem.Models.UserRole> UserRoles { get; set; }

        public System.Data.Entity.DbSet<GymMembershipManagementSystem.Models.UserInfo> UserInfoes { get; set; }

        public System.Data.Entity.DbSet<GymMembershipManagementSystem.Models.MembershipType> MembershipTypes { get; set; }

        public System.Data.Entity.DbSet<GymMembershipManagementSystem.Models.MembershipRegistration> MembershipRegistrations { get; set; }

        public System.Data.Entity.DbSet<GymMembershipManagementSystem.Models.ClassEnrollment> ClassEnrollments { get; set; }

        public System.Data.Entity.DbSet<GymMembershipManagementSystem.Models.ClassSchedule> ClassSchedules { get; set; }

        public System.Data.Entity.DbSet<GymMembershipManagementSystem.Models.ClassSession> ClassSessions { get; set; }

        public System.Data.Entity.DbSet<GymMembershipManagementSystem.Models.Report> Reports { get; set; }
    }
}
