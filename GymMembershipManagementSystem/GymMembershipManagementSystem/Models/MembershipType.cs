using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GymMembershipManagementSystem.Models
{
    public class MembershipType
    {
        [Key]
        public int MembershipId { get; set; }


        [DisplayName("Membership Type")]
        [Required(ErrorMessage = "Membership type is required.")]
        [StringLength(100, ErrorMessage = "Maximum limit is 100 characters.")]
        public string MembershipName { get; set; }

        [DataType(DataType.Duration)]
        [DisplayName("Membership Duration (Days)")]
        [Required(ErrorMessage = "Membership duration is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than 0.")]
        public int MembershipDuration { get; set; }


        [DisplayName("Membership Type Description")]
        [StringLength(1000, ErrorMessage = "Maximum limit is 1000 characters.")]
        public string MembershipDescription { get; set; }


        [DisplayName("Fee")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Membership fee is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fee must be greater than 0.")]
        public decimal Fee { get; set; }


        //Property Navigation

        public virtual ICollection<MembershipRegistration> MembershipRegistration { get; set; } 

    }
}