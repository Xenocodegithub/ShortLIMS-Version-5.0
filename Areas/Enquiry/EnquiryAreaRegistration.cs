using System.Web.Mvc;

namespace LIMS_DEMO.Areas.Enquiry
{
    public class EnquiryAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Enquiry";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Enquiry_default",
                "Enquiry/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}