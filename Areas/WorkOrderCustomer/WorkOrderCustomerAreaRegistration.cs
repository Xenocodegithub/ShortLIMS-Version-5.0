using System.Web.Mvc;

namespace LIMS_DEMO.Areas.WorkOrderCustomer
{
    public class WorkOrderCustomerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WorkOrderCustomer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WorkOrderCustomer_default",
                "WorkOrderCustomer/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}