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
    public class UnitMasterBAL
    {
        public UnitMasterBAL()
        {
            CoreFactory.unitMaster = new UnitMasterDAL();
        }
        public string AddUnitMaster(UnitMasterEntity unitMasterEntity)
        {
            return CoreFactory.unitMaster.AddUnitMaster(unitMasterEntity);
        }
        public string UpdateUnitMaster(UnitMasterEntity unitMasterEntity)
        {
            return CoreFactory.unitMaster.UpdateUnitMaster(unitMasterEntity);
        }
        public UnitMasterEntity GetDetails(int UnitId)
        {
            return CoreFactory.unitMaster.GetDetails(UnitId);
        }
        public string DeleteUnitMaster(int UnitId)
        {
            return CoreFactory.unitMaster.DeleteUnitMaster(UnitId);
        }
        public List<UnitMasterEntity> GetUnitList()
        {
            return CoreFactory.unitMaster.GetUnitList();
        }
    }
}