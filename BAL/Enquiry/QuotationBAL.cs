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
    public class QuotationBAL
    {
        public QuotationBAL()
        {
            CoreFactory.quotation = new QuotationDAL();
        }

        public List<QuotationEntity> GetQuotationList(int LabMasterId, DateTime? FromDate, DateTime? ToDate)
        {
            return CoreFactory.quotation.GetQuotationList(LabMasterId, FromDate, ToDate);
        }

        public QuotationPreviewEntity GetQuotationPreview(long EnquiryId)
        {
            return CoreFactory.quotation.GetQuotationPreview(EnquiryId);
        }

        public HeadOfficeEntity GetHeadOfficeDetails(int LabMasterId)
        {
            return CoreFactory.quotation.GetHeadOfficeDetails(LabMasterId);
        }
        public string GetQuotationRevisedDates(long EnquiryId)
        {
            return CoreFactory.quotation.GetQuotationRevisedDates(EnquiryId);
        }

        public string GetParamters(long EnquirySampleID)
        {
            return CoreFactory.quotation.GetParamters(EnquirySampleID);
        }

        public List<QuotationLogEntity> GetQuotationLog(long EnquiryId)
        {
            return CoreFactory.quotation.GetQuotationLog(EnquiryId);
        }
        //public QuotationLogEntity GetQuotationLogDetails(long EnquiryId)
        //{
        //    return CoreFactory.quotation.GetQuotationLogDetails(EnquiryId);
        //}
        public QuotationLogEntity GetQuotationLogDetails(long EnquiryId)
        {
            return CoreFactory.quotation.GetQuotationLogDetails(EnquiryId);
        }
        public bool AddQuotationLog(QuotationLogEntity QuoteLog)
        {
            return CoreFactory.quotation.AddQuotationLog(QuoteLog);
        }

        public QuotationEntity GetQuotationDetails(long EnquiryId)
        {
            return CoreFactory.quotation.GetQuotationDetails(EnquiryId);
        }

    }
}
