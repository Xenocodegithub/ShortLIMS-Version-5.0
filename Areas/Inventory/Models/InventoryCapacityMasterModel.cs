using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class InventoryCapacityMasterModel
    {
        [Key]
        public long InventoryCapacityMasterId { get; set; }
        [Required(ErrorMessage = "Please enter Capacity Name")]
        public string Capacity { get; set; }
        [Required(ErrorMessage = "Please enter Capacity Description")]
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
}