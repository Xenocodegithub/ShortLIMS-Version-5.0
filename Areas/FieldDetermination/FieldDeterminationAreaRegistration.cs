using System.Web.Mvc;

namespace LIMS_DEMO.Areas.FieldDetermination
{
    public class FieldDeterminationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FieldDetermination";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FieldDetermination_default",
                "FieldDetermination/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}