using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Application.Web.UI.Reporting
{
    public partial class frmShowReport : System.Web.UI.Page
    {
        #region Private Properties
        #region wrappers for web.config
        /// <summary>
        /// Report Path setting that was stored in the web.config
        /// </summary>

        string Report_No = "", Send = "", FileName = "", Report_Type = "";
        DateTime Reporting_date = new DateTime();
        private string ReportPath
        {
            get
            {
                //If you get a compile error on the next line, you can fix it by setting - Project, Properties, Settings: add a setting called ReportPath, with type "String", with a value "/Reports" (matching the Reports folder in this project);
                //It is also stored in the web.config file, which is nice when you go to deploy your project.
                //string reportPath = Properties.Settings.Default.ReportPath;
                // "../Invoices/showserviceinvoice.aspx?Path=invoice&ReqId=1"
                string reportPath = "../Reports/";
                if (reportPath.StartsWith("/"))
                    return Server.MapPath(reportPath);
                else if (Directory.Exists(reportPath))
                    return reportPath;
                else
                    return Server.MapPath(reportPath);
            }
            set { }
        }

        /// <summary>
        //Database connection string that was stored in the web.config
        /// </summary>
        private string DBConnectionString
        {
            get
            {
                //If you get a compile error on the next line, you can fix it 
                //by setting Project, Properties, Settings: 
                //add a setting called DnsReport, with type "ConnectionString", pointed at your DB;
                //return Properties.Settings.Default.DsnReport;
                string _connectioString = Convert.ToString(ConfigurationManager.ConnectionStrings["LIMS"]);
                return _connectioString;
                //return "server=ABHISHEK-PC\\SQLEXPRESS;database=NCBLIMSV1;user id=sa; pwd=admin@123;";
                //return ""; 
            }
            //set { }
        }
        #endregion

        #region Wrapper for QueryString parameters
        /// <summary>
        /// all report params that were passed-in via the QueryString
        /// </summary>
        private System.Collections.Hashtable ReportParameters
        {
            get
            {
                System.Collections.Hashtable re = new System.Collections.Hashtable();
                //gather any params so they can be passed to the report
                foreach (string key in Request.QueryString.AllKeys)
                {
                    if (key.ToLower() != "path")//ignore the “path” param. It describes the report's file path
                    {
                        re.Add(key, Request.QueryString[key]);
                    }
                }
                return re;
            }
            //set { }
        }

        /// <summary>
        /// the report file info (filename, ext, path, etc)
        /// </summary>
        private FileInfo ReportFile
        {
            get
            {
                FileInfo re = null;
                try
                {
                    string reportFullPath = "", reportName = "";

                    if (Request.QueryString["path"] != null)
                    {
                        reportName = Request.QueryString["path"];
                    }
                    if (Request.QueryString["ReportType"] != null)
                    {
                        Report_Type = Request.QueryString["ReportType"];
                    }
                    if (Request.QueryString["ReportingDate"] != null && Request.QueryString["ReportingDate"] !="undefined")
                    {
                        CultureInfo provider = CultureInfo.InvariantCulture;

                        Reporting_date = DateTime.ParseExact(Request.QueryString["ReportingDate"], "dd/MM/yyyy", provider);
                 //        Reporting_date = Reporting_date.AddMonths(1);
                
                    }
                    //check to make sure the file ACTUALLY exists, before we start working on it
                    if (File.Exists(Path.Combine(this.ReportPath, reportName)))
                    {
                        reportFullPath = Path.Combine(this.ReportPath, reportName);
                        reportName = reportName.Substring(0, reportName.LastIndexOf(".") - 1);
                    }
                    else if (File.Exists(Path.Combine(this.ReportPath, reportName + ".rdl")))
                        reportFullPath = Path.Combine(this.ReportPath, reportName + ".rdl");
                    else if (File.Exists(Path.Combine(this.ReportPath, reportName + ".rdlc")))
                        reportFullPath = Path.Combine(this.ReportPath, reportName + ".rdlc");

                    if (reportFullPath != "")
                        re = new FileInfo(reportFullPath);
                }
                finally { }

                return re;
            }
            //set { }
        }

        //FileInfo ReportFile = new FileInfo("C:\\file.txt");
        /// <summary>
        /// the Report file (.rdl/.rdlc) de-serialized into an object
        /// </summary>
        private Serialization.Report ReportDefinition
        {
            get
            {
                FileInfo reportFile = this.ReportFile;
                if (reportFile != null)
                    return Serialization.Report.GetReportFromFile(reportFile.FullName);
                else
                    return new Serialization.Report();
            }
        }
        #endregion
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // lblError.Text = "";
            if (!Page.IsPostBack)
            {
                rvReportViewer.ReportError += rvReportViewer_ReportError;
                //      call the report
                if (Request.QueryString["path"] != null)
                {
                    if (Request.QueryString["reportno"] != null)
                    {
                        Report_No = Convert.ToString(Request.QueryString["reportno"]);
                    }
                    //ShowReport(); //with data
                    //DownloadReportFile();
                    DownloadReport1();


                    string reportFullPath = this.ReportFile.ToString();
                }
                else
                {
                    ShowBlankReport(); //blank (default)
                }
            }
        }
        protected void ReportViewer_OnLoad(object sender, EventArgs e)
        {
            //string exportOption = "Excel";
            //string exportOption = "Word";
            //string exportOption = "PDF";
            //RenderingExtension extension = rvReportViewer.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
            //if (extension != null)
            //{
            //    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            //    fieldInfo.SetValue(extension, false);
            //}
            //foreach (RenderingExtension extension in rvReportViewer.LocalReport.ListRenderingExtensions())
            //{
            //    if (extension.Name == "IMAGE" || extension.Name == "EXCELOPENXML")
            //    {
            //        FieldInfo fi = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
            //        fi.SetValue(extension, false);
            //    }
            //}
        }
        void rvReportViewer_ReportError(object sender, ReportErrorEventArgs e)
        {
            //lblError.Text = e.Exception.Message;
            //throw new NotImplementedException();
        }
        #region Private Helper Methods
        #region  Show Report
        /// <summary>
        /// Simple example to show the basics and to test that your initial config and prerequisites are correct
        /// </summary>
        private void ShowBlankReport()
        {
            //look up the report file
            string reportPath = ReportPath + "Example.rdlc";

            //In the example report, there are no parameters to load
            //Otherwise, I would do that first

            //attach an empty dataset, because all reports need a dataset
            System.Data.DataSet ds = new System.Data.DataSet() { Tables = { new System.Data.DataTable() } };
            ReportDataSource rds = new ReportDataSource("dsTableList", ds.Tables[0]);
            rvReportViewer.LocalReport.DataSources.Clear();
            rvReportViewer.LocalReport.DataSources.Add(rds);

            //apply the data to the report
            rvReportViewer.LocalReport.Refresh();

            //the viewer will load the report definition from the file
            rvReportViewer.LocalReport.ReportPath = reportPath;
        }

        /// <summary>
        /// Run one of your reports
        /// </summary>
        protected void ShowReport()
        {
            //adapted from http://www.codeproject.com/Articles/37845/Using-RDLC-and-DataSets-to-develop-ASP-NET-Reporti

            FileInfo reportFullPath = this.ReportFile;
            //check to make sure the file ACTUALLY exists, before we start working on it
            if (reportFullPath != null)
            {
                //map the reporting engine to the .rdl/.rdlc file
                LoadReportDefinitionFile(rvReportViewer.LocalReport, reportFullPath);

                //  1. Clear Report Data
                rvReportViewer.LocalReport.DataSources.Clear();

                //  2. Load new data
                // Look-up the DB query in the "DataSets" element of the report file (.rdl/.rdlc which contains XML)
                Serialization.Report reportDef = this.ReportDefinition;

                // Run each query (usually, there is only one) and attach each to the report
                foreach (Serialization.DataSet ds in reportDef.DataSets)
                {
                    //copy the parameters from the QueryString into the ReportParameters definitions (objects)
                    ds.AssignParameters(this.ReportParameters);

                    //run the query to get real data for the report
                    System.Data.DataTable tbl = ds.GetDataTable(DBConnectionString);

                    //attach the data/table to the Report's dataset(s), by name
                    ReportDataSource rds = new ReportDataSource();
                    rds.Name = ds.Name; //This refers to the dataset name in the RDLC file
                    rds.Value = tbl;
                    rvReportViewer.LocalReport.DataSources.Add(rds);
                }

                //Load any other report parameters (which are not part of the DB query).  
                //If any of the parameters are required, make sure they were provided, or show an error message.  Note: SSRS cannot render the report if required parameters are missing
                CheckReportParameters(rvReportViewer.LocalReport);

                rvReportViewer.LocalReport.Refresh();
                if (Report_No != null)
                {
                    string[] getdata = Report_No.Split('/');//for ex. Report No. is MPIL/00001
                    //rvReportViewer.LocalReport.DisplayName = getdata[0].ToString().Trim() +  getdata[1].ToString().Trim() +  getdata[2].ToString().Trim();
                    rvReportViewer.LocalReport.DisplayName = Report_No.ToString().Trim();
                    //Report Name generate as ReportNo.pdf in Test Report time, added by POONAM 
                }
            }
           // DownloadReportFile();
        }

        /// <summary>
        /// Generate a PDF (as a download)
        /// adapted from http://www.codeproject.com/Articles/492739/Exporting-to-Word-PDF-using-Microsoft-Report-RDLC
        /// This fcn also gives much more useful errors, if something is wrong. 
        /// I often use it for troubleshooting.
        /// </summary>
        private void DownloadReportFile()
        {
            //this SSRS object allows us to run the report in-memory without a ReportViewer control
            LocalReport report = new LocalReport();

            FileInfo reportFullPath = this.ReportFile;
            //check to make sure the file ACTUALLY exists, before we start working on it
            if (reportFullPath != null)
            {
                //report name, minus the file extension (so the PDF will have a similar file name)
                string reportName = reportFullPath.Name.Substring(0, reportFullPath.Name.Length - reportFullPath.Extension.Length - 1);

                //map the reporting engine to the .rdl/.rdlc file
                LoadReportDefinitionFile(report, reportFullPath);

                //  1. Clear Report Data
                report.DataSources.Clear();

                //  2. Load new data
                // Look-up the DB query in the "DataSets" element of the report file (.rdl/.rdlc which contains XML)
                Serialization.Report reportDef = this.ReportDefinition;

                foreach (Serialization.DataSet ds in reportDef.DataSets)
                {
                    //copy the parameters from the QueryString into the ReportParameters definitions (objects)
                    ds.AssignParameters(this.ReportParameters);

                    //run the query to get real data for the report
                    System.Data.DataTable tbl = ds.GetDataTable(DBConnectionString);

                    //attach the data/table to the Report's dataset(s), by name
                    ReportDataSource rds = new ReportDataSource();
                    rds.Name = ds.Name; //This refers to the dataset name in the RDLC file
                    rds.Value = tbl;// { TableName="TableList", Columns = { new System.Data.DataColumn("Name"), new System.Data.DataColumn("object_id"), new System.Data.DataColumn("type") } };
                    report.DataSources.Add(rds);
                }

                //Load any other report parameters (which are not part of the DB query).  
                //If any of the parameters are required, make sure they were provided, or show an error message.  Note: SSRS cannot render the report if required parameters are missing
                CheckReportParameters(report);

                //     Run the report
                //report.LocalReport.Render("PDF", null);
                Byte[] mybytes = report.Render("PDF");
               
                //output the PDF via the binary response stream
                Response.Clear();
                Response.AddHeader("content–disposition", "attachment; filename=" + reportName + ".pdf");
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(mybytes);
                //using (FileStream fs = File.Create(@"D:\SalSlip1.pdf"))
                //{
                //    fs.Write(mybytes, 0, mybytes.Length);
                //}
            }
            else
            {
                //punt
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.Write("Error: cannot find the report file [" + Request.QueryString["Path"] + "] in the configure report path.");
            }
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            //     byte[] bytes = report.Render(
            //       "PDF", null, out mimeType, out encoding, out extension,
            //      out streamids, out warnings);
            //     string fileDirectory = "";
            //     fileDirectory = @"D:\Reports";
            //     fileDirectory = fileDirectory + @"\" + Reporting_date.Year + @"\" + Reporting_date.ToString("MMMM") + @"\" + Reporting_date.Day + @"\";
            //     // FileInfo file = new FileInfo(fileDirectory);
            //     if (!Directory.Exists(fileDirectory))
            //     {
            //         Directory.CreateDirectory(fileDirectory);
            //     }

            //    // string _fileName = @"" + Report_No.Replace('/', '%') + ".pdf";
            ////     string _fileName = @"" + Report_No+ ".pdf";
            //     string _fileName = new Regex(@"[<>:""/\\|?*]").Replace(Report_No, "-") + ".pdf";
            //     string pathString = System.IO.Path.Combine(fileDirectory, _fileName);
            //     //FileStream fs = new FileStream(Server.MapPath("~/SendFile/" + _fileName), FileMode.Create);
            //     FileStream fs = new FileStream(pathString, FileMode.Create);
            //     fs.Write(bytes, 0, bytes.Length);
            //     fs.Close();
            //     this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);

        }
        #endregion

        #region  Helpers for loading files, parameters, settings
        /// <summary>
        /// Load the .rdl/.rdlc file into the reporting engine.  Also, fix the path for any embedded graphics.
        /// </summary>
        /// <param name="report">Instance of the ReportViewer control or equiv object</param>
        /// <param name="reportFullPath">(file) path to the Reports folder (on the HDD)</param>
        private void LoadReportDefinitionFile(LocalReport report, FileInfo reportFullPath)
        {
            string xml = File.ReadAllText(reportFullPath.FullName);
            //if (xml.Contains("<Image"))
            //{
            //    string rawUrlPath = Request.Url.OriginalString.ToLower();
            //    string newImagePath = rawUrlPath.Substring(0, rawUrlPath.IndexOf("/view")) + Properties.Settings.Default.ReportPath;
            //    xml = xml.Replace("<Value>/", "<Value>" + newImagePath);

            //    //if the report contains any images or report parts, then the report viewer
            //    //needs to read the string from a stream.  Create a string reader to convert 
            //    //the xml (string) into a stream
            //    using (StringReader sr = new StringReader(xml))
            //    {
            //        //this actually changes the xml into the object
            //        report.LoadReportDefinition(sr);
            //        sr.Close();
            //    }
            //    report.EnableExternalImages = true;
            //}
            //else
            //{
            report.ReportPath = reportFullPath.FullName;
            //}
        }

        /// <summary>
        /// Note: SSRS cannot render the report if required parameters are missing.
        /// This will load any report parameters.
        /// If any of the parameters were required, but they were not provided, show an error message.
        /// </summary>
        /// <param name="report">Instance of the ReportViewer control or equiv object</param>
        private void CheckReportParameters(LocalReport report)
        {
            rvReportViewer.LocalReport.EnableExternalImages = true; // for Signature Display in Report added by...Poonam
            //copy-in any report parameters which were not part of the DB query
            ReportParameterInfoCollection rdlParams = report.GetParameters();
            foreach (ReportParameterInfo rdlParam in rdlParams)
            {
                if (this.ReportParameters.ContainsKey(rdlParam.Name))
                {

                    string val = this.ReportParameters[rdlParam.Name].ToString();
                    ReportParameter newParam = new ReportParameter(rdlParam.Name, val);
                    report.SetParameters(newParam);
                }
                else if (!rdlParam.AllowBlank)
                {
                    // lblError.Text += "Report Parameter \"" + rdlParam.Name + "\" is required, but was not provided.<br/>";
                }
            }
        }

        protected void rvReportViewer_PreRender(object sender, EventArgs e)
        {
            //DisableUnwantedExportFormat((ReportViewer)sender, "EXCELOPENXML");
            //DisableUnwantedExportFormat((ReportViewer)sender, "WORDOPENXML");
            //DisableUnwantedExportFormat((ReportViewer)sender, "Word");
            //DisableUnwantedExportFormat((ReportViewer)sender, "Pdf");
            //string exportOption = "pdf";

            //RenderingExtension extension = rvReportViewer.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
            //if (extension != null)
            //{
            //    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            //    fieldInfo.SetValue(extension, false);
            //}
        }

        protected void rvReportViewer_Load(object sender, EventArgs e)
        {
            //foreach (RenderingExtension extension in rvReportViewer.LocalReport.ListRenderingExtensions())
            //{
            //    if (extension.Name == "IMAGE" || extension.Name == "EXCELOPENXML")
            //    {
            //        FieldInfo fi = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
            //        fi.SetValue(extension, false);
            //    }
            //}
        }

        /// <summary>
        /// Hidden the special SSRS rendering format in ReportViewer control
        /// </summary>
        /// <param name="ReportViewerID">The ID of the relevant ReportViewer control</param>
        /// <param name="strFormatName">Format Name</param>
        //public void DisableUnwantedExportFormat(ReportViewer ReportViewerID, string strFormatName)
        //{
        //    FieldInfo info;
        //    foreach (RenderingExtension extension in ReportViewerID.LocalReport.ListRenderingExtensions())
        //    {
        //        if (extension.Name.Trim().ToUpper() == strFormatName.Trim().ToUpper())
        //        {
        //            info = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
        //            info.SetValue(extension, false);
        //        }
        //    }

        //}
        //Further reading:
        //sub-reports: http://www.codeproject.com/Articles/473844/Using-Custom-Data-Source-to-create-RDLC-Reports

        #endregion
        #endregion
        private void DownloadReport1()
        {
            FileInfo reportFullPath = this.ReportFile;

            if (reportFullPath != null)
            {
                string reportName = reportFullPath.Name.Substring(0, reportFullPath.Name.Length - reportFullPath.Extension.Length - 1);
                LoadReportDefinitionFile(rvReportViewer.LocalReport, reportFullPath);
                rvReportViewer.LocalReport.DataSources.Clear();
                Serialization.Report reportDef = this.ReportDefinition;
                foreach (Serialization.DataSet ds in reportDef.DataSets)
                {
                    ds.AssignParameters(this.ReportParameters);
                    //run the query to get real data for the report
                    System.Data.DataTable tbl = ds.GetDataTable(DBConnectionString);
                    //attach the data/table to the Report's dataset(s), by name
                    ReportDataSource rds = new ReportDataSource();
                    rds.Name = ds.Name; //This refers to the dataset name in the RDLC file
                    rds.Value = tbl;
                    rvReportViewer.LocalReport.DataSources.Add(rds);
                }
                CheckReportParameters(rvReportViewer.LocalReport);
                rvReportViewer.LocalReport.Refresh();
                if (Report_No != null)
                {
                    string[] getdata = Report_No.Split('/');//for ex. Report No. is MPIL/00001
                    rvReportViewer.LocalReport.DisplayName = Report_No.ToString().Trim();
                    ConvertReportToPDF(rvReportViewer.LocalReport, reportName);
                    //  string Url = ConvertReportToPDF(rvReportViewer.LocalReport, reportName);
                    // System.Diagnostics.Process.Start(Url);
                    //Report Name generate as ReportNo.pdf in Test Report time, added by POONAM 
                }
            }
        }

        //private void DownloadReport1()
        //{
        //    FileInfo reportFullPath = this.ReportFile;

        //    if (reportFullPath != null)
        //    {
        //        string reportName = reportFullPath.Name.Substring(0, reportFullPath.Name.Length - reportFullPath.Extension.Length - 1);
        //        LoadReportDefinitionFile(rvReportViewer.LocalReport, reportFullPath);
        //        rvReportViewer.LocalReport.DataSources.Clear();
        //        Serialization.Report reportDef = this.ReportDefinition;
        //        foreach (Serialization.DataSet ds in reportDef.DataSets)
        //        {
        //            ds.AssignParameters(this.ReportParameters);
        //            //run the query to get real data for the report
        //            System.Data.DataTable tbl = ds.GetDataTable(DBConnectionString);
        //            //attach the data/table to the Report's dataset(s), by name
        //            ReportDataSource rds = new ReportDataSource();
        //            rds.Name = ds.Name; //This refers to the dataset name in the RDLC file
        //            rds.Value = tbl;
        //            rvReportViewer.LocalReport.DataSources.Add(rds);
        //        }
        //        rvReportViewer.LocalReport.Refresh();
        //        if (Report_No != null)
        //        {
        //            string[] getdata = Report_No.Split('/');//for ex. Report No. is MPIL/00001
        //            rvReportViewer.LocalReport.DisplayName = Report_No.ToString().Trim();
        //           ConvertReportToPDF(rvReportViewer.LocalReport, reportName);
        //           // string Url = ConvertReportToPDF(rvReportViewer.LocalReport, reportName);
        //            // System.Diagnostics.Process.Start(Url);
        //            //Report Name generate as ReportNo.pdf in Test Report time, added by POONAM 
        //        }
        //    }
        //}
        private void  ConvertReportToPDF(LocalReport rep, string reportName)
        {
            string reportType = "PDF";
            string mimeType;
            string encoding;

            //string deviceInfo = "<DeviceInfo>" +
            //   "  <OutputFormat>PDF</OutputFormat>" +
            //   "  <PageWidth>8.27in</PageWidth>" +
            //   "  <PageHeight>6.0in</PageHeight>" +
            //   "  <MarginTop>0.2in</MarginTop>" +
            //   "  <MarginLeft>0.2in</MarginLeft>" +
            //   "  <MarginRight>0.2in</MarginRight>" +
            //   "  <MarginBottom>0.2in</MarginBottom>" +
            //   "</DeviceInfo>";

            Warning[] warnings;
            string[] streamIds;
            string extension = string.Empty;

            byte[] bytes = rep.Render(reportType, null, out mimeType, out encoding, out extension, out streamIds, out warnings);




            // string localPath = System.Configuration.ConfigurationManager.AppSettings["TempFiles"].ToString();
            // string localPath = AppDomain.CurrentDomain.BaseDirectory;
            // string localPath = "";
            //string fileName = reportName + ".pdf";
            //localPath = localPath + fileName;
            //System.IO.File.WriteAllBytes(localPath, bytes);
            //return localPath;

            ////Response.Clear();
            ////Response.AddHeader("content–disposition", "attachment; filename=" + reportName + ".pdf");
            ////Response.ContentType = "application/pdf";
            ////Response.BinaryWrite(bytes);

            using (FileStream fs = File.Create(Server.MapPath("/Reports/") + reportName + ".pdf"))
            {
                fs.Write(bytes, 0, bytes.Length);
            }

            Response.ClearHeaders();
            Response.ClearContent();
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + reportName + ".pdf");
            Response.WriteFile(Server.MapPath("~/Reports/" + reportName + ".pdf"));
            Response.Flush();
            Response.Close();
            Response.End();
        }
    }

}