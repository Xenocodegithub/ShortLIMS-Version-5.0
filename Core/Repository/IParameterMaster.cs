using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Configuration;


namespace LIMS_DEMO.Core.Repository
{
   public interface IParameterMaster : IDisposable
    {
        string AddParameter(ParameterMasterEntity parameterMasterEntity);
        string UpdateParameter(ParameterMasterEntity parameterMasterEntity);
        List<ParameterMasterEntity> GetParameterList();
        ParameterMasterEntity GetDetailsParameter(int ParameterMasterId);
        ParameterMasterEntity GetParameter(int ParameterMasterId);
        long GetParameterMasterMappingDetail(ParameterMasterEntity model);
        List<ParameterMasterEntity> GetParameterMasterList();
        string AddParameterMaster(ParameterMasterEntity parameterMasterEntity);
        int AddParameterGroup(ParameterMasterEntity parameterMasterEntity);
        ParameterMasterEntity GetDetails(int ParameterMappingId);
        ParameterMasterEntity GetParameterGroupDetails(int ParameterGroupId);
        string Update(ParameterMasterEntity parameterMasterEntity);
        int GetParameterDetails(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId);
        string DeleteParameterMaster(int ParameterMappingId);
        string DeleteParameter(int ParameterMasterId);
    }
}
