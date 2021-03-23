using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.DAL.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.BAL.Configuration
{
    public class ProductSubMatrixBAL
    {
        public ProductSubMatrixBAL()
        {
            CoreFactory.productSubMatrix = new ProductSubMatrixDAL();
        }
        public ProductSubMatrixEntity GetDetailsSTP(int SampleTypeProductId)
        {
           return CoreFactory.productSubMatrix.GetDetailsSTP(SampleTypeProductId);
        }
        public string AddSampleTypeProduct(ProductSubMatrixEntity productSubMatrixEntity)
        {
            return CoreFactory.productSubMatrix.AddSampleTypeProduct(productSubMatrixEntity);
        }
        public string UpdateSTP(ProductSubMatrixEntity productSubMatrixEntity)
        {
            return CoreFactory.productSubMatrix.UpdateSTP(productSubMatrixEntity);
        }
        public List<ProductSubMatrixEntity> GetSampleTypeProductList()
        {
            return CoreFactory.productSubMatrix.GetSampleTypeProductList();
        }
        public ProductSubMatrixEntity GetDetailsPG(int SampleTypeProductId, int ProductGroupId)
        {
            return CoreFactory.productSubMatrix.GetDetailsPG(SampleTypeProductId,ProductGroupId);
        }
        public string AddProductGroup(ProductSubMatrixEntity productSubMatrixEntity)
        {
            return CoreFactory.productSubMatrix.AddProductGroup(productSubMatrixEntity);
        }
        public string UpdatePG(ProductSubMatrixEntity productSubMatrixEntity)
        {
            return CoreFactory.productSubMatrix.UpdatePG(productSubMatrixEntity);
        }
        public List<ProductSubMatrixEntity> GetProductGroupList()
        {
            return CoreFactory.productSubMatrix.GetProductGroupList();
        }
        public string DeletePG(int SampleTypeProductId, int ProductGroupId)
        {
            return CoreFactory.productSubMatrix.DeletePG(SampleTypeProductId,ProductGroupId);
        }
        public ProductSubMatrixEntity GetDetailsSG(int SampleTypeProductId, int ProductGroupId, int SubgroupId)
        {
            return CoreFactory.productSubMatrix.GetDetailsSG( SampleTypeProductId,  ProductGroupId,  SubgroupId);
        }
        public string AddSubGroup(ProductSubMatrixEntity productSubMatrixEntity)
        {
            return CoreFactory.productSubMatrix.AddSubGroup(productSubMatrixEntity);
        }
        public string UpdateSG(ProductSubMatrixEntity productSubMatrixEntity)
        {
            return CoreFactory.productSubMatrix.UpdateSG(productSubMatrixEntity);
        }
        public List<ProductSubMatrixEntity> GetSubGroupList()
        {
            return CoreFactory.productSubMatrix.GetSubGroupList();
        }
        public string DeleteSG(int SampleTypeProductId, int ProductGroupId, int SubGroupId)
        {
            return CoreFactory.productSubMatrix.DeleteSG( SampleTypeProductId,  ProductGroupId,  SubGroupId);
        }
        public ProductSubMatrixEntity GetDetailsMatrix(int SampleTypeProductId, int ProductGroupId, int SubgroupId, int MatrixId)
        {
            return CoreFactory.productSubMatrix.GetDetailsMatrix( SampleTypeProductId,  ProductGroupId,  SubgroupId,  MatrixId);
        }
        public string AddMatrix(ProductSubMatrixEntity productSubMatrixEntity)
        {
            return CoreFactory.productSubMatrix.AddMatrix(productSubMatrixEntity);
        }
        public string UpdateMatrix(ProductSubMatrixEntity productSubMatrixEntity)
        {
            return CoreFactory.productSubMatrix.UpdateMatrix(productSubMatrixEntity);
        }
        public string DeleteMatrix(int SampleTypeProductId, int ProductGroupId, int SubGroupId, int MatrixId)
        {
            return CoreFactory.productSubMatrix.DeleteMatrix( SampleTypeProductId, ProductGroupId, SubGroupId, MatrixId);
        }
        public List<ProductSubMatrixEntity> GetMatrixList()
        {
            return CoreFactory.productSubMatrix.GetMatrixList();
        }
        public string DeleteSTP(int SampleTypeProductId)
        {
            return CoreFactory.productSubMatrix.DeleteSTP(SampleTypeProductId);
        }
    }
}