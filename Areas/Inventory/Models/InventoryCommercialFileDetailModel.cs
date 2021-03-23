using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class InventoryCommercialFileDetailModel
    {
        [Key]
        public int ID { get; set; }
        public Nullable<int> InventoryBasicDetailID { get; set; }
        public string FileName { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}