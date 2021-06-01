using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Dashboard;
using LIMS_DEMO.DAL.Dashboard;


namespace LIMS_DEMO.BAL.Dashboard
{
    public class DashboardBAL
    {
        public DashboardBAL()
        {
            CoreFactory.dashboard = new DashboardDAL();
        }
       
        public List<DashboardEntity> GetTotalEnquiry(DashboardEntity dashboard)
        {
            return CoreFactory.dashboard.GetTotalEnquiry(dashboard);
        }
        public List<DashboardEntity> GetTotalSampleTypeWise(DashboardEntity dashboard)
        {
            return CoreFactory.dashboard.GetTotalSampleTypeWise(dashboard);
        }
        public List<DashboardEntity> GetTotalEnquiryCount()
        {
            return CoreFactory.dashboard.GetTotalEnquiryCount();
        }
        public List<DashboardEntity> GetTotalSampleCollectedCount()
        {
            return CoreFactory.dashboard.GetTotalSampleCollectedCount();
        }
        public List<DashboardEntity> GetTotalWO(DashboardEntity dashboard)
        {
            return CoreFactory.dashboard.GetTotalWO(dashboard);
        }
        public decimal? GetTotalRevenue()
        {
            return CoreFactory.dashboard.GetTotalRevenue();
        }
        public List<DashboardEntity> GetTotalPOAmountMonthWise(DashboardEntity dashboard)
        {
            return CoreFactory.dashboard.GetTotalPOAmountMonthWise(dashboard);
        }
        public List<DashboardEntity> GetTotalSampleMonthWise(DashboardEntity dashboard)
        {
            return CoreFactory.dashboard.GetTotalSampleMonthWise(dashboard);
        }
        public decimal? GetTotalPOAmount()
        {
            return CoreFactory.dashboard.GetTotalPOAmount();
        }
        public List<DashboardEntity> GetTotalWOCount()
        {
            return CoreFactory.dashboard.GetTotalWOCount();
        }
        public List<DashboardEntity> GetTotalQuotationMonthWise(DashboardEntity dashboard)
        {
            return CoreFactory.dashboard.GetTotalQuotationMonthWise(dashboard);
        }

        public List<DashboardEntity> GetTotalQuotationCount()
        {
            return CoreFactory.dashboard.GetTotalQuotationCount();
        }
        public List<DashboardEntity> GetTotalPOAmountItemWise(DashboardEntity dashboard)
        {
            return CoreFactory.dashboard.GetTotalPOAmountItemWise(dashboard);
        }
        public List<DashboardEntity> GetTotalWOComp(DashboardEntity dashboard)
        {
            return CoreFactory.dashboard.GetTotalWOComp(dashboard);
        }
        public List<DashboardEntity> GetTotalWOExe(DashboardEntity dashboard)
        {
            return CoreFactory.dashboard.GetTotalWOExe(dashboard);
        }
        public List<DashboardEntity> GetTotalWOCompCount()
        {
            return CoreFactory.dashboard.GetTotalWOCompCount();
        }
        public List<DashboardEntity> GetTotalWOExeCount()
        {
            return CoreFactory.dashboard.GetTotalWOExeCount();
        }
        public List<DashboardEntity> GetTotalSampleTestedCount()
        {
            return CoreFactory.dashboard.GetTotalSampleTestedCount();
        }
        public List<DashboardEntity> GetTotalSampleTestedMonthWise(DashboardEntity dashboard)
        {
            return CoreFactory.dashboard.GetTotalSampleTestedMonthWise(dashboard);
        }
        public List<DashboardEntity> GetTotalSampleTestedSampleTypeWise(DashboardEntity dashboard)
        {
            return CoreFactory.dashboard.GetTotalSampleTestedSampleTypeWise(dashboard);
        }
        public List<DashboardEntity> GetTotalSampleApproved(DashboardEntity dashboard)
        {
            return CoreFactory.dashboard.GetTotalSampleApproved(dashboard);
        }
        public void Initialize()
        {
            CoreFactory.login.Initialize();
        }
    }
}