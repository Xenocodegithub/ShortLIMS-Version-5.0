using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.DAL.Configuration
{
    public class ProductSubMatrixDAL: IProductSubMatrix
    {
        readonly LIMSEntities _dbContext;
        public ProductSubMatrixDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public ProductSubMatrixEntity GetDetailsSTP(int SampleTypeProductId)
        {
            return _dbContext.SampleTypeProductMasters.Where(u => u.SampleTypeProductId == SampleTypeProductId).Select(u => new ProductSubMatrixEntity()
            {
                SampleTypeProductId = u.SampleTypeProductId,
                SampleTypeProductName = u.SampleTypeProductName,
                SampleTypeProductCode = u.SampleTypeProductCode,
                IsActive = u.IsActive
            }).FirstOrDefault();
        }
        public string AddSampleTypeProduct(ProductSubMatrixEntity productSubMatrixEntity)
        {
            try
            {
                _dbContext.SampleTypeProductMasters.Add(new SampleTypeProductMaster()
                {
                    SampleTypeProductId = (Int32)productSubMatrixEntity.SampleTypeProductId,
                    SampleTypeProductName = productSubMatrixEntity.SampleTypeProductName,
                    SampleTypeProductCode = productSubMatrixEntity.SampleTypeProductCode,
                    IsActive = productSubMatrixEntity.IsActive,
                    EnteredBy = productSubMatrixEntity.EnteredBy,
                    EnteredDate = productSubMatrixEntity.EnteredDate,
                    ModifiedBy = productSubMatrixEntity.ModifiedBy

                });
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }

        }
        public string UpdateSTP(ProductSubMatrixEntity productSubMatrixEntity)
        {
            try
            {
                var STPMaster = _dbContext.SampleTypeProductMasters.Find(productSubMatrixEntity.SampleTypeProductId);
                STPMaster.SampleTypeProductId = (Int32)productSubMatrixEntity.SampleTypeProductId;
                STPMaster.SampleTypeProductName = productSubMatrixEntity.SampleTypeProductName;
                STPMaster.SampleTypeProductCode = productSubMatrixEntity.SampleTypeProductCode;
                STPMaster.IsActive = productSubMatrixEntity.IsActive;
                STPMaster.EnteredBy = productSubMatrixEntity.EnteredBy;
                STPMaster.EnteredDate = productSubMatrixEntity.EnteredDate;

                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public List<ProductSubMatrixEntity> GetSampleTypeProductList()
        {
            try
            {

                return (from stp in _dbContext.SampleTypeProductMasters
                        where stp.IsActive == true
                        select new ProductSubMatrixEntity()
                        {
                            SampleTypeProductId = stp.SampleTypeProductId,
                            SampleTypeProductName = stp.SampleTypeProductName,
                            SampleTypeProductCode = stp.SampleTypeProductCode,

                            IsActive = stp.IsActive,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public ProductSubMatrixEntity GetDetailsPG(int SampleTypeProductId, int ProductGroupId)
        {
            return _dbContext.ProductGroupMasters.Where(u => u.ProductGroupId == ProductGroupId).Select(u => new ProductSubMatrixEntity()
            {
                ProductGroupId = u.ProductGroupId,
                ProductGroupName = u.ProductGroupName,
                SampleTypeProductId = u.SampleTypeProductId,
                Code = u.ProductGroupCode,
                IsActive = u.IsActive
            }).FirstOrDefault();
        }
        public string AddProductGroup(ProductSubMatrixEntity productSubMatrixEntity)
        {
            try
            {
                _dbContext.ProductGroupMasters.Add(new ProductGroupMaster()
                {
                    ProductGroupId = productSubMatrixEntity.ProductGroupId,
                    ProductGroupName = productSubMatrixEntity.ProductGroupName,
                    ProductGroupCode = productSubMatrixEntity.Code,
                    SampleTypeProductId = productSubMatrixEntity.SampleTypeProductId,
                    //Description = configurationEntity.Description,
                    //IsSampleToBeDisposed = configurationEntity.IsSampleToBeDisposed,
                    //SampleRetentionPeriod = configurationEntity.SampleRetentionPeriod,
                    IsActive = productSubMatrixEntity.IsActive,
                    EnteredBy = productSubMatrixEntity.EnteredBy,
                    EnteredDate = productSubMatrixEntity.EnteredDate,
                    ModifiedBy = productSubMatrixEntity.ModifiedBy

                });
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }

        }
        public string UpdatePG(ProductSubMatrixEntity productSubMatrixEntity)
        {
            try
            {
                var PGMaster = _dbContext.ProductGroupMasters.Find(productSubMatrixEntity.ProductGroupId);
                PGMaster.ProductGroupId = productSubMatrixEntity.ProductGroupId;
                PGMaster.SampleTypeProductId = productSubMatrixEntity.SampleTypeProductId;
                PGMaster.ProductGroupName = productSubMatrixEntity.ProductGroupName;
                PGMaster.ProductGroupCode = productSubMatrixEntity.Code;
                PGMaster.IsActive = productSubMatrixEntity.IsActive;
                PGMaster.EnteredBy = productSubMatrixEntity.EnteredBy;
                PGMaster.EnteredDate = productSubMatrixEntity.EnteredDate;

                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public List<ProductSubMatrixEntity> GetProductGroupList()
        {
            try
            {
                var a= (from e in _dbContext.ProductGroupMasters
                        join stp in _dbContext.SampleTypeProductMasters on e.SampleTypeProductId equals stp.SampleTypeProductId
                        where e.IsActive == true
                        select new ProductSubMatrixEntity()
                        {
                            ProductGroupName = e.ProductGroupName,
                            ProductGroupId = e.ProductGroupId,
                            Code = e.ProductGroupCode,
                            SampleTypeProductName = stp.SampleTypeProductName,
                            SampleTypeProductId = e.SampleTypeProductId,
                            IsActive = e.IsActive,
                        }).ToList();
                return a;
            }
            catch (Exception ex)
            {
                return null;
            }
            //throw new NotImplementedException();
        }
        public string DeletePG(int SampleTypeProductId, int ProductGroupId)
        {
            try
            {
                var PGMaster = _dbContext.ProductGroupMasters.Find(ProductGroupId);
                PGMaster.IsActive = false;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public ProductSubMatrixEntity GetDetailsSG(int SampleTypeProductId, int ProductGroupId, int SubgroupId)
        {
            return _dbContext.SubGroupMasters.Where(u => u.SubGroupId == SubgroupId).Select(u => new ProductSubMatrixEntity()
            {
                ProductGroupId = ProductGroupId,
                SampleTypeProductId = SampleTypeProductId,
                SubGroupId = u.SubGroupId,
                SubGroupName = u.SubGroupName,
                SubGroupCode = u.SubGroupCode,
                IsActive = u.IsActive
            }).FirstOrDefault();
        }
        public string AddSubGroup(ProductSubMatrixEntity productSubMatrixEntity)
        {
            try
            {
                _dbContext.SubGroupMasters.Add(new SubGroupMaster()
                {
                    SubGroupId = productSubMatrixEntity.SubGroupId,
                    ProductGroupId = productSubMatrixEntity.ProductGroupId,
                    SampleTypeProductId = productSubMatrixEntity.SampleTypeProductId,
                    SubGroupName = productSubMatrixEntity.SubGroupName,
                    SubGroupCode = productSubMatrixEntity.SubGroupCode,
                    IsActive = productSubMatrixEntity.IsActive,
                    EnteredBy = productSubMatrixEntity.EnteredBy,
                    EnteredDate = productSubMatrixEntity.EnteredDate,
                    ModifiedBy = productSubMatrixEntity.ModifiedBy

                });
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }

        }
        public string UpdateSG(ProductSubMatrixEntity productSubMatrixEntity)
        {
            try
            {
                var SGMaster = _dbContext.SubGroupMasters.Find(productSubMatrixEntity.SubGroupId);
                SGMaster.ProductGroupId = productSubMatrixEntity.ProductGroupId;
                SGMaster.SampleTypeProductId = productSubMatrixEntity.SampleTypeProductId;
                SGMaster.SubGroupName = productSubMatrixEntity.SubGroupName;
                SGMaster.SubGroupCode = productSubMatrixEntity.SubGroupCode;
                SGMaster.IsActive = productSubMatrixEntity.IsActive;
                SGMaster.EnteredBy = productSubMatrixEntity.EnteredBy;
                SGMaster.EnteredDate = productSubMatrixEntity.EnteredDate;


                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public List<ProductSubMatrixEntity> GetSubGroupList()
        {
            try
            {

                return (from e in _dbContext.SubGroupMasters
                        join stp in _dbContext.SampleTypeProductMasters on e.SampleTypeProductId equals stp.SampleTypeProductId
                        join pgm in _dbContext.ProductGroupMasters on e.ProductGroupId equals pgm.ProductGroupId
                        where e.IsActive == true
                        select new ProductSubMatrixEntity()
                        {
                            ProductGroupId = e.ProductGroupId,
                            ProductGroupName = pgm.ProductGroupName,
                            SampleTypeProductId = e.SampleTypeProductId,
                            SampleTypeProductName = stp.SampleTypeProductName,
                            SubGroupId = e.SubGroupId,
                            SubGroupName = e.SubGroupName,
                            SubGroupCode = e.SubGroupCode,
                            IsActive = e.IsActive,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public string DeleteSG(int SampleTypeProductId, int ProductGroupId, int SubGroupId)
        {
            try
            {
                var SGMaster = _dbContext.SubGroupMasters.Find(SubGroupId);
                SGMaster.IsActive = false;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public ProductSubMatrixEntity GetDetailsMatrix(int SampleTypeProductId, int ProductGroupId, int SubgroupId, int MatrixId)
        {
            return _dbContext.MatrixMasters.Where(u => u.MatrixId == MatrixId).Select(u => new ProductSubMatrixEntity()
            {
                ProductGroupId = ProductGroupId,
                SubGroupId = u.SubGroupId,
                SampleTypeProductId = u.SampleTypeProductId,
                MatrixId = u.MatrixId,
                MatrixName = u.MatrixName,
                MatrixCode = u.MatrixCode,
                IsActive = u.IsActive
            }).FirstOrDefault();
        }
        public string AddMatrix(ProductSubMatrixEntity productSubMatrixEntity)
        {
            try
            {
                _dbContext.MatrixMasters.Add(new MatrixMaster()
                {
                    MatrixId = productSubMatrixEntity.MatrixId,
                    ProductGroupId = productSubMatrixEntity.ProductGroupId,
                    SampleTypeProductId = productSubMatrixEntity.SampleTypeProductId,
                    SubGroupId = productSubMatrixEntity.SubGroupId,
                    MatrixName = productSubMatrixEntity.MatrixName,
                    MatrixCode = productSubMatrixEntity.MatrixCode,
                    IsActive = productSubMatrixEntity.IsActive,
                    EnteredBy = productSubMatrixEntity.EnteredBy,
                    EnteredDate = productSubMatrixEntity.EnteredDate,
                    ModifiedBy = productSubMatrixEntity.ModifiedBy

                });
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }

        }
        public string UpdateMatrix(ProductSubMatrixEntity productSubMatrixEntity)
        {
            try
            {
                var MatrixMaster = _dbContext.MatrixMasters.Find(productSubMatrixEntity.MatrixId);
                MatrixMaster.ProductGroupId = productSubMatrixEntity.ProductGroupId;
                MatrixMaster.SubGroupId = productSubMatrixEntity.SubGroupId;
                MatrixMaster.MatrixName = productSubMatrixEntity.MatrixName;
                MatrixMaster.SampleTypeProductId = productSubMatrixEntity.SampleTypeProductId;
                MatrixMaster.MatrixCode = productSubMatrixEntity.MatrixCode;
                MatrixMaster.IsActive = productSubMatrixEntity.IsActive;
                MatrixMaster.EnteredBy = productSubMatrixEntity.EnteredBy;
                MatrixMaster.EnteredDate = productSubMatrixEntity.EnteredDate;


                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string DeleteMatrix(int SampleTypeProductId, int ProductGroupId, int SubGroupId, int MatrixId)
        {
            try
            {
                var MatrixMaster = _dbContext.MatrixMasters.Find(MatrixId);
                MatrixMaster.IsActive = false;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public List<ProductSubMatrixEntity> GetMatrixList()
        {
            try
            {

                return (from e in _dbContext.MatrixMasters
                        join stp in _dbContext.SampleTypeProductMasters on e.SampleTypeProductId equals stp.SampleTypeProductId
                        join pgm in _dbContext.ProductGroupMasters on e.ProductGroupId equals pgm.ProductGroupId
                        join sgm in _dbContext.SubGroupMasters on e.SubGroupId equals sgm.SubGroupId
                        where e.IsActive == true
                        select new ProductSubMatrixEntity()
                        {
                            ProductGroupName = pgm.ProductGroupName,
                            ProductGroupId = pgm.ProductGroupId,
                            SampleTypeProductId = e.SampleTypeProductId,
                            SampleTypeProductName = stp.SampleTypeProductName,
                            SubGroupId = sgm.SubGroupId,
                            SubGroupName = sgm.SubGroupName,
                            MatrixName = e.MatrixName,
                            MatrixId = e.MatrixId,
                            MatrixCode = e.MatrixCode,
                            IsActive = e.IsActive,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public string DeleteSTP(int SampleTypeProductId)
        {
            try
            {
                var STPMaster = _dbContext.SampleTypeProductMasters.Find(SampleTypeProductId);
                STPMaster.IsActive = false;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
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