using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Enquiry;

namespace LIMS_DEMO.Core.Repository
{
    public interface IQuotation
    {
        List<QuotationEntity> GetQuotationList(int LabMasterId, DateTime? FromDate, DateTime? ToDate);
        QuotationPreviewEntity GetQuotationPreview(long EnquiryId);
        HeadOfficeEntity GetHeadOfficeDetails(int LabMasterId);
        string GetQuotationRevisedDates(long EnquiryId);
        string GetParamters(long EnquirySampleID);
        List<QuotationLogEntity> GetQuotationLog(long EnquiryId);
        //QuotationLogEntity GetQuotationLogDetails(long EnquiryId);
        QuotationLogEntity GetQuotationLogDetails(long EnquiryId);
        bool AddQuotationLog(QuotationLogEntity QuoteLog);
        QuotationEntity GetQuotationDetails(long EnquiryId);
       
    }
}
