using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.DAL.Enquiry;

namespace LIMS_DEMO.BAL.Enquiry
{
    public class EnquiryBAL
    {
        public EnquiryBAL()
        {
            CoreFactory.enquiry = new EnquiryDAL();
        }
        public long Add(EnquiryEntity enquiryEntity)
        {
            return CoreFactory.enquiry.Add(enquiryEntity);
        }
        public string Update(EnquiryEntity enquiryEntity)
        {
            return CoreFactory.enquiry.Update(enquiryEntity);
        }
        public EnquiryEntity GetDetails(int EnquiryId)
        {
            return CoreFactory.enquiry.GetDetails(EnquiryId);
        }
        public List<EnquiryEntity> GetEnquiries()
        {
            return CoreFactory.enquiry.GetEnquiries();
        }
        public List<EnquiryEntity> GetParameterDetails(int EnquiryId)
        {
            return CoreFactory.enquiry.GetParameterDetails(EnquiryId);
        }
        public string UpdateEnquiryStatus(long EnquiryId, int StatusId)
        {
            return CoreFactory.enquiry.UpdateEnquiryStatus(EnquiryId, StatusId);
        }

        public string GenerateQuotationNo(long EnquiryId)
        {
            return CoreFactory.enquiry.GenerateQuotationNo(EnquiryId);
        }

        public string DeleteEnquiry(long EnquiryId, bool IsActive)
        {
            return CoreFactory.enquiry.DeleteEnquiry(EnquiryId, IsActive);
        }

        public long AddLog(EnquiryLogEntity enquiryLogEntity)
        {
            return CoreFactory.enquiry.AddLog(enquiryLogEntity);
        }
        public string UpdateLog(EnquiryLogEntity enquiryLogEntity)
        {
            return CoreFactory.enquiry.UpdateLog(enquiryLogEntity);
        }
        public EnquiryLogEntity GetEnquiryLogDetails(int EnquiryId)
        {
            return CoreFactory.enquiry.GetEnquiryLogDetails(EnquiryId);
        }
        public List<EnquiryLogEntity> GetEnquirLog(int EnquiryId)
        {
            return CoreFactory.enquiry.GetEnquirLog(EnquiryId);
        }
    }
}