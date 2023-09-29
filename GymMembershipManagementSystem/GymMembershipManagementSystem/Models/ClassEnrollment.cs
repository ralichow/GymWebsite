using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymMembershipManagementSystem.Models
{
    public class ClassEnrollment
    {
        [Key]
        public int EnrollmentId { get; set; }


        [ForeignKey("ClassSchedule")]
        public int? ScheduleID { get; set; }


        [ForeignKey("User")]
        public int? UserID { get; set; }



        //Property Navigation
        public virtual User User { get; set; }
        public virtual ClassSchedule ClassSchedule { get; set; }
    }
}