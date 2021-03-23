using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.Dashboard;
using LIMS_DEMO.Core.Repository;
using System.Data.Entity.Core.Objects;
using System.Threading.Tasks;



namespace LIMS_DEMO.DAL.Dashboard
{
    public class DashboardDAL : IDashboard
    {
        readonly LIMSEntities _dbContext;

        public DashboardDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public List<DashboardEntity> GetTotalEnquiry(DashboardEntity dashboard)
        {
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));
            
            List<DashboardEntity> list = new List<DashboardEntity>();

            var Result = _dbContext.USP_DashBoard_BDM(dashboard.Mode,objectParameter,dashboard.StartDate, dashboard.EndDate).ToList();
            foreach (var item in Result)
            {
                list.Add(new DashboardEntity()
                {
                   count=item.countEnquiry,
                   Month=item.Month_Name,
                   MonthCount=item.MonthEnquiry,
                   Year = item.Year,
                   

                });
            }
            return list;
        }

        public List<DashboardEntity> GetTotalSampleTypeWise(DashboardEntity dashboard)
        {
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));

            List<DashboardEntity> list = new List<DashboardEntity>();
            var Result = _dbContext.USP_DashBoard_Select(dashboard.Mode, objectParameter, dashboard.StartDate, dashboard.EndDate).ToList();
            foreach (var item in Result)
            {
                list.Add(new DashboardEntity()
                {
                    count = item.TotalSampleTypeWise,
                    SampleTypeProductName = item.SampleType,
                });
            }
            return list;
        }
        public List<DashboardEntity> GetTotalWO(DashboardEntity dashboard)
        {
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));

            List<DashboardEntity> list = new List<DashboardEntity>();
            var Result = _dbContext.USP_DashBoard_TotalWO(dashboard.Mode, objectParameter, dashboard.StartDate, dashboard.EndDate).ToList();

            foreach (var item in Result)
            {
                list.Add(new DashboardEntity()
                {
                   count = item.WOCount1,
                   Month = item.Month_Name1,
                   MonthCount = item.Month1,
                   Year = item.Year1
                });
            }
            return list;
        }
        public List<DashboardEntity> GetTotalPOAmountMonthWise(DashboardEntity dashboard)
        {
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));

            List<DashboardEntity> list = new List<DashboardEntity>();
            var Result = _dbContext.USP_DashBoard_TotalPOAmountMonthWise(dashboard.Mode, objectParameter, dashboard.StartDate, dashboard.EndDate).ToList();

            foreach (var item in Result)
            {
                list.Add(new DashboardEntity()
                {
                    TotalRevenue = item.Total,
                    Month = item.Month_Name,
                    Year = item.Year,
                    MonthCount = item.Month
                });
            }
            return list;
        }

        public List<DashboardEntity> GetTotalQuotationMonthWise(DashboardEntity dashboard)
        {
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));

            List<DashboardEntity> list = new List<DashboardEntity>();
            var Result = _dbContext.USP_DashBoard_TotalQuotationSentMonthWise(dashboard.Mode, objectParameter, dashboard.StartDate, dashboard.EndDate).ToList();

            foreach (var item in Result)
            {
                list.Add(new DashboardEntity()
                {
                    count = item.QuotationCount,
                    Month = item.MonthNameQuotation,
                    Year = item.Year,
                    MonthCount = item.MonthQuotation
                });
            }
            return list;
        }
        public List<DashboardEntity> GetTotalSampleMonthWise(DashboardEntity dashboard)
        {
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));

            List<DashboardEntity> list = new List<DashboardEntity>();
            var Result = _dbContext.USP_DashBoard_TotalSampleMonthWise(dashboard.Mode, objectParameter, dashboard.StartDate, dashboard.EndDate).ToList();
            foreach (var item in Result)
            {
                list.Add(new DashboardEntity()
                {
                    count = item.sampleCount,
                    Month = item.Month_Name,
                    Year = item.Year,
                    MonthCount = item.Month,
                });
            }
            return list;
        }
        public decimal? GetTotalRevenue()
        {
            try
            {
               var a =(from u in _dbContext.Costings
                        select new DashboardEntity()
                        {
                            TotalRevenue = u.UnitPrice

                        }).Sum(e => e.TotalRevenue);
                return a;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public decimal? GetTotalPOAmount()
        {
            try
            {
                var a = (from u in _dbContext.PurchaseRequestDetails
                         select new DashboardEntity()
                         {
                             TotalRevenue = u.NetAmount

                         }).Sum(e => e.TotalRevenue);
                return a;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public List<DashboardEntity> GetTotalWOCount()
        {
            try
            {
                return (from u in _dbContext.WorkOrders
                        select new DashboardEntity()
                        {
                            enquiry = u.WorkOrderId

                        }).OrderBy(e => e.enquiry).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<DashboardEntity> GetTotalQuotationCount()
        {
            try
            {
                return (from u in _dbContext.EnquiryMasters
                        where u.StatusId == 5 || u.StatusId==6 || u.StatusId==9
                        select new DashboardEntity()
                        {
                            enquiry = u.EnquiryId

                        }).OrderBy(e => e.enquiry).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<DashboardEntity> GetTotalEnquiryCount()
        {
            try
            {
                return (from u in _dbContext.EnquiryMasters
                        select new DashboardEntity()
                        {
                            enquiry = u.EnquiryId

                        }).OrderBy(e => e.enquiry).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<DashboardEntity> GetTotalSampleCollectedCount()
        {
            try
            {
                return (from u in _dbContext.SampleCollections
                        where u.StatusId == 28 || u.StatusId == 29 || u.StatusId == 32 || u.StatusId == 34 || u.StatusId == 35 || u.StatusId == 7 || u.StatusId == 23
                        select new DashboardEntity()
                        {
                            countsamplecollected=u.SampleCollectionId

                        }).OrderBy(e => e.countsamplecollected).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<DashboardEntity> GetTotalPOAmountItemWise(DashboardEntity dashboard)
        {
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));

            List<DashboardEntity> list = new List<DashboardEntity>();
            var Result = _dbContext.USP_DashBoard_Sample(dashboard.Mode, objectParameter, dashboard.StartDate, dashboard.EndDate).ToList();

            foreach (var item in Result)
            {
                list.Add(new DashboardEntity()
                {
                    TotalRevenue = item.Total,
                    ItemName = item.Item
                });
            }
            return list;
        }

        public List<DashboardEntity> GetTotalWOComp(DashboardEntity dashboard)
        {
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));

            List<DashboardEntity> list = new List<DashboardEntity>();
            var Result = _dbContext.USP_DashBoard_STL(dashboard.Mode, objectParameter, dashboard.StartDate, dashboard.EndDate).ToList();

            foreach (var item in Result)
            {
                list.Add(new DashboardEntity()
                {
                    count = item.WOCountExe,
                    Month = item.CustomerName,
                    //MonthCount = item.Month1,
                    //Year = item.Year1
                });
            }
            return list;
        }
        public List<DashboardEntity> GetTotalWOExe(DashboardEntity dashboard)
        {
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));

            List<DashboardEntity> list = new List<DashboardEntity>();
            var Result = _dbContext.USP_DashBoard_WOExe(dashboard.Mode, objectParameter, dashboard.StartDate, dashboard.EndDate).ToList();

            foreach (var item in Result)
            {
                list.Add(new DashboardEntity()
                {
                    count = item.WOCountExe,
                    Month = item.CustomerName,
                    
                });
            }
            return list;
        }
        public List<DashboardEntity> GetTotalWOExeCount()
        {
            try
            {
                return (from u in _dbContext.WorkOrders
                        join sc in _dbContext.SampleCollections on u.WorkOrderId equals sc.WorkOrderID
                        where sc.StatusId == 31 || sc.StatusId == 29 || sc.StatusId == 28 || sc.StatusId == 32 || sc.StatusId == 7 ||
                        sc.StatusId == 35
                        select new DashboardEntity()
                        {
                            enquiry = u.WorkOrderId

                        }).OrderBy(e => e.enquiry).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<DashboardEntity> GetTotalWOCompCount()
        {
            try
            {
                return (from u in _dbContext.WorkOrders
                        join sc in _dbContext.SampleCollections on u.WorkOrderId equals sc.WorkOrderID
                        where sc.StatusId == 31 || sc.StatusId == 29 || sc.StatusId == 28 || sc.StatusId == 32 || sc.StatusId == 7 ||
                        sc.StatusId == 35 || sc.StatusId == 23
                        select new DashboardEntity()
                        {
                            enquiry = u.WorkOrderId

                        }).OrderBy(e => e.enquiry).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<DashboardEntity> GetTotalSampleTestedMonthWise(DashboardEntity dashboard)
        {
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));

            List<DashboardEntity> list = new List<DashboardEntity>();
            var Result = _dbContext.USP_DashBoard_SampleTestedMonthWise(dashboard.Mode, objectParameter, dashboard.StartDate, dashboard.EndDate).ToList();

            foreach (var item in Result)
            {
                list.Add(new DashboardEntity()
                {
                    count = item.sampleCount,
                    Month = item.Month_Name,
                    MonthCount = item.Month,
                    Year = item.Year

                });
            }
            return list;
        }
        public List<DashboardEntity> GetTotalSampleTestedSampleTypeWise(DashboardEntity dashboard)
        {
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));

            List<DashboardEntity> list = new List<DashboardEntity>();
            var Result = _dbContext.USP_DashBoard_Planner(dashboard.Mode, objectParameter, dashboard.StartDate, dashboard.EndDate).ToList();

            foreach (var item in Result)
            {
                list.Add(new DashboardEntity()
                {
                    count = item.TotalSample,
                    Month = item.SampleType

                });
            }
            return list;
        }
        public List<DashboardEntity> GetTotalSampleTestedCount()
        {
            try
            {
                return (from sc in  _dbContext.SampleCollections
                        join esd in _dbContext.EnquirySampleDetails on sc.EnquirySampleID equals esd.EnquirySampleID
                        join ed in _dbContext.EnquiryDetails on esd.EnquiryDetailId equals ed.EnquiryDetailId
                        join stp in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stp.SampleTypeProductId
                        join spp in _dbContext.SampleParameterPlannings on sc.SampleCollectionId equals spp.SampleCollectionId
                          where spp.AnalystUserMasterID == 1
                        select new DashboardEntity()
                        {
                            enquiry = spp.SampleParameterPlanningID

                        }).OrderBy(e => e.enquiry).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<DashboardEntity> GetTotalSampleApproved(DashboardEntity dashboard)
        {
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));

            List<DashboardEntity> list = new List<DashboardEntity>();
            var Result = _dbContext.USP_DashBoard_SampleApprovedMonthWise(dashboard.Mode, objectParameter, dashboard.StartDate, dashboard.EndDate).ToList();

            foreach (var item in Result)
            {
                list.Add(new DashboardEntity()
                {
                    count = item.TotalSample,
                    Month = item.SampleType

                });
            }
            return list;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            { if (disposing) { _dbContext.Dispose(); } }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}