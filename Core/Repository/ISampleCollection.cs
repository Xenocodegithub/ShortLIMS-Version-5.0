using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Collection;

namespace LIMS_DEMO.Core.Repository
{
    public interface ISampleCollection : IDisposable
    {
      
        long AddNotification(string Msg, string RoleName, SampleCollectionEntity samplecollectionEntity);
        string GetExpectedCollDate(int WorkOrderSampleCollectionDateId);
        long GetFDDetails(int SampleCollectionId);
        string UpdateWorkOrderSampleCollectionDate(int WorkOrderSampleCollectionDateId, DateTime CollectionDate);
        string AddSampleCollection(SampleCollectionEntity samplecollectionEntity);
        SampleCollectionEntity GetSampleCollectionDetails(int SampleCollectionId);
        //SampleCollectionEntity GetWorkOrderDetails(int QuotationId);
        string Update(SampleCollectionEntity samplecollectionEntity);
        List<SampleCollectionEntity> GetCollectionList(int UserMasterID, int CollectedBy);
        List<QuantityPreservativeEntity> GetSampleQty(int SampleTypeProductId);
        List<ProcedureEntity> GetCollectionProcedureList(int SampleCollectionId);
        string AddQuantityPreservative(List<QuantityPreservativeEntity> quantity, long EnquirySampleID, int WorkOrderID);
        string AddSampleDevice(List<SampleDeviceEntity> device);
        string AddSampleProcedure(List<ProcedureEntity> procedure);
        string UpdateCollectionStatus(long SampleCollectionId, int StatusId);
        string UpdateExpectSampleCollDate(int WorkOrderID, DateTime CollectionDate);
        int GetFieldIdByMatrixName(string MatrixName, int SampleCollectionId);
    }
}
