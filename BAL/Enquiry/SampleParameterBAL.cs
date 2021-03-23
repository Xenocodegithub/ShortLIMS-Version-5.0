using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.DAL.Enquiry;

namespace LIMS_DEMO.BAL.Enquiry
{
    public class SampleParameterBAL
    {

        public SampleParameterBAL()
        {
            CoreFactory.sampleParameter = new SampleParameterDAL();
        }
        public string GenerateDisplaySampleName()
        {
            return CoreFactory.sampleParameter.GenerateDisplaySampleName();

        }
        public List<SampleLocationEntity> GetSampleLocationList(int EnquirySampleID)
        {
            return CoreFactory.sampleParameter.GetSampleLocationList(EnquirySampleID);

        }
        public void UpdateSampleTypeProductId(long EnquiryMasterSampleTypeId,string DisplaySampleName)
        {
            CoreFactory.sampleParameter.UpdateSampleTypeProductId(EnquiryMasterSampleTypeId, DisplaySampleName);

        }
        public long GetEnquirySampleID()
        {
            return CoreFactory.sampleParameter.GetEnquirySampleID();

        }
       public EnquiryParameterEntity GetFormula(EnquiryParameterEntity objParam)
        {
            return CoreFactory.sampleParameter.GetFormula(objParam);

        }
        public List<EnquiryParameterEntity> GetSampleParameterList(int EnquiryDetailId, int EnquirySampleId, int SampleTypeProductId)
        {
            return CoreFactory.sampleParameter.GetSampleParameterList(EnquiryDetailId, EnquirySampleId, SampleTypeProductId);
        }
        public List<EnquiryParameterEntity> GetSampleParameterList(long EnquirySampleId)
        {
            return CoreFactory.sampleParameter.GetSampleParameterList(EnquirySampleId);
        }
        public EnquirySampleEntity GetSampleNumber(int EnquiryId,int EnquiryDetailId,int SampleTypeProductId)
        {
            return CoreFactory.sampleParameter.GetSampleNumber(EnquiryId,EnquiryDetailId, SampleTypeProductId);
        }
        public EnquirySampleEntity GetWODSampleNumber(int WorkOrderId, int EnquiryDetailId, int SampleTypeProductId)
        {
            return CoreFactory.sampleParameter.GetWODSampleNumber(WorkOrderId, EnquiryDetailId, SampleTypeProductId);
        }
        public EnquirySampleEntity AddEnquirySampleDetail(EnquirySampleEntity enquirySampleEntity)
        {
            return CoreFactory.sampleParameter.AddEnquirySampleDetail(enquirySampleEntity);
        }
        public void UpdateEnquirySampleCharges(long EnquirySampleId, decimal TotalCharges)
        {
           CoreFactory.sampleParameter.UpdateEnquirySampleCharges(EnquirySampleId, TotalCharges);
        }

        public void UpdateEnquiryParameterPCB(List<EnquiryParameterEntity> enquiryParameterList)
        {
            CoreFactory.sampleParameter.UpdateEnquiryParameterPCB(enquiryParameterList);
        }

        public void DeleteSample(long EnquirySampleId, bool isActive, string CurrentStatus)
        {
            CoreFactory.sampleParameter.DeleteSample(EnquirySampleId, isActive,CurrentStatus);
        }

        public EnquirySampleEntity GetEnquirySampleDetail(int EnquirySampleID)
        {
            return CoreFactory.sampleParameter.GetEnquirySampleDetail(EnquirySampleID);
        }

        public List<EnquirySampleEntity> GetEnquirySampleList(int EnquiryID)
        {
            return CoreFactory.sampleParameter.GetEnquirySampleList(EnquiryID);
        }

        public bool AddEnquiryParameterDetail(List<EnquiryParameterEntity> parameters)
        {
            return CoreFactory.sampleParameter.AddEnquiryParameterDetail(parameters);
        }

        public string GetSampleParameters(int EnquirySampleID)
        {
            return CoreFactory.sampleParameter.GetSampleParameters(EnquirySampleID);
        }
        public string GetPGSGM(int EnquiryDetailId)
        {
            return CoreFactory.sampleParameter.GetPGSGM(EnquiryDetailId);
        }
        public string GetParameterRemarks(int EnquirySampleID)
        {
            return CoreFactory.sampleParameter.GetParameterRemarks(EnquirySampleID);

        }
        public string GetSamplePCBLimit(int EnquirySampleID)
        {
            return CoreFactory.sampleParameter.GetSamplePCBLimit(EnquirySampleID);

        }
        public string GetFDParameters(int ParameterMappingId)
        {
            return CoreFactory.sampleParameter.GetFDParameters(ParameterMappingId);
        }

    }
}
