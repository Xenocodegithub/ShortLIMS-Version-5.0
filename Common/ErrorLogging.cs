using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LIMS_DEMO.Common
{
    public class ErrorLogging
    {
        public void Error(Exception ex)
        {
            //string strPath = @"C:\Users\p10481696\Downloads\Project Lims\InfoLIMS.Common\ErrorLog\Log.txt";
            //if (!File.Exists(strPath))
            //{
            //    File.Create(strPath).Dispose();
            //}
            //using (StreamWriter sw = File.AppendText(strPath))
            //{
            //    //sw.WriteLine("=============Error Logging ===========");
            //    //sw.WriteLine("===========Start============= " + DateTime.Now);
            //    //sw.WriteLine("Error Message: " + ex.Message);
            //    //sw.WriteLine("Stack Trace: " + ex.StackTrace);
            //    //sw.WriteLine("===========End============= " + DateTime.Now);

            //}
        }
    }
}

