using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace GymMembershipManagementSystem.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }


        [DisplayName("User Name")]
        [Required(ErrorMessage = "User name is required.")]
        [StringLength(50, ErrorMessage = "Maximum limit is 50 characters.")]
        public string UserName { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }


        [DisplayName("Is Staff")]
        public bool IsStaff { get; set; }


        //Function

        public static string HashPassword(string password)
        {
            using (var md5Hash = MD5.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = md5Hash.ComputeHash(sourceBytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder hashBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashBuilder.Append(b.ToString("x2"));
                }

                return hashBuilder.ToString();
            }
        }

        //Property Navigation
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<MembershipType> MembershipType { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual ICollection<ClassEnrollment> ClassEnrollment { get; set; } = null;


        
        }

    }
