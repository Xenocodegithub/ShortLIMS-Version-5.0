using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Dashboard;


namespace LIMS_DEMO.Core.Repository
{
   public interface IDashboard : IDisposable
    {
        List<DashboardEntity> GetTotalEnquiry(DashboardEntity dashboard);
        List<DashboardEntity> GetTotalSampleTypeWise(DashboardEntity dashboard);
        List<DashboardEntity> GetTotalEnquiryCount();
        List<DashboardEntity> GetTotalSampleCollectedCount();
        decimal? GetTotalRevenue();
        List<DashboardEntity> GetTotalWO(DashboardEntity dashboard);
        List<DashboardEntity> GetTotalPOAmountMonthWise(DashboardEntity dashboard);
        List<DashboardEntity> GetTotalSampleMonthWise(DashboardEntity dashboard);

        decimal? GetTotalPOAmount();
        List<DashboardEntity> GetTotalWOCount();
        List<DashboardEntity> GetTotalQuotationMonthWise(DashboardEntity dashboard);
        List<DashboardEntity> GetTotalQuotationCount();
        List<DashboardEntity> GetTotalPOAmountItemWise(DashboardEntity dashboard);
        List<DashboardEntity> GetTotalWOComp(DashboardEntity dashboard);
        List<DashboardEntity> GetTotalWOExe(DashboardEntity dashboard);
        List<DashboardEntity> GetTotalWOCompCount();
        List<DashboardEntity> GetTotalWOExeCount();
        List<DashboardEntity> GetTotalSampleTestedCount();
        List<DashboardEntity> GetTotalSampleTestedMonthWise(DashboardEntity dashboard);
        List<DashboardEntity> GetTotalSampleTestedSampleTypeWise(DashboardEntity dashboard);
        List<DashboardEntity> GetTotalSampleApproved(DashboardEntity dashboard);
    }
}
