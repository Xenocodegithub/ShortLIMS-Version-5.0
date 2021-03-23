using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Enquiry;
namespace LIMS_DEMO.Core.Repository
{
    public interface IContract : IDisposable
    {
        string AddContract(ContractEntity contractEntity);
        string UpdateContract(ContractEntity contractEntity);
        string DeleteContract(long EnquiryDetailId);
        IList<ContractEntity> GetContractDetails(int EnquiryId);
    }
}
