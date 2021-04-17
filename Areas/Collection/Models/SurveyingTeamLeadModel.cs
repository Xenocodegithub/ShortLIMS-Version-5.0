using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace LIMS_DEMO.Areas.Collection.Models
{
    public class SurveyingTeamLeadModel
    {
      
        public int WorkOrderSampleCollectionDateId { get; set; }
        public int LocationSampleCollectionID { get; set; }
        public long SampleCollectionId { get; set; }//SampleCollection tbl
        public string DisplaySampleName { get; set; }
        public int WorkOrderID { get; set; }//SampleCollection tbl
        [Display(Name = "WorkOrder No.")]
        public string WorkOrderNo { get; set; }// WorkOrder tbl
        public int ProductGroupId { get; set; }//SampleCollection tbl
        [Display(Name = "ProductGroup Name")]
        public string ProductGroupName { get; set; }// WorkOrder tbl
        [Display(Name = " Sample to be Collected On")]
        public string CollectedOn { get; set; }
        [Display(Name = "SampleTypeProduct Name")]
        public string SampleTypeProductName { get; set; }
        public int SubGroupId { get; set; }
        [Display(Name = "ProductGroup Name")]
        public string SubGroupName { get; set; }
        public int MatrixId { get; set; }
        public int SampleTypeProductId { get; set; }
        [Display(Name = "Matrix Name")]
        public string MatrixName { get; set; }
        public System.DateTime ExpectSampleCollDate { get; set; }// WorkOrder tbl
        public long EnquirySampleID { get; set; }//SampleCollection tbl
        public byte StatusId { get; set; }//SampleCollection tbl

        [Display(Name = "Status")]
        public string CurrentStatus { get; set; }// StatusMaster tbls
        public string StatusCode { get; set; }
        public Nullable<int> FieldDeterminationId { get; set; }//SampleCollection tbl

        [Display(Name = "Iteration")]
        public Nullable<int> Iteration { get; set; }//SampleCollection tbl
        public Nullable<bool> IsWitness { get; set; }//SampleCollection tbl
        public string WitnessName { get; set; }//SampleCollection tbl
        public string SampleNo { get; set; }//SampleCollection tbl
        public bool IsSampleIntact { get; set; }//SampleCollection tbl
        public string IndustryType { get; set; }//SampleCollection tbl
        public string Source { get; set; }//SampleCollection tbl
        public string Location { get; set; }//SampleCollection tbl
        public string RequestNo { get; set; }//SampleCollection tbl
        public string ULRNo { get; set; }//SampleCollection tbl
        public bool IsActive { get; set; }//SampleCollection tbl
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public long EnquiryDetailId { get; set; }//EnquirySampleDetail tbl
        public long EnquiryId { get; set; }
        public int CustomerMasterId { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Sample No.")]
        public string SampleName { get; set; }//EnquirySampleDetail tbl
        public Nullable<byte> FrequencyMasterId { get; set; }//EnquirySampleDetail tbl

        [Display(Name = "Frequency")]
        public string Frequency { get; set; }//FrequencyMaster tbl
        public Nullable<int> SampleCollectedBy { get; set; }//EnquirySampleDetail tbl

        [Display(Name = "Collected By")]
        public string CollectedBy { get; set; }

        [Required(ErrorMessage = "Please Select Collector By")]
        public string EmployeeId { get; set; }//EnquirySampleDetail tbl Set Collector Name
        public int UserRoleId { get; set; }//tbl UserRole
        public int UserMasterId { get; set; }//tbl UserRole
        public int RoleId { get; set; }//tbl UserRole,RoleMaster
        public string RoleName { get; set; }//tbl RoleMaster
        //public int UserMasterID { get; set; }//tbl UserMaster
        public string UserName { get; set; }//tbl UserMaster

        public Nullable<System.DateTime> CollectionDate { get; set; }
       

    }
}