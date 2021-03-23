using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.ManageSampleCollection;
using LIMS_DEMO.Core.Repository;
namespace LIMS_DEMO.DAL.ManageSampleCollection
{
    public class ManageSampleCollectionDAL : IManageSample
    {
        readonly LIMSEntities _dbContext;

        public ManageSampleCollectionDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public string UpdateCalendarStatus(int WorkOrderSampleCollectionDateId,int Status)
        {
            try
            {
                var sampleCal = _dbContext.WorkOrderSampleCollectionDates.Find(WorkOrderSampleCollectionDateId);
                sampleCal.Status = Status;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
               return ex.InnerException.Message;
            }
        }
        public List<ManageSampleEntity> GetCalendarList()/*int UserMasterID, int CollectedBy*/
        {
            //  string _userName = Convert.ToString(UserMasterID);
            try
            {
                return (from scoll in _dbContext.SampleCollections
                        join wo in _dbContext.WorkOrders on scoll.WorkOrderID equals wo.WorkOrderId
                        join esd in _dbContext.EnquirySampleDetails on scoll.EnquirySampleID equals esd.EnquirySampleID
                        join ed in _dbContext.EnquiryDetails on esd.EnquiryDetailId equals ed.EnquiryDetailId
                        join stp in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stp.SampleTypeProductId
                        join woscd in _dbContext.WorkOrderSampleCollectionDates on scoll.WorkOrderSampleCollectionDateId equals woscd.WorkOrderSampleCollectionDateId
                        join loc in _dbContext.LocationSampleCollections on scoll.LocationSampleCollectionID equals loc.LocationSampleCollectionID
                        join sloc in _dbContext.SampleLocations on loc.SampleLocationId equals sloc.SampleLocationId
                        join cust in _dbContext.CustomerMasters on wo.CustomerMasterId equals cust.CustomerMasterId
                        where scoll.IsActive == true
                        //from woscd in _dbContext.WorkOrderSampleCollectionDates
                        //     join wo in _dbContext.WorkOrders on woscd.WorkOrderId equals wo.WorkOrderId
                        //     join scoll in _dbContext.SampleCollections on wo.WorkOrderId equals scoll.WorkOrderID
                        //     join loc in _dbContext.LocationSampleCollections on wo.WorkOrderId equals loc.WorkOrderId
                        //     join cust in _dbContext.CustomerMasters on wo.CustomerMasterId equals cust.CustomerMasterId
                        //     where woscd.IsActive == true
                        select new ManageSampleEntity()
                         {
                             WorkOrderSampleCollectionDateId = (Int32)woscd.WorkOrderSampleCollectionDateId,
                             WorkOrderId = woscd.WorkOrderId,
                             ExpectSampleCollDate = woscd.ExpectSampleCollDate,
                             Status = woscd.Status,
                             Sr = (Int32)woscd.WorkOrderSampleCollectionDateId,
                             Title = cust.RegistrationName+"/"+stp.SampleTypeProductName+"/"+ loc.LocationSampleName+"/"+sloc.Location,
                             Desc = "Dummy",
                             SampleCollectionId = scoll.SampleCollectionId,
                             Start_Date = woscd.ExpectSampleCollDate.ToString(),
                             End_Date = woscd.ExpectSampleCollDate.ToString(),

                         }).ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
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
