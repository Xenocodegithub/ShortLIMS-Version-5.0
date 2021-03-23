using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Enquiry;

namespace LIMS_DEMO.Core.Repository
{
    public interface ICosting
    {
        List<CostingEntity> GetPGSGM(int EnquiryID);
        List<CostingEntity> GetSampleTypeCostingList(int EnquiryId);
        long AddQuotation(QuotationEntity quotation);
        bool AddQuotationTermsAndCondition(List<QuotationTNCEntity> quotationTerms, long QuotationId, string Remarks);
        bool AddCosting(CostingEntity costingEntity);
        bool UpdateCosting(CostingEntity costingEntity);
        bool DeleteCosting(int CostingId);
        CostingEntity GetCosting(int EnquiryMasterSampleTypeId, int CostingId);
        List<CostingEntity> GetCostingList(int EnquiryId);
        List<TermsAndConditionEntity> GetTermsAndCondition(int EnquiryId);
        decimal GetQuotedAmount(long EnquiryId);
    }
}
