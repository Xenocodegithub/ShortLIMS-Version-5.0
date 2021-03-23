using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class PurchaseSupplierDetailModel
    {
        [Key]
        public int PurchaseSupplierID { get; set; }
        public string SupplierName { get; set; }
        public string SupplierContactPerson { get; set; }
        public string SupplierContactNo { get; set; }
        public string SupplierAddress { get; set; }
        public string Product { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> DateOfApproval { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<short> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<System.DateTime> EnteredDate { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public string Remarks { get; set; }
    }
}