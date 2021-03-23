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
    public class FoodAndAgriProductsBAL
    {
        public FoodAndAgriProductsBAL()
        {
            CoreFactory.fieldDetermination = new FoodAndAgriProductsDAL();
        }
        public string AddFoodProducts(FoodAndAgriProductsEntity foodAndAgriProductsEntity)
        {
            return CoreFactory.fieldDetermination.AddFoodProducts(foodAndAgriProductsEntity);
        }
        public FDFoodInfo GetFoodDetails(int FieldFoodAndAgriCultureId, int SampleCollectionId)
        {
            return CoreFactory.fieldDetermination.GetFoodDetails(FieldFoodAndAgriCultureId, SampleCollectionId);
        }
        public string DeleteFoodAgriField(long FieldFoodAndAgriCultureId)
        {
            return CoreFactory.fieldDetermination.DeleteFoodAgriField(FieldFoodAndAgriCultureId);

        }
    }
}
