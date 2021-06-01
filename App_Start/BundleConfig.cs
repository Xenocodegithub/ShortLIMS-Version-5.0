using System.Web;
using System.Web.Optimization;

namespace LIMS_DEMO
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusive").Include(
             "~/Scripts/jquery.unobtrusive-ajax.js"
             ));
            bundles.Add(new ScriptBundle("~/bundles/Home").Include(
            "~/Scripts/Application/Home.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Site/js").Include(
               "~/Component/scripts/core.js",
               "~/Component/scripts/script.min.js",
               "~/Component/scripts/process.js",
               "~/Component/scripts/layout-settings.js",
               "~/Component/plugins/datatables/js/jquery.dataTables.min.js",
               "~/Component/plugins/datatables/js/dataTables.bootstrap4.min.js",
               "~/Component/plugins/datatables/js/dataTables.responsive.min.js",
                "~/Component/plugins/datatables/js/responsive.bootstrap4.min.js",
                //"~/Component/scripts/dashboard.js",
               "~/Component/plugins/datatables/js/dataTables.buttons.min.js",
               "~/Component/plugins/datatables/js/buttons.bootstrap4.min.js",
               "~/Component/plugins/datatables/js/buttons.print.min.js",
               "~/Component/plugins/datatables/js/buttons.html5.min.js",
               "~/Component/plugins/datatables/js/buttons.flash.min.js",
               "~/Component/plugins/datatables/js/pdfmake.min.js",
               "~/Component/plugins/datatables/js/vfs_fonts.js",
               "~/Component/plugins/datatables/js/datatable-setting.js"
               //"~/Component/plugins/apexcharts/apexcharts.min.js"
               ));
            //add reference css
            bundles.Add(new StyleBundle("~/Site/css").Include(
                "~/Component/styles/core.css",
                "~/Component/styles/icon-font.min.css",
                "~/Component/styles/dataTables.bootstrap4.min.css",
                "~/Component/styles/responsive.bootstrap4.min.css",
                "~/Component/fonts/font-awesome/css/font-awesome.min.css"
               // "~/Component/styles/style.css"
                ));

            #region-----------------Inventory Managemnt------------------

            bundles.Add(new ScriptBundle("~/bundles/InventoryTypeMaster").Include(
                "~/Scripts/Application/InventoryTypeMaster.js"));

            bundles.Add(new ScriptBundle("~/bundles/CategoryMaster").Include(
                 "~/Scripts/Application/CategoryMaster.js"));

            bundles.Add(new ScriptBundle("~/bundles/InventoryVendorMaster").Include(
                 "~/Scripts/Application/InventoryVendorMaster.js"));

            bundles.Add(new ScriptBundle("~/bundles/InventoryItemMaster").Include(
                 "~/Scripts/Application/InventoryItemMaster.js"));

            bundles.Add(new ScriptBundle("~/bundles/InventoryCommercialDetails").Include(
                 "~/Scripts/Application/InventoryCommercialDetails.js"));


            bundles.Add(new ScriptBundle("~/bundles/InventoryBasicDetail").Include(
                 "~/Scripts/Application/InventoryBasicDetail.js"));

            bundles.Add(new ScriptBundle("~/bundles/MaintainanceDetails").Include(
                 "~/Scripts/Application/MaintainanceDetails.js"));

            bundles.Add(new ScriptBundle("~/bundles/CalibrationDetails").Include(
                 "~/Scripts/Application/CalibrationDetails.js"));

            bundles.Add(new ScriptBundle("~/bundles/InventoryMaintainanceAndCalibration").Include(
                 "~/Scripts/Application/InventoryMaintainanceAndCalibration.js"));
            bundles.Add(new ScriptBundle("~/bundles/makeAjaxRequest").Include(
             "~/Scripts/Application/makeAjaxRequest.js"));

            bundles.Add(new ScriptBundle("~/bundles/ApplicationSystem").Include(
             "~/Scripts/Application/ApplicationSystem.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapdialogjs").Include(
                    "~/Scripts/bootstrap-dialog.js"
                    ));
            bundles.Add(new ScriptBundle("~/bundles/jquerydatatable").Include(
                   "~/Scripts/jquery.dataTables.js"));
            bundles.Add(new ScriptBundle("~/bundles/MaintainanceDetails").Include(
                "~/Scripts/Application/MaintainanceDetails.js"));

            bundles.Add(new ScriptBundle("~/bundles/CalibrationDetails").Include(
              "~/Scripts/Application/CalibrationDetails.js"));

            bundles.Add(new ScriptBundle("~/bundles/InventoryNonConsumableList").Include(
             "~/Scripts/Application/InventoryNonConsumableList.js"));


            //bundles.Add(new ScriptBundle("~/bundles/momentjs").Include(
            //        "~/assets/js/moment.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                "~/Content/fullcalendar/dist/moment.js"

                ));

            bundles.Add(new ScriptBundle("~/bundles/FullCalendar_JS").Include(
               "~/Content/fullcalendar/dist/fullcalendar.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/InventoryCapacityMaster").Include(
          "~/Scripts/Application/InventoryCapacityMaster.js"));

            #endregion
        }
    }
}
