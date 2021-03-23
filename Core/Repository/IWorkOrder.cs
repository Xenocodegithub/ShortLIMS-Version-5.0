using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Enquiry;

namespace LIMS_DEMO.Core.Repository
{
    public interface IWorkOrder : IDisposable
    {
        WorkOrderEntity GetLastDay(string monthName);
        List<WorkOrderEntity> GetSampleLocationCount(int EnquirySampleID);
        List<WorkOrderEntity> GetLocation(int EnquirySampleID);
        WorkOrderEntity GetDetail(int WorkOrderId);
        long AddNotification(string Msg,string RoleName, WorkOrderEntity workOrder);
        int? GetFrequencyDetails(int FrequencyMasterId);
        long AddLocation(EnquiryParameterEntity enquiryParameterEntity);
        //bool AddWorkOrder(WorkOrderEntity workOrder);
        long AddWorkOrder(WorkOrderEntity workOrder);
        bool UpdateWorkOrder(WorkOrderEntity workOrder);
        long AddWOSampleCollectionDate(WorkOrderEntity workOrder);
        long AddWOMasterCollDate(bool IsActive, long EnquirySampleID);
        //bool UpdateWOSampleCollectionDate(WorkOrderEntity workOrder);
        void UpdateEnquirySampleDetail(IList<EnquirySampleEntity> enquirySamples);
        WorkOrderEntity GetWorkOrderDetail(int EnquiryId);
        WorkOrderEntity GetWOSampleCollectionDateDetail(int WorkOrderId);
        List<WorkOrderHODEntity> GetWorkOrderList(int LabMasterId, DateTime? FromDate, DateTime? ToDate);
        //void WorkOrderApprove(int WorkOrderId, long EnquiryId, int AssignToId, int iStatusId, int UserId);
        void WorkOrderApprove(int WorkOrderId, long EnquiryId, int iStatusId, int UserId);
        void AssignSTL(int WorkOrderId, int AssignToId, int UserId);
        void WorkOrderReject(int WorkOrderId, string Remark, int UserId);
        List<ParameterPCBEntity> GetParameterPCBList(long EnquirySampleId);
        void UpdateParameterPCBLimit(List<ParameterPCBEntity> PCBLimits, int UserId);
        string UpdateRemark(long EnquiryId, string Remark);
        UserRoleEntity GetUserRoles(string strUserName);
    }
}
