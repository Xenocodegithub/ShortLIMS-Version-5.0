using LIMS_DEMO.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Repository
{
    public interface IProductSubMatrix : IDisposable
    {
        ProductSubMatrixEntity GetDetailsSTP(int SampleTypeProductId);
        string AddSampleTypeProduct(ProductSubMatrixEntity productSubMatrixEntity);
        string UpdateSTP(ProductSubMatrixEntity productSubMatrixEntity);
        List<ProductSubMatrixEntity> GetSampleTypeProductList();
        ProductSubMatrixEntity GetDetailsPG(int SampleTypeProductId, int ProductGroupId);
        string AddProductGroup(ProductSubMatrixEntity productSubMatrixEntity);
        string UpdatePG(ProductSubMatrixEntity productSubMatrixEntity);
        List<ProductSubMatrixEntity> GetProductGroupList();
        string DeletePG(int SampleTypeProductId, int ProductGroupId);
        ProductSubMatrixEntity GetDetailsSG(int SampleTypeProductId, int ProductGroupId, int SubgroupId);
        string AddSubGroup(ProductSubMatrixEntity productSubMatrixEntity);
        string UpdateSG(ProductSubMatrixEntity productSubMatrixEntity);
        List<ProductSubMatrixEntity> GetSubGroupList();
        string DeleteSG(int SampleTypeProductId, int ProductGroupId, int SubGroupId);
        ProductSubMatrixEntity GetDetailsMatrix(int SampleTypeProductId, int ProductGroupId, int SubgroupId, int MatrixId);
        string AddMatrix(ProductSubMatrixEntity productSubMatrixEntity);
        string UpdateMatrix(ProductSubMatrixEntity productSubMatrixEntity);
        string DeleteMatrix(int SampleTypeProductId, int ProductGroupId, int SubGroupId, int MatrixId);
        List<ProductSubMatrixEntity> GetMatrixList();
        string DeleteSTP(int SampleTypeProductId);
    }
}
