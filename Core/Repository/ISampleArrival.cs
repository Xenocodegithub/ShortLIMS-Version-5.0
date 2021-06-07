using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Arrival;


namespace LIMS_DEMO.Core.Repository
{
    public interface ISampleArrival : IDisposable
    {
        string AddTRFPlannerByDiscipline(List<PlannerByDisciplineEntity> planners,int SAmpleCollectionId);
        string UpdateTRFSampleArrival(SampleArrivalEntity samplearrivalEntity);
        string GetTRFReportULRNumber(int SampleTypeProductId, int EnquirySampleID, int WorkOrderID);
        SampleArrivalEntity GetTRFArrivalDetails(int SampleCollectionId);
        List<SampleArrivalEntity> GetTRFReceiverList(int UserMasterID);
        List<SampleArrivalEntity> GetERFArrivalList(int WorkOrderID);
        List<SampleArrivalEntity> GetCollectionDevicesList1();
        DisposalEntity GetDates(long SampleCollectionId);
        List<DisposalEntity> GetCode(int SampleTypeProductId);
        List<DisposalEntity> GetDisposalData();
        long SaveDisposalData(DisposalEntity disposalEntity);
        string AddApprover(List<PlannerByDisciplineEntity> approvers,long? EnquirySampleId,long SampleCollectionId);
        List<SampleArrivalEntity> GetSampleReturnedList();
        SampleArrivalEntity GetInFieldIsNabl(int EnquirySampleID, int ParameterMasterId, int TestMethodId);
        long AddNotification(string Msg, string RoleName, SampleArrivalEntity samplearrivalEntity);
        List<SampleArrivalEntity> GetDisciplineList();
        string GetParameterByDiscipline(int EnquirySampleID, int DisciplineId);
        string GetReportULRNumber(int EnquiryId, int SampleTypeProductId,int EnquirySampleID, int WorkOrderID);
        List<SampleArrivalEntity> GetArrivalList();
        List<FinalReportEntity> GetFinalReportsList();
        List<SampleArrivalEntity> GetArrivalParameterUnitList(int EnquirySampleID);
        List<SampleArrivalEntity> GetTestMethod(int EnquirySampleID, int ParameterMasterId);
        List<SampleArrivalEntity> GetArrivalQtyPreservativeList(int EnquirySampleID);
        List<SampleArrivalEntity> GetCollectionDevicesList(int SampleCollectionId);
        string AddSampleArrival(SampleArrivalEntity samplearrivalEntity);
        string UpdateSampleArrival(SampleArrivalEntity samplearrivalEntity);
        SampleArrivalEntity GetSampleArrivalDetails(int SampleCollectionId);
        SampleArrivalEntity GetARCDetails(int ARCId);
    
        string GenerateULRNo(int SampleCollectionId,int EnquiryDetailId);
        string GenerateReportNo(int SampleCollectionId, string SampleName, string Collectedby, string CustName, string CustLoc);
        string GenerateSampleNo(int SampleCollectionId, string SampleNo);

        //////////////For Received Person////////////////////////////////////////////////////////////////////////////////
        List<SampleArrivalEntity> GetReceiverList(int UserMasterID);
        string UpdateSampleReceived(SampleArrivalEntity samplearrivalEntity);
        string AddPlannerByDiscipline(List<PlannerByDisciplineEntity> planners);

    }
}
