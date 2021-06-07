using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Arrival;
using LIMS_DEMO.Core.Collection;
using LIMS_DEMO.DAL.Collection;
namespace LIMS_DEMO.BAL.Collection
{
    public class SampleCollectionBAL
    {
        public SampleCollectionBAL()
        {
            CoreFactory.samplecollection = new SampleCollectionDAL();
        }
    
        public long AddNotification(string Msg, string RoleName, SampleCollectionEntity samplecollectionEntity)
        {
            return CoreFactory.samplecollection.AddNotification(Msg,RoleName, samplecollectionEntity);

        }
        public string GetExpectedCollDate(int WorkOrderSampleCollectionDateId)
        {
            return CoreFactory.samplecollection.GetExpectedCollDate(WorkOrderSampleCollectionDateId);

        }
        public long GetFDDetails(int SampleCollectionId)
        {
            return CoreFactory.samplecollection.GetFDDetails(SampleCollectionId);
        }
       
        public string UpdateWorkOrderSampleCollectionDate(int WorkOrderSampleCollectionDateId, DateTime CollectionDate)
        {
            return CoreFactory.samplecollection.UpdateWorkOrderSampleCollectionDate(WorkOrderSampleCollectionDateId, CollectionDate);

        }
        public string AddARC(int SampleCollectionId, int EnteredBy)
        {
            return CoreFactory.samplecollection.AddARC(SampleCollectionId, EnteredBy);
        }
        public string AddSampleCollection(SampleCollectionEntity samplecollectionEntity)
        {
            return CoreFactory.samplecollection.AddSampleCollection(samplecollectionEntity);
        }
        public string TRFEnv_Update(SampleArrivalEntity samplecollectionEntity)
        {
            return CoreFactory.samplecollection.TRFEnv_Update(samplecollectionEntity);
        }
        public SampleCollectionEntity GetSampleCollectionDetails(int SampleCollectionId)
        {
            return CoreFactory.samplecollection.GetSampleCollectionDetails(SampleCollectionId);
        }
        public List<ProcedureEntity> GetCollectionProcedureList(int SampleCollectionId)
        {
            return CoreFactory.samplecollection.GetCollectionProcedureList(SampleCollectionId);
        }
        //public SampleCollectionEntity GetWorkOrderDetails(int QuotationId)
        //{
        //    return CoreFactory.samplecollection.GetWorkOrderDetails(QuotationId);
        //}
        public string Update(SampleCollectionEntity samplecollectionEntity)
        {
            return CoreFactory.samplecollection.Update(samplecollectionEntity);
        }
        public List<SampleCollectionEntity> GetCollectionList(int UserMasterID, int CollectedBy)
        {
            return CoreFactory.samplecollection.GetCollectionList(UserMasterID,  CollectedBy);
        }
        public List<QuantityPreservativeEntity> GetSampleQty(int SampleTypeProductId)
        {
            return CoreFactory.samplecollection.GetSampleQty(SampleTypeProductId);
        }
        public string AddQuantityPreservative(List<QuantityPreservativeEntity> quantity, long EnquirySampleID, int WorkOrderID)
        {
            return CoreFactory.samplecollection.AddQuantityPreservative(quantity,EnquirySampleID,WorkOrderID);
        }
        public string AddSampleDevice(List<SampleDeviceEntity> device)
        {
            return CoreFactory.samplecollection.AddSampleDevice(device);
        }
        public string AddSampleProcedure(List<ProcedureEntity> procedure)
        {
            return CoreFactory.samplecollection.AddSampleProcedure(procedure);
        }
        public string UpdateCollectionStatus(long SampleCollectionId, int StatusId)
        {
            return CoreFactory.samplecollection.UpdateCollectionStatus(SampleCollectionId, StatusId);
           
        }
        public string UpdateExpectSampleCollDate(int WorkOrderID, DateTime CollectionDate)
        {
            return CoreFactory.samplecollection.UpdateExpectSampleCollDate(WorkOrderID, CollectionDate);

        }
        public int GetFieldIdByMatrixName(string MatrixName, int SampleCollectionId)
        {
            return CoreFactory.samplecollection.GetFieldIdByMatrixName(MatrixName, SampleCollectionId);
        }
    }
}
