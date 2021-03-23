using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class StockLogDataModel
    {
        [Key]
        public long ID { get; set; }
        public Nullable<long> IssueToNameID { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public Nullable<int> IssueQty { get; set; }
        public Nullable<long> ItemID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedTime { get; set; }
    }
}