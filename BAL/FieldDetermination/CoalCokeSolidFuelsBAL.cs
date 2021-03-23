using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.FieldDetermination;
using LIMS_DEMO.DAL.FieldDetermination;

namespace LIMS_DEMO.BAL.FieldDetermination
{
    public class CoalCokeSolidFuelsBAL
    {
        public CoalCokeSolidFuelsBAL()
        {
            CoreFactory.fieldDetermination = new CoalCokeSolidFuelDAL();
        }
        public string AddCoal(CoalCokeSolidFuelEntity coalCokeSolidFuelEntity)
        {
            return CoreFactory.fieldDetermination.AddCoal(coalCokeSolidFuelEntity);
        }
        public FDCoalCokeInfo GetCoalDetails(int FieldId, int SampleCollectionId)
        {
            return CoreFactory.fieldDetermination.GetCoalDetails(FieldId, SampleCollectionId);
        }
        public string DeleteCoalCokeField(long CoalCokeId)
        {
            return CoreFactory.fieldDetermination.DeleteCoalCokeField(CoalCokeId);

        }
    }
}
