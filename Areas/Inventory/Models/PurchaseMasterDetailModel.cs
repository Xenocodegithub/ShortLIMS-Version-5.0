using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class PurchaseMasterDetailModel
    {
        [Key]
        public int PurchaseMasterID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<short> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<System.DateTime> EnteredDate { get; set; }


    }
}