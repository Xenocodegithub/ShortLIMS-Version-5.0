using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Collection
{
    public class SurveyingTeamLeadEntity
    {
        public int LocationSampleCollectionID { get; set; }
        public int ProductGroupId { get; set; }
        public int SubGroupId { get; set; }
        public string ProductGroupName { get; set; }
        public string SampleTypeProductName { get; set; }
        public int SampleTypeProductId { get; set; }
        public string SubGroupName { get; set; }
        public long SampleCollectionId { get; set; }//SampleCollection tbl
        public int WorkOrderID { get; set; }//SampleCollection tbl
        public string WorkOrderNo { get; set; }// WorkOrder tbl
        public Nullable<System.DateTime> ExpectSampleCollDate { get; set; }
        public string DisplaySampleName { get; set; }
        //public System.DateTime ExpectSampleCollDate { get; set; }// WorkOrder tbl
        public long EnquirySampleID { get; set; }//SampleCollection tbl
        public byte StatusId { get; set; }//SampleCollection tbl
        public string CurrentStatus { get; set; }// StatusMaster tbl
        public string StatusCode { get; set; }
        public Nullable<int> FieldDeterminationId { get; set; }//SampleCollection tbl
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
        public string CustomerName { get; set; }
        public string SampleName { get; set; }//EnquirySampleDetail tbl
        public Nullable<byte> FrequencyMasterId { get; set; }//EnquirySampleDetail tbl
        public string Frequency { get; set; }//FrequencyMaster tbl
        public Nullable<int> SampleCollectedBy { get; set; }//EnquirySampleDetail tbl
        public string EmployeeId { get; set; }//EnquirySampleDetail tbl Set Collector Name
        public int UserRoleId { get; set; }//tbl UserRole
        public int UserMasterId { get; set; }//tbl UserRole
        public int RoleId { get; set; }//tbl UserRole,RoleMaster
        public string RoleName { get; set; }//tbl RoleMaster
        public int UserMasterID { get; set; }//tbl UserMaster
        public string UserName { get; set; }//tbl UserMaster
        public Nullable<System.DateTime> CollectionDate { get; set; }

    }
}
