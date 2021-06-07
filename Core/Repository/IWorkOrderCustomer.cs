using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.WorkOrderCustomer;
using LIMS_DEMO.Core.Enquiry;
using Microsoft.SqlServer.Server;

namespace LIMS_DEMO.Core.Repository
{
    public interface IWorkOrderCustomer : IDisposable
    {
       
        string AddCostPercentage(WorkOrderCustomerEntity workOrderCustomerEntity);
        long AddNotification(string Msg, string RoleName, WorkOrderCustomerEntity workOrderCustomerEntity);
        bool DeleteCosting(int CostingId);
        void DeleteSample(long EnquirySampleId, bool isActive);
        string DeleteContract(long EnquiryDetailId);
        long Add(WorkOrderCustomerEntity workOrderCustomerEntity);
        string Update(WorkOrderCustomerEntity workOrderCustomerEntity);
        //string AddContract(WorkOrderSampleEntity entity);
        WorkOrderSampleEntity AddContract(WorkOrderSampleEntity entity);
        List<WorkOrderSampleEntity> GetContractList(int WorkOrderID);
        string UpdateWorkOrderCustomerStatus(long WorkOrderID, int StatusId);
        List<EnquirySampleEntity> GetWorkOrderCustomerSampleList(int WorkOrderID);
        List<TermsAndConditionEntity> GetWorkOrderCustomerTermsAndCondition(int WorkOrderID);
        List<CostingEntity> GetWorkOrderCustomerCostingList(int WorkOrderID);
        WorkOrderEntity GetWorkOrderCustomerDetail(int WorkOrderID);
        List<WorkOrderCustomerEntity> GetWorkOrderCustomerList(int LabMasterId, DateTime? FromDate, DateTime? ToDate);
        void WorkOrderApprove(int WorkOrderId, int iStatusId, int UserId);
        bool AddWorkOrderCosting(WorkOderCostingEntity costingEntity);
        bool UpdateWorkOrderCosting(WorkOderCostingEntity costingEntity);
    }
}
