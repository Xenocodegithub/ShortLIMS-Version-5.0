using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.DAL.Enquiry;
using LIMS_DEMO.Core.User;

namespace LIMS_DEMO.BAL.Enquiry
{
    public class WorkOrderBAL
    {
        public WorkOrderBAL()
        {
            CoreFactory.workOrder = new WorkOrderDAL();
        }

        public List<WorkOrderHODEntity> GetTRF_WOList(int LabMasterId, DateTime? FromDate, DateTime? ToDate)
        {
            return CoreFactory.workOrder.GetTRF_WOList(LabMasterId, FromDate, ToDate);
        }
        public WorkOrderEntity GetLastDay(string monthName)
        {
            return CoreFactory.workOrder.GetLastDay(monthName);
        }
        public List<WorkOrderEntity> GetSampleLocationCount(int EnquirySampleID)
        {
            return CoreFactory.workOrder.GetSampleLocationCount(EnquirySampleID);
        }
        public List<WorkOrderEntity> GetLocation(int EnquirySampleID)
        {
            return CoreFactory.workOrder.GetLocation(EnquirySampleID);
        }
        public WorkOrderEntity GetDetail(int WorkOrderId)
        {
            return CoreFactory.workOrder.GetDetail(WorkOrderId);

        }
        public long AddNotification(string Msg,string RoleName, WorkOrderEntity workOrder)
        {
            return CoreFactory.workOrder.AddNotification(Msg,RoleName,workOrder);

        }
        public int? GetFrequencyDetails(int FrequencyMasterId)
        {
            return CoreFactory.workOrder.GetFrequencyDetails(FrequencyMasterId);

        }
        public long AddLocation(EnquiryParameterEntity enquiryParameterEntity)
        {
            return CoreFactory.workOrder.AddLocation(enquiryParameterEntity);
        }
        public long AddWorkOrder(WorkOrderEntity workOrder)
        {
            return CoreFactory.workOrder.AddWorkOrder(workOrder);
        }
        //public bool AddWorkOrder(WorkOrderEntity workOrder)
        //{
        //    return CoreFactory.workOrder.AddWorkOrder(workOrder);
        //}
        public bool UpdateWorkOrder(WorkOrderEntity workOrder)
        {
            return CoreFactory.workOrder.UpdateWorkOrder(workOrder);
        }

        public long AddWOSampleCollectionDate(WorkOrderEntity workOrder)
        {
            return CoreFactory.workOrder.AddWOSampleCollectionDate(workOrder);
        }
        public long AddWOMasterCollDate(bool IsActive, long EnquirySampleID)
        {
            return CoreFactory.workOrder.AddWOMasterCollDate(IsActive, EnquirySampleID);
        }
        //public bool UpdateWOSampleCollectionDate(WorkOrderEntity workOrder)
        //{
        //    return CoreFactory.workOrder.UpdateWOSampleCollectionDate(workOrder);
        //}
        public void UpdateEnquirySampleDetail(IList<EnquirySampleEntity> enquirySamples)
        {
            CoreFactory.workOrder.UpdateEnquirySampleDetail(enquirySamples);
        }

        public WorkOrderEntity GetWorkOrderDetail(int EnquiryId)
        {
            return CoreFactory.workOrder.GetWorkOrderDetail(EnquiryId);
        }
        public WorkOrderEntity GetWOSampleCollectionDateDetail(int WorkOrderId)
        {
            return CoreFactory.workOrder.GetWOSampleCollectionDateDetail(WorkOrderId);

        }
        public List<WorkOrderHODEntity> GetWorkOrderList(int LabMasterId, DateTime? FromDate, DateTime? ToDate)
        {
            return CoreFactory.workOrder.GetWorkOrderList(LabMasterId, FromDate, ToDate);
        }

        //public void WorkOrderApprove(int WorkOrderId, long EnquiryId, int AssignToId, int iStatusId, int UserId)
        //{
        //    CoreFactory.workOrder.WorkOrderApprove(WorkOrderId, EnquiryId, AssignToId, iStatusId, UserId);
        //}
        public void WorkOrderApprove(int WorkOrderId, long EnquiryId, int iStatusId, int UserId)
        {
            CoreFactory.workOrder.WorkOrderApprove(WorkOrderId, EnquiryId, iStatusId, UserId);

        }
        public void AssignSTL(int WorkOrderId, int AssignToId, int UserId)
        {
            CoreFactory.workOrder.AssignSTL(WorkOrderId, AssignToId, UserId);

        }
        public void WorkOrderReject(int WorkOrderId, string Remark, int UserId)
        {
            CoreFactory.workOrder.WorkOrderReject(WorkOrderId, Remark, UserId);
        }

        public List<ParameterPCBEntity> GetParameterPCBList(long EnquirySampleId)
        {
            return CoreFactory.workOrder.GetParameterPCBList(EnquirySampleId);
        }

        public void UpdateParameterPCBLimit(List<ParameterPCBEntity> PCBLimits, int UserId)
        {
            CoreFactory.workOrder.UpdateParameterPCBLimit(PCBLimits, UserId);
        }
        public string UpdateRemark(long EnquiryId, string Remark)
        {
            return CoreFactory.workOrder.UpdateRemark(EnquiryId, Remark);
        }
        public UserRoleEntity GetUserRoles(string strUserName)
        {
            return CoreFactory.workOrder.GetUserRoles(strUserName);
        }
    }
}
