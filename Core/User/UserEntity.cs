using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.User
{
    public class UserEntity
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool ResetActive { get; set; }
        public int UserDetailID { get; set; }
        public int UserMasterID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string EmployeeCode { get; set; }
        public int DisciplineId { get; set; }
        public int UserRoleID { get; set; }
        public bool IsActive { get; set; }
    }
}