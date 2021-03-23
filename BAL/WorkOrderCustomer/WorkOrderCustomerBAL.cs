using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.WorkOrderCustomer;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.DAL.WorkOrderCustomer;

namespace LIMS_DEMO.BAL.WorkOrderCustomer
{
    public class WorkOrderCustomerBAL
    {
        public WorkOrderCustomerBAL()
        {
            CoreFactory.workOrderCustomer = new WorkOrderCustomerDAL();
        }
        public string AddCostPercentage(WorkOrderCustomerEntity workOrderCustomerEntity)
        {
            return CoreFactory.workOrderCustomer.AddCostPercentage(workOrderCustomerEntity);
        }
        public long AddNotification(string Msg, string RoleName, WorkOrderCustomerEntity workOrderCustomerEntity)
        {
            return CoreFactory.workOrderCustomer.AddNotification(Msg, RoleName,workOrderCustomerEntity);
        }
        public bool DeleteCosting(int CostingId)
        {
            return CoreFactory.workOrderCustomer.DeleteCosting(CostingId);
        }
        public void DeleteSample(long EnquirySampleId, bool isActive)
        {
            CoreFactory.workOrderCustomer.DeleteSample(EnquirySampleId, isActive);
        }
        public string DeleteContract(long EnquiryDetailId)
        {
            return CoreFactory.workOrderCustomer.DeleteContract(EnquiryDetailId);
        }
        public long Add(WorkOrderCustomerEntity workOrderCustomerEntity)
        {
            return CoreFactory.workOrderCustomer.Add(workOrderCustomerEntity);
        }
        public string Update(WorkOrderCustomerEntity workOrderCustomerEntity)
        {
            return CoreFactory.workOrderCustomer.Update(workOrderCustomerEntity);
        }
        //public string AddContract(WorkOrderSampleEntity entity)
        //{
        //    return CoreFactory.workOrderCustomer.AddContract(entity);

        //}
        public WorkOrderSampleEntity AddContract(WorkOrderSampleEntity entity)
        {
            return CoreFactory.workOrderCustomer.AddContract(entity);

        }

        public List<WorkOrderSampleEntity> GetContractList(int WorkOrderID)
        {
            return CoreFactory.workOrderCustomer.GetContractList(WorkOrderID);

        }
        public string UpdateWorkOrderCustomerStatus(long WorkOrderID, int StatusId)
        {
            return CoreFactory.workOrderCustomer.UpdateWorkOrderCustomerStatus(WorkOrderID, StatusId);

        }
        public List<EnquirySampleEntity> GetWorkOrderCustomerSampleList(int WorkOrderID)
        {
            return CoreFactory.workOrderCustomer.GetWorkOrderCustomerSampleList(WorkOrderID);

        }
        public List<TermsAndConditionEntity> GetWorkOrderCustomerTermsAndCondition(int WorkOrderID)
        {
            return CoreFactory.workOrderCustomer.GetWorkOrderCustomerTermsAndCondition(WorkOrderID);

        }
        public List<CostingEntity> GetWorkOrderCustomerCostingList(int WorkOrderID)
        {
            return CoreFactory.workOrderCustomer.GetWorkOrderCustomerCostingList(WorkOrderID);

        }
        public WorkOrderEntity GetWorkOrderCustomerDetail(int WorkOrderID)
        {
            return CoreFactory.workOrderCustomer.GetWorkOrderCustomerDetail(WorkOrderID);

        }
        public List<WorkOrderCustomerEntity> GetWorkOrderCustomerList(int LabMasterId, DateTime? FromDate, DateTime? ToDate)
        {
            return CoreFactory.workOrderCustomer.GetWorkOrderCustomerList(LabMasterId, FromDate, ToDate);

        }
        public void WorkOrderApprove(int WorkOrderId, int iStatusId, int UserId)
        {
             CoreFactory.workOrderCustomer.WorkOrderApprove(WorkOrderId, iStatusId, UserId);

        }
        public bool AddWorkOrderCosting(WorkOderCostingEntity costingEntity)
        {
            return CoreFactory.workOrderCustomer.AddWorkOrderCosting(costingEntity);

        }
        public bool UpdateWorkOrderCosting(WorkOderCostingEntity costingEntity)
        {
           return CoreFactory.workOrderCustomer.UpdateWorkOrderCosting(costingEntity);

        }
    }
}
