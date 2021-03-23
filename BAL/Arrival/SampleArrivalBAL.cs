using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Arrival;
using LIMS_DEMO.DAL.Arrival;

namespace LIMS_DEMO.BAL.Arrival
{
    public class SampleArrivalBAL
    {
        public SampleArrivalBAL()
        {
            CoreFactory.samplearrival = new SampleArrivalDAL();
        }
        public DisposalEntity GetDates(long SampleCollectionId)
        {
            return CoreFactory.samplearrival.GetDates(SampleCollectionId);
        }
        public List<DisposalEntity> GetCode(int SampleTypeProductId)
        {
            return CoreFactory.samplearrival.GetCode(SampleTypeProductId);
        }
        public List<DisposalEntity> GetDisposalData()
        {
            return CoreFactory.samplearrival.GetDisposalData();
        }
        public long SaveDisposalData(DisposalEntity disposalEntity)
        {
            return CoreFactory.samplearrival.SaveDisposalData(disposalEntity);
        }
        public List<SampleArrivalEntity> GetSampleReturnedList()
        {
            return CoreFactory.samplearrival.GetSampleReturnedList();
        }
        public SampleArrivalEntity GetInFieldIsNabl(int EnquirySampleID, int ParameterMasterId, int TestMethodId)
        {
            return CoreFactory.samplearrival.GetInFieldIsNabl(EnquirySampleID, ParameterMasterId, TestMethodId);

        }
        public long AddNotification(string Msg, string RoleName, SampleArrivalEntity samplearrivalEntity)
        {
            return CoreFactory.samplearrival.AddNotification(Msg, RoleName, samplearrivalEntity);

        }
        public string GetParameterByDiscipline(int EnquirySampleID, int DisciplineId)
        {
            return CoreFactory.samplearrival.GetParameterByDiscipline(EnquirySampleID, DisciplineId);

        }
        public string GetReportULRNumber(int EnquiryId, int SampleTypeProductId,int EnquirySampleID,int WorkOrderID )
        {
            return CoreFactory.samplearrival.GetReportULRNumber(EnquiryId, SampleTypeProductId, EnquirySampleID,WorkOrderID);
        }
      
        public List<SampleArrivalEntity> GetArrivalList()
        {
            return CoreFactory.samplearrival.GetArrivalList();
        }
        public List<FinalReportEntity> GetFinalReportsList()
        {
            return CoreFactory.samplearrival.GetFinalReportsList();
        }
        
        public List<SampleArrivalEntity> GetDisciplineList()
        {
            return CoreFactory.samplearrival.GetDisciplineList();
        }
        public List<SampleArrivalEntity> GetArrivalParameterUnitList(int EnquirySampleID)
        {
            return CoreFactory.samplearrival.GetArrivalParameterUnitList(EnquirySampleID);
        }
        public List<SampleArrivalEntity> GetTestMethod(int EnquirySampleID, int ParameterMasterId)
        {
            return CoreFactory.samplearrival.GetTestMethod(EnquirySampleID, ParameterMasterId);
        }
        public List<SampleArrivalEntity> GetArrivalQtyPreservativeList(int SampleCollectionId)
        {
            return CoreFactory.samplearrival.GetArrivalQtyPreservativeList(SampleCollectionId);
        }
        public List<SampleArrivalEntity> GetCollectionDevicesList(int SampleCollectionId)
        {
            return CoreFactory.samplearrival.GetCollectionDevicesList(SampleCollectionId);

        }
        public string AddSampleArrival(SampleArrivalEntity samplearrivalEntity)
        {
            return CoreFactory.samplearrival.AddSampleArrival(samplearrivalEntity);
        }
        public string UpdateSampleArrival(SampleArrivalEntity samplearrivalEntity)
        {
            return CoreFactory.samplearrival.UpdateSampleArrival(samplearrivalEntity);
        }
        public SampleArrivalEntity GetSampleArrivalDetails(int SampleCollectionId)
        {
            return CoreFactory.samplearrival.GetSampleArrivalDetails(SampleCollectionId);

        }
        public SampleArrivalEntity GetARCDetails(int ARCId)
        {
            return CoreFactory.samplearrival.GetARCDetails(ARCId);
        }
       
        public string GenerateULRNo(int SampleCollectionId,int EnquiryDetailId)
        {
            return CoreFactory.samplearrival.GenerateULRNo(SampleCollectionId, EnquiryDetailId);

        }
        public string GenerateReportNo(int SampleCollectionId, string SampleName, string Collectedby, string CustName, string CustLoc)
        {
            return CoreFactory.samplearrival.GenerateReportNo(SampleCollectionId, SampleName, Collectedby, CustName, CustLoc);

        }
        public string GenerateSampleNo(int SampleCollectionId, string SampleNo)
        {
            return CoreFactory.samplearrival.GenerateSampleNo(SampleCollectionId, SampleNo);

        }

        //////////////For Received Person////////////////////////////////////////////////////////////////////////////////
        public string UpdateSampleReceived(SampleArrivalEntity samplearrivalEntity)
        {
            return CoreFactory.samplearrival.UpdateSampleReceived(samplearrivalEntity);
        }
        public string AddApprover(List<PlannerByDisciplineEntity> approvers,long EnquirySampleId,long SampleCollectionId)
        {
            return CoreFactory.samplearrival.AddApprover(approvers, EnquirySampleId, SampleCollectionId);

        }
        public string AddPlannerByDiscipline(List<PlannerByDisciplineEntity> planners)
        {
            return CoreFactory.samplearrival.AddPlannerByDiscipline(planners);

        }
        public List<SampleArrivalEntity> GetReceiverList(int UserMasterID)
        {
            return CoreFactory.samplearrival.GetReceiverList(UserMasterID);
        }
    }
}
