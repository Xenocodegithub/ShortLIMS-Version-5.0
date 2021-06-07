using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Enquiry;
namespace LIMS_DEMO.Core.Repository
{
    public interface IEnquiry:IDisposable
    {
        string UpdateTRFStatus(long WorkOrderId, int StatusId);
        long Add(EnquiryEntity enquiryEntity);
        string Update(EnquiryEntity enquiryEntity);
        EnquiryEntity GetDetails(int EnquiryId);
        List<EnquiryEntity> GetEnquiries();
        List<EnquiryEntity> GetParameterDetails(int EnquiryId);
        string UpdateEnquiryStatus(long EnquiryId, int StatusId);
        string GenerateQuotationNo(long EnquiryId);
        string DeleteEnquiry(long EnquiryId, bool isActive);

        long AddLog(EnquiryLogEntity enquiryLogEntity);
        string UpdateLog(EnquiryLogEntity enquiryLogEntity);
        EnquiryLogEntity GetEnquiryLogDetails(int EnquiryId);
        List<EnquiryLogEntity> GetEnquirLog(int EnquiryId);
    }
}
