using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymMembershipManagementSystem.Models
{
    public class ClassSchedule
    {
        [Key]
        public int ScheduleID { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Please provide a start date for the class")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [Required(ErrorMessage = "Please provide an end date.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Compare("StartDate", ErrorMessage = "End date must be in the future of the start date.")]
        public DateTime EndDate { get; set; }


        [DataType(DataType.Duration)]
        [Display(Name = "Class Duration")]
        [Range(0, 365, ErrorMessage = "Class duration should be between 0 and 365 days.")]
        [Required(ErrorMessage = "Please provide a duration in days.")]
        public int ClassDuration { get; set; }


        [Display(Name = "Class Capacity")]
        public int Capacity { get; set; }



        [ForeignKey("ClassSession")]
        public int ClassID { get; set; }


        [ForeignKey("UserRole")]
        public int? UserRoleID { get; set; }


        //Property Navigation

        public virtual ICollection<ClassEnrollment> Enrollment { get; set; }
        public virtual UserRole UserRole { get; set; }
        public virtual ClassSession ClassSession { get; set; }
    }
}