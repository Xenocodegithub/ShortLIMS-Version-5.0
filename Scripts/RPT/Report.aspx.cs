using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LIMS_DEMO.DAL;
using Microsoft.Reporting.WebForms;

namespace LIMS_DEMO.RPT
{
    public partial class Report : System.Web.UI.Page
    {
        
       
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Page.IsPostBack == false)
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RPT/category.rdlc");
                LIMSEntities _dbContext = new LIMSEntities();
                //NorthwindEntities entities = new NorthwindEntities();
                ReportDataSource datasource = new ReportDataSource("InventoryDS", (from InventoryCategoryMaster in _dbContext.InventoryCategoryMasters.Take(10)

                                                                                   select InventoryCategoryMaster));

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();
            }


            // if (!IsPostBack)
            //{
            //    List<MVCReportViwer.Customer> customers = null;
            //    using (MVCReportViwer.MyDatabaseEntities dc = new MVCReportViwer.MyDatabaseEntities())
            //    {
            //        customers = dc.Customers.OrderBy(a => a.CustomerID).ToList();
            //        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RPTReports/rptCustomer.rdlc");
            //        ReportViewer1.LocalReport.DataSources.Clear();
            //        ReportDataSource rdc = new ReportDataSource("MyDataset", customers);
            //        ReportViewer1.LocalReport.DataSources.Add(rdc);
            //        ReportViewer1.LocalReport.Refresh();
            //    }
            //}

        }
    }
}