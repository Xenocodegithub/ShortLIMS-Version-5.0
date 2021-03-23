using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class InventoryItemMasterModel
    {
        [Key]
        public int ItemMasterID { get; set; } // ItemMasterId =ID

        [Required(ErrorMessage = "Please enter CatagoryMasterID")]
        public Nullable<int> CatagoryMasterID { get; set; } // CatagoryMasterId
        public Nullable<int> CategoryID { get; set; } // CatagoryMasterId
        
       // public int CatagoryMasterID { get; set; } // CatagoryMasterId

        [Required(ErrorMessage = "Please enter Category Name")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Please enter InventoryTypeID")]
        public Nullable<short> InventoryTypeID { get; set; } //InventoryTypeID
        public Nullable<short> TypeID { get; set; } //InventoryTypeID
        [Required(ErrorMessage = "Please enter InventoryTypeName")]
        public string InventoryTypeName { get; set; }
        [Required(ErrorMessage = "Please enter NameOfStock ")]
        public string NameOfStock { get; set; }
        [Required(ErrorMessage = "Please enter Code ")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Please enter UnitID ")]
        public Nullable<short> UnitID { get; set; }
        [Required(ErrorMessage = "Please enter Unit ")]
        public string Unit { get; set; }
        public Nullable<decimal> MinimumStock { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<short> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<short> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<short> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string  AvailableStock { get; set; }
        [Required(ErrorMessage = "Please enter Capacity ID ")]
        public Nullable<long> InventoryCapacityMasterId { get; set; }
        [Required(ErrorMessage = "Please enter Capacity")]
        public string Capacity { get; set; }
        public int ItemID { get; set; }
        public int EmpId { get; set; }


        // for stock log data
        public int IssueToID { get; set; }
        public int IssueToNameID { get; set; }
        [Required(ErrorMessage = "Please enter Issue Date")]
        public Nullable<System.DateTime> IssueDate { get; set; }
        [Required(ErrorMessage = "Please enter Issue Quantity")]
        public int IssueQty { get; set; }
        public string EmpName { get; set; }

    }
}