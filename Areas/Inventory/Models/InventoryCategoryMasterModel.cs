using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class InventoryCategoryMasterModel
    {

        [Key]
        public short CatagoryMasterID { get; set; } // CatagoryMasterId
        [Required(ErrorMessage = "Please enter Category Name")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Please enter InventoryTypeID")]
        public short InventoryTypeID { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string InventoryTypeName { get; set; }

    }
}