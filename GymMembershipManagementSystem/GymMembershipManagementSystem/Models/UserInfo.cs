using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMembershipManagementSystem.Models
{
    public class UserInfo
    {
        [Key]
        public int UserInfoID { get; set; }


       // [ForeignKey("User")]
       // public int UserID { get; set; }


        [DisplayName("First Name")]
        [StringLength(75, ErrorMessage = "Maximum limit is 75 characters.")]
        public string FirstName { get; set; }


        [DisplayName("Last Name")]
        [StringLength(75, ErrorMessage = "Maximum limit is 75 characters.")]
        public string LastName { get; set; }


        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email address is required.")]
        public string Email { get; set; }


        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "Password is required.")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Maximum limit is 200 characters.")]
        public string Address { get; set; }


        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01/01/1950", "01/01/2007", ErrorMessage = "Date of Birth must be between 01/01/1950 and 01/01/2007.")]
        [DisplayName("Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }





        //Property Navigation
        public virtual User User { get; set; }
    }
}