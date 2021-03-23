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
    public class DisciplineMasterBAL
    {
        public DisciplineMasterBAL()
        {
          CoreFactory.discipline = new DisciplineMasterDAL();
        }
        public string AddDiscipline(DisciplineMasterEntity disciplineMasterEntity)
        {
          return CoreFactory.discipline.AddDiscipline(disciplineMasterEntity);
        }
        public List<DisciplineMasterEntity> GetDisciplineList()
        {
          return CoreFactory.discipline.GetDisciplineList();
        }
        public string UpdateDiscipline(DisciplineMasterEntity disciplineMasterEntity)
        {
           return CoreFactory.discipline.UpdateDiscipline(disciplineMasterEntity);
        }
        public DisciplineMasterEntity GetDetailsDiscipline(int DisciplineId)
        {
            return CoreFactory.discipline.GetDetailsDiscipline(DisciplineId);
        }
        public string DeleteDiscipline(int DisciplineId)
        {
          return CoreFactory.discipline.DeleteDiscipline(DisciplineId);
        }
    }
}