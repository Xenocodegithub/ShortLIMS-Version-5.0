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
    public class CostingBAL
    {
        public CostingBAL()
        {
            CoreFactory.costing = new CostingDAL();
        }
        public List<CostingEntity> GetPGSGM(int EnquiryID)
        {
            return CoreFactory.costing.GetPGSGM(EnquiryID);
        }
        public long AddQuotation(QuotationEntity quotation)
        {
            return CoreFactory.costing.AddQuotation(quotation);
        }
        public bool AddQuotationTermsAndCondition(List<QuotationTNCEntity> quotationTerms, long QuotationId, string Remarks)
        {
            return CoreFactory.costing.AddQuotationTermsAndCondition(quotationTerms, QuotationId,Remarks);
        }
        public bool AddCosting(CostingEntity costingEntity)
        {
            return CoreFactory.costing.AddCosting(costingEntity);
        }
        public bool UpdateCosting(CostingEntity costingEntity)
        {
            return CoreFactory.costing.UpdateCosting(costingEntity);
        }
        public bool DeleteCosting(int CostingId)
        {
            return CoreFactory.costing.DeleteCosting(CostingId);
        }
        public CostingEntity GetCosting(int EnquirySampleID, int CostingId)
        {
            return CoreFactory.costing.GetCosting(EnquirySampleID, CostingId);
        }
        public List<CostingEntity> GetCostingList(int EnquiryId)
        {
            return CoreFactory.costing.GetCostingList(EnquiryId);
        }
        public List<CostingEntity> GetSampleTypeCostingList(int EnquiryId)
        {
            return CoreFactory.costing.GetSampleTypeCostingList(EnquiryId);
        }
        public List<TermsAndConditionEntity> GetTermsAndCondition(int EnquiryId)
        {
            return CoreFactory.costing.GetTermsAndCondition(EnquiryId);
        }

        public decimal GetQuotedAmount(long EnquiryId)
        {
            return CoreFactory.costing.GetQuotedAmount(EnquiryId);
        }
    }
}
