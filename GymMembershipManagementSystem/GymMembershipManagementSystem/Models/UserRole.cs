using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymMembershipManagementSystem.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleID { get; set; }


        [ForeignKey("Role")]
        public int? RoleID { get; set; }


        [ForeignKey("User")]
        public int? UserID { get; set; }

        //Property Navigation

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<ClassSchedule> Schedule { get; set; }
    }
}