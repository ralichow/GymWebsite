using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GymMembershipManagementSystem.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }


        [DisplayName("Role Name")]
        [Required(ErrorMessage = "Role name is required.")]
        public string RoleName { get; set; }


        //Property Navigation
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}