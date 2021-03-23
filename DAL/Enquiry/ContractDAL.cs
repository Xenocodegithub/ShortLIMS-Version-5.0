using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.Core.Repository;

namespace LIMS_DEMO.DAL.Enquiry
{
    public class ContractDAL : IContract
    {

        readonly LIMSEntities _dbContext;

        public ContractDAL()
        {
            _dbContext = new LIMSEntities();
        }
     
         public string AddContract(ContractEntity contractEntity)
        {
            try
            {
                _dbContext.EnquiryDetails.Add(new EnquiryDetail()
                {
                    EnquiryId = contractEntity.EnquiryId,
                    SampleTypeProductId = contractEntity.SampleTypeProductId,
                    //ProductGroupId = contractEntity.ProductGroupId,
                    //SubGroupId = contractEntity.SubGroupId,
                    IsActive = true,
                    EnteredBy = contractEntity.EnteredBy,
                    EnteredDate = System.DateTime.UtcNow
                });
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }

        }

        public string UpdateContract(ContractEntity contractEntity)
        {
            try
            {
                var contract = _dbContext.EnquiryDetails.Find(contractEntity.EnquiryDetailId);
                contract.SampleTypeProductId = contractEntity.SampleTypeProductId;
                // contract.ProductGroupId = contractEntity.ProductGroupId;
                //contract.SubGroupId = contractEntity.SubGroupId;
                contract.ModifiedBy = contractEntity.EnteredBy;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public string DeleteContract(long EnquiryDetailId)
        {
            try
            {
                var contract = _dbContext.EnquiryDetails.Find(EnquiryDetailId);
                contract.IsActive = false;
                //_dbContext.EnquiryDetails.Remove(contract);
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public IList<ContractEntity> GetContractDetails(int EnquiryId)
        {
            return (from e in _dbContext.EnquiryDetails
                    join stp in _dbContext.SampleTypeProductMasters on e.SampleTypeProductId equals stp.SampleTypeProductId
                    //join p in _dbContext.ProductGroupMasters on e.ProductGroupId equals p.ProductGroupId
                    //join s in _dbContext.SubGroupMasters on e.SubGroupId equals s.SubGroupId
                    //into contracts
                    //from c in contracts
                    where e.EnquiryId == EnquiryId && e.IsActive == true
                    select new ContractEntity()
                    {
                        EnquiryId = (long)e.EnquiryId.Value,
                        EnquiryDetailId = e.EnquiryDetailId,
                        //ProductGroupId = (Int32)e.ProductGroupId,
                        SampleTypeProductId = e.SampleTypeProductId,
                        SampleTypeProductName =stp.SampleTypeProductName,
                        SampleTypeProductCode = stp.SampleTypeProductCode,
                        //ProductGroupName = p.ProductGroupName,
                        //SubGroupId = (Int32)e.SubGroupId,
                        //SubGroupName = s.SubGroupName,
                        //SubGroupCode = s.SubGroupCode,
                    }).ToList();

        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            { if (disposing) { _dbContext.Dispose(); } }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
