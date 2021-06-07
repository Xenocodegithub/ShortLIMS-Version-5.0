using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.DropDowns
{
    public class UserListEntity
    {
        public Nullable<int> UserMasterID { get; set; }
        public string UserName { get; set; }
        public bool ResetActive { get; set; }
        public string PhoneNo { get; set; }
    }
}
