using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class TermsAndConditionPurchaseModel
    {
        public int InventoryTypeID { get; set; }
        public int PurchaseRequestID { get; set; }
        public int PurchaseMasterID { get; set; }
        public int TermAndCondtId { get; set; }
        public string TermAndCondt { get; set; }
        public bool IsSelected { get; set; }
        public int Count { get; set; }
    }
}