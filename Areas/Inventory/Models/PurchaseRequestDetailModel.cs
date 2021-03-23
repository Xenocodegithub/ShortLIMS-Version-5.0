﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class PurchaseRequestDetailModel
    {
        [Key]
        public int PurchaseRequestID { get; set; }
        public Nullable<int> ItemMasterID { get; set; }
        public string Brand { get; set; }
        public string Grade { get; set; }
        public string PackSize { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<short> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<System.DateTime> EnteredDate { get; set; }
        public string Priority { get; set; }
        public string Specification { get; set; }
        public string Purpose { get; set; }
        public string Remark { get; set; }
        public string EstimatedLagTime { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<bool> TickIf { get; set; }
        public string COA_COC { get; set; }
        public Nullable<int> PurchaseSupplierID { get; set; }
        public string Comment { get; set; }
        public Nullable<int> Approvests { get; set; }
        public Nullable<int> CurrentStatus { get; set; }
        public string PONumber { get; set; }
        public Nullable<int> ApprovePO { get; set; }
        public Nullable<int> ApproveStatus { get; set; }
        public Nullable<int> PurchaseMasterID { get; set; }
        public Nullable<int> RecordStatus { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> GSTAmount { get; set; }
        public Nullable<decimal> DiscAmount { get; set; }
        public Nullable<decimal> NetAmount { get; set; }
        public Nullable<short> InventoryTypeID { get; set; }
        public string FileName { get; set; }
        public string PurchaseUpload { get; set; }
        public Nullable<decimal> DiscPercent { get; set; }
        public Nullable<decimal> GSTPercent { get; set; }
        public string CatagoryName { get; set; }
        public Nullable<bool> IsIGST { get; set; }
    }
}