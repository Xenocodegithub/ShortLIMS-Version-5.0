using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core;
using LIMS_DEMO.DAL;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.DAL.Configuration;


namespace LIMS_DEMO.BAL.Configuration
{
    public class ParameterMasterBAL
    {
        public ParameterMasterBAL()
        {
           CoreFactory.parameterMaster = new ParameterMasterDAL();
        }
        public string AddParameter(ParameterMasterEntity parameterMasterEntity)
        {
            return CoreFactory.parameterMaster.AddParameter(parameterMasterEntity);
        }
        public string UpdateParameter(ParameterMasterEntity parameterMasterEntity)
        {
            return CoreFactory.parameterMaster.UpdateParameter(parameterMasterEntity);
        }
        public List<ParameterMasterEntity> GetParameterList()
        {
            return CoreFactory.parameterMaster.GetParameterList();
        }
        public ParameterMasterEntity GetDetailsParameter(int ParameterMasterId)
        {
            return CoreFactory.parameterMaster.GetDetailsParameter(ParameterMasterId);
        }
        public int GetParameterDetails(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId)
        {
            return CoreFactory.parameterMaster.GetParameterDetails(SampleTypeProductId, ProductgroupId, SubgroupId, MatrixId);
        }
        public int AddParameterGroup(ParameterMasterEntity parameterMasterEntity)
        {
            return CoreFactory.parameterMaster.AddParameterGroup(parameterMasterEntity);
        }
        public ParameterMasterEntity GetParameterGroupDetails(int ParameterGroupId)
        {
            return CoreFactory.parameterMaster.GetParameterGroupDetails(ParameterGroupId);
        }
        public long GetParameterMasterMappingDetail(ParameterMasterEntity model)
        {
            return CoreFactory.parameterMaster.GetParameterMasterMappingDetail(model);
        }
        public string AddParameterMaster(ParameterMasterEntity parameterMasterEntity)
        {
            return CoreFactory.parameterMaster.AddParameterMaster(parameterMasterEntity);
        }
        public string Update(ParameterMasterEntity parameterMasterEntity)
        {
            return CoreFactory.parameterMaster.Update(parameterMasterEntity);
        }
        public List<ParameterMasterEntity> GetParameterMasterList()
        {
            return CoreFactory.parameterMaster.GetParameterMasterList();
        }
        public string DeleteParameterMaster(int ParameterMappingId)
        {
            return CoreFactory.parameterMaster.DeleteParameterMaster(ParameterMappingId);
        }
        public string DeleteParameter(int ParameterMasterId)
        {
            return CoreFactory.parameterMaster.DeleteParameter(ParameterMasterId);
        }
    }
}