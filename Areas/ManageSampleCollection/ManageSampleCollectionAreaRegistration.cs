using System.Web.Mvc;

namespace LIMS_DEMO.Areas.ManageSampleCollection
{
    public class ManageSampleCollectionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ManageSampleCollection";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ManageSampleCollection_default",
                "ManageSampleCollection/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}