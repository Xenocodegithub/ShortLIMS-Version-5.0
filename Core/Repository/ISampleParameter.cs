using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Enquiry;

namespace LIMS_DEMO.Core.Repository
{
    public interface ISampleParameter : IDisposable
    {
        string GenerateDisplaySampleName();
        List<SampleLocationEntity> GetSampleLocationList(int EnquirySampleID);
        void UpdateSampleTypeProductId(long EnquiryMasterSampleTypeId,string DisplaySampleName);
        EnquiryParameterEntity GetFormula(EnquiryParameterEntity objParam);
        string GetPGSGM(int EnquiryDetailId);
        long GetEnquirySampleID();
        List<EnquiryParameterEntity> GetSampleParameterList(int EnquiryDetailId, int EnquirySampleId, int SampleTypeProductId);
        List<EnquiryParameterEntity> GetSampleParameterList(long EnquirySampleId);
        EnquirySampleEntity AddEnquirySampleDetail(EnquirySampleEntity enquirySampleEntity);
        void UpdateEnquirySampleCharges(long EnquirySampleId, decimal TotalCharges);
        EnquirySampleEntity GetEnquirySampleDetail(int EnquirySampleID);
        List<EnquirySampleEntity> GetEnquirySampleList(int EnquiryID);
        bool AddEnquiryParameterDetail(List<EnquiryParameterEntity> parameters);
        string GetSampleParameters(int EnquirySampleID);
        string GetFDParameters(int ParameterMappingId);
        string GetParameterRemarks(int EnquirySampleID);
        EnquirySampleEntity GetSampleNumber(int EnquiryId,int EnquiryDetailId, int SampleTypeProductId);
        EnquirySampleEntity GetWODSampleNumber(int WorkOrderId, int EnquiryDetailId, int SampleTypeProductId);

        string GetSamplePCBLimit(int EnquirySampleID);
        void DeleteSample(long EnquirySampleId, bool isActive, string CurrentStatus);
        void UpdateEnquiryParameterPCB(List<EnquiryParameterEntity> enquiryParameterList);
    }
}
