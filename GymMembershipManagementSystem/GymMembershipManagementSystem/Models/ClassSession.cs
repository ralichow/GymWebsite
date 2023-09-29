using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GymMembershipManagementSystem.Models
{
    public class ClassSession
    {
        [Key]
        public int ClassID { get; set; }


        [DisplayName("Class Name")]
        [Required(ErrorMessage = "Please enter the Class Name.")]
        [StringLength(100, ErrorMessage = "Class Name must be less than 100 characters.")]
        public string ClassName { get; set; }


        [DisplayName("Class Description")]
        [Required(ErrorMessage = "Please enter a class description.")]
        [StringLength(1000, ErrorMessage = "Class description must be less than 100 characters.")]
        public String Description { get; set; }


        //Property Navigation
        public virtual ICollection<ClassSchedule> Schedule { get; set; }
    }
}