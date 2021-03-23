using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Collection;
using LIMS_DEMO.Core.Repository;
namespace LIMS_DEMO.DAL.Collection
{
    public class SurveyingTeamLeadDAL : ISurveyingTeamLead
    {
        readonly LIMSEntities _dbContext;
        public SurveyingTeamLeadDAL()
        {
            _dbContext = new LIMSEntities();
        }


        public List<SurveyingTeamLeadEntity> GetSurveyingTeamList(int UserMasterID)
        {
            try
            {
                // and wo.IsAssignedTo = RoleId of Survey Team Lead
                return (from loc in _dbContext.LocationSampleCollections
                        join sc in _dbContext.SampleLocations on loc.SampleLocationId equals sc.SampleLocationId
                        join scoll in _dbContext.SampleCollections on loc.LocationSampleCollectionID equals scoll.LocationSampleCollectionID
                        join es in _dbContext.EnquirySampleDetails on scoll.EnquirySampleID equals es.EnquirySampleID 

                        join sts in _dbContext.StatusMasters on scoll.StatusId equals sts.StatusId 

                        join wo in _dbContext.WorkOrders on scoll.WorkOrderID equals wo.WorkOrderId 

                        join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId
                        join stp in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stp.SampleTypeProductId
                        //join pg in _dbContext.ProductGroupMasters on ed.ProductGroupId equals pg.ProductGroupId
                        //join sg in _dbContext.SubGroupMasters on ed.SubGroupId equals sg.SubGroupId
                        join em in _dbContext.EnquiryMasters on ed.EnquiryId equals em.EnquiryId into enqu
                        from e in enqu.DefaultIfEmpty()

                        join ctm in _dbContext.CustomerMasters on e.CustomerMasterId equals ctm.CustomerMasterId into ctm
                        from a in ctm.DefaultIfEmpty()

                            //   join wo in _dbContext.WorkOrders on scoll.WorkOrderID equals wo.WorkOrderId
                        join ctmwo in _dbContext.CustomerMasters on wo.CustomerMasterId equals ctmwo.CustomerMasterId into temp

                        from last in temp.DefaultIfEmpty()

                        where scoll.IsActive == true && es.IsActive == true && wo.AssignedToId==UserMasterID
                        select new SurveyingTeamLeadEntity()
                        {
                            LocationSampleCollectionID = (Int32)scoll.LocationSampleCollectionID,
                            SampleCollectionId = scoll.SampleCollectionId,
                            Location = sc.Location,
                            EnquirySampleID = scoll.EnquirySampleDetail.EnquirySampleID,
                            EnquiryDetailId = es.EnquiryDetailId == null ? 0 : es.EnquiryDetailId,
                            SampleName = es.SampleName,//Doubt for Sample No.
                            EmployeeId = scoll.EmployeeId, //RoleId,UserName,CollectorName
                            Iteration = scoll.Iteration,
                            CollectionDate = scoll.CollectionDate,
                            DisplaySampleName = loc.LocationSampleName,
                            //ProductGroupId = (Int32)ed.ProductGroupId,
                            //SubGroupId = (Int32)ed.SubGroupId,
                            //ProductGroupName = pg.ProductGroupName,
                            //SubGroupName = sg.SubGroupName,
                            SampleTypeProductName = stp.SampleTypeProductName,
                            EnquiryId = e.EnquiryId == null ? 0 : e.EnquiryId,
                            CustomerName = a.RegistrationName == "" || a.RegistrationName == null ? last.RegistrationName : a.RegistrationName,
                            StatusId = scoll.StatusMaster.StatusId,
                            CurrentStatus = sts.StatusName,
                            StatusCode = sts.StatusCode,
                            WorkOrderID = scoll.WorkOrder.WorkOrderId,
                            WorkOrderNo = wo.WorkOrderNo, //WorkOrderNo
                                                           // ExpectSampleCollDate = (DateTime)wo.ExpectSampleCollDate == null? DateTime.Now :(DateTime)wo.ExpectSampleCollDate,//Sample to be Collected On
                            FrequencyMasterId = es.FrequencyMasterId,// Frequency
                            Frequency = es.FrequencyMaster.Frequency,
                        }).OrderByDescending(wo => wo.WorkOrderID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SurveyingTeamLeadEntity GetSurveyingTeamLeadEntity(int SampleCollectionId)
        {
            try
            {
                return (from scoll in _dbContext.SampleCollections
                        join es in _dbContext.EnquirySampleDetails on scoll.EnquirySampleID equals es.EnquirySampleID
                        join sts in _dbContext.StatusMasters on scoll.StatusId equals sts.StatusId
                        join wo in _dbContext.WorkOrders on scoll.WorkOrderID equals wo.WorkOrderId

                        join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId
                        
                        join em in _dbContext.EnquiryMasters on ed.EnquiryId equals em.EnquiryId into em
                        from emt in em.DefaultIfEmpty()
                        
                        join ctm in _dbContext.CustomerMasters on emt.CustomerMasterId equals ctm.CustomerMasterId into ctm
                        from a in ctm.DefaultIfEmpty()

                        join ctmwo in _dbContext.CustomerMasters on wo.CustomerMasterId equals ctmwo.CustomerMasterId into temp
                        from last in temp.DefaultIfEmpty()

                        where scoll.SampleCollectionId == SampleCollectionId && scoll.IsActive
                        select new SurveyingTeamLeadEntity()
                        { 
                            LocationSampleCollectionID = (Int32)scoll.LocationSampleCollectionID,
                            SampleCollectionId = scoll.SampleCollectionId,
                            EnquirySampleID = scoll.EnquirySampleDetail.EnquirySampleID,
                            EnquiryDetailId = es.EnquiryDetailId,
                            SampleName = es.SampleName,//Doubt for Sample No.
                            EmployeeId = scoll.EmployeeId, //RoleId,UserName,CollectorName
                            Iteration = scoll.Iteration,
                            DisplaySampleName = es.DisplaySampleName,
                            EnquiryId = emt.EnquiryId == null ? 0 : emt.EnquiryId,
                            //   CustomerName = ctm.RegistrationName,
                            CustomerName = a.RegistrationName == "" || a.RegistrationName == null ? last.RegistrationName : a.RegistrationName,
                            StatusId = scoll.StatusMaster.StatusId,
                            CurrentStatus = sts.StatusName,
                            StatusCode = sts.StatusCode,
                            WorkOrderID = scoll.WorkOrder.WorkOrderId,
                            WorkOrderNo = wo.WorkOrderNo, //WorkOrderNo
                            ExpectSampleCollDate = (DateTime)wo.ExpectSampleCollDate,//Sample to be Collected On

                            FrequencyMasterId = es.FrequencyMasterId,// Frequency
                            Frequency = es.FrequencyMaster.Frequency,
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string AddCollector(SurveyingTeamLeadEntity surveyingTeamEntity)
        {
            try
            {
                var sampleCollection = _dbContext.SampleCollections.Find(surveyingTeamEntity.SampleCollectionId);
                sampleCollection.SampleCollectionId = surveyingTeamEntity.SampleCollectionId;
                sampleCollection.EmployeeId = surveyingTeamEntity.EmployeeId;
                _dbContext.SaveChanges();

                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }

        }

        public string AddCollectedBy(SurveyingTeamLeadEntity surveyingTeamEntity)
        {
            //try
            //{
            //    var enquirySample = _dbContext.EnquirySampleDetails.Find(surveyingTeamEntity.EnquirySampleID);
            //    enquirySample.EnquirySampleID = surveyingTeamEntity.EnquirySampleID;
            //    enquirySample.SampleCollectedBy = surveyingTeamEntity.SampleCollectedBy;
            //    _dbContext.SaveChanges();
            //    return "success";
            //}
            //catch (Exception ex)
            //{
            //    return ex.InnerException.Message;
            //}
            try
            {
                var enquirySample = _dbContext.LocationSampleCollections.Find(surveyingTeamEntity.LocationSampleCollectionID);
                enquirySample.SampleCollectedBy = surveyingTeamEntity.SampleCollectedBy;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
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
