using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace LIMS_DEMO.Core.User
{
    public class NotificationEntity
    {
        public int NotificationDetailId { get; set; }
        public Nullable<long> RoleId { get; set; }
        public string NotificationName { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public string LastUpdated { get; set; }
        public string DisplayDate { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; }
        public System.DateTime EnteredDate { get; set; }

    }
}
