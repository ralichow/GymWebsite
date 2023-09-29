using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace GymMembershipManagementSystem.Models
{
    public class MembershipRegistration
    {



        [Key]
        public int RegistrationID { get; set; }


        [ForeignKey("User")]
        public int? UserID { get; set; }


        [ForeignKey("MembershipType")]
        public int MembershipTypeID { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "End date is required.")]
        [EndDateGreaterThanStartDate]
        public DateTime EndDate { get; set; }


        [DisplayName("Active Status")]
        public bool ActiveStatus { get; set; }


        [DisplayName("Note")]
        [StringLength(1000, ErrorMessage = "Maximum limit is 1000 characters.")]
        public string Note { get; set; }


        //Property Navigation
        public virtual User User { get; set; }

        public virtual MembershipType MembershipType { get; set; }
    }
}