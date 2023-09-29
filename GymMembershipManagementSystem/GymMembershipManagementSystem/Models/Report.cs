using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using GymMembershipManagementSystem.Data;

namespace GymMembershipManagementSystem.Models
{
    public class Report
    {
        private readonly GymMembershipManagementSystemContext _dbContext;

        public Report(GymMembershipManagementSystemContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Key]
        public int ReportId { get; set; }


        [ForeignKey("MembershipRegistration")]
        public int? RegistrationId { get; set; }


        [ForeignKey("UserRole")]
        public int? UserRoleID { get; set; }


        [DisplayName("Report Name")]
        [Required(ErrorMessage = "Report name is required.")]
        [StringLength(150, ErrorMessage = "Maximum limit is 150 characters.")]
        public string ReportName { get; set; }


        [DisplayName("Description")]
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Maximum limit is 1000 characters.")]
        public String Description { get; set; }


        //Function

        public IEnumerable<dynamic> GetMembershipTypeReport()
        {
            // This method returns a report of membership types and the count of members
            // enrolled in each type using anonymous types.
            var membershipTypesReport = _dbContext.MembershipTypes
                .Select(mt => new
                {
                    MembershipType = mt,
                    MemberCount = _dbContext.MembershipRegistrations
                        .Count(mr => mr.MembershipTypeID == mt.MembershipId)
                })
                .ToList();

            return membershipTypesReport;
        }

        public IEnumerable<dynamic> GetMembersByMembershipType(int membershipTypeId)
        {
            // This method returns a report of members enrolled in a specific
            // membership type using anonymous types.
            var membersByType = _dbContext.MembershipRegistrations
                .Where(mr => mr.MembershipTypeID == membershipTypeId)
                .Select(mr => new
                {
                    Member = mr.User,
                    EnrollmentDate = mr.StartDate
                })
                .ToList();

            return membersByType;
        }

        //Property Navigation

        public virtual MembershipRegistration MembershipRegistration { get; set; }

        public virtual UserRole UserRole { get; set; }
    }
}