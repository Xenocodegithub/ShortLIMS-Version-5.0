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
    public class ContractBAL
    {
        public ContractBAL()
        {
            CoreFactory.contract = new ContractDAL();
        }
        public string AddContract(ContractEntity contractEntity)
        {
            return CoreFactory.contract.AddContract(contractEntity);
        }

        public string UpdateContract(ContractEntity contractEntity)
        {
            return CoreFactory.contract.UpdateContract(contractEntity);
        }

        public string DeleteContract(long EnquiryDetailId)
        {
            return CoreFactory.contract.DeleteContract(EnquiryDetailId);
        }

        public IList<ContractEntity> GetContractDetails(int EnquiryId)
        {
            return CoreFactory.contract.GetContractDetails(EnquiryId);
        }

    }
}
