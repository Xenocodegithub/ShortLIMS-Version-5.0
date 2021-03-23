using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.Core.Repository;
namespace LIMS_DEMO.DAL.Enquiry
{
    public class EnquiryDAL : IEnquiry
    {
        readonly LIMSEntities _dbContext;
        public EnquiryDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public List<EnquiryEntity> GetEnquiries()
        {
            try
            {
                return (from e in _dbContext.EnquiryMasters
                        join mdc in _dbContext.ModeOfCommunications on e.ModeOfCommunicationId equals mdc.ModeOfCommunicationId
                        join ctm in _dbContext.CustomerMasters on e.CustomerMasterId equals ctm.CustomerMasterId
                        join es in _dbContext.StatusMasters on e.StatusId equals es.StatusId
                        where e.IsActive
                        select new EnquiryEntity()
                        {
                            EnquiryId = e.EnquiryId,
                            Remarks = e.Remarks,
                            EnquiryOn = e.EnquiryOn,
                            CustomerMasterId = e.CustomerMasterId,
                            IsActive = e.IsActive,
                            CommunicationMode = mdc.CommunicationMode,
                            CustomerName = ctm.RegistrationName,
                            CurrentStatus = es.StatusName,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public long Add(EnquiryEntity enquiryEntity)
        {
            try
            {
                var enquiry = new EnquiryMaster()
                {
                    EnquiryId = enquiryEntity.EnquiryId,
                    Remarks = enquiryEntity.Remarks,
                    EnquiryOn = enquiryEntity.EnquiryOn,
                    ModeOfCommunicationId = enquiryEntity.ModeOfCommunicationId,
                    CustomerMasterId = enquiryEntity.CustomerMasterId,
                    StatusId = enquiryEntity.StatusId,
                    LabMasterId = enquiryEntity.LabMasterId,
                    IsActive = enquiryEntity.IsActive,
                    EnteredBy = enquiryEntity.EnteredBy,
                    EnteredDate = DateTime.Now
                };
                _dbContext.EnquiryMasters.Add(enquiry);
                _dbContext.SaveChanges();
                return enquiry.EnquiryId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string Update(EnquiryEntity enquiryEntity)
        {
            try
            {
                var enquiryMaster = _dbContext.EnquiryMasters.Find(enquiryEntity.EnquiryId);
                enquiryMaster.EnquiryId = enquiryEntity.EnquiryId;
                enquiryMaster.Remarks = enquiryEntity.Remarks;
                enquiryMaster.EnquiryOn = enquiryEntity.EnquiryOn;
                enquiryMaster.ModeOfCommunicationId = enquiryEntity.ModeOfCommunicationId;
                enquiryMaster.CustomerMasterId = enquiryEntity.CustomerMasterId;
                enquiryMaster.StatusId = enquiryEntity.StatusId;
                enquiryMaster.LabMasterId = enquiryEntity.LabMasterId;
                enquiryMaster.IsActive = enquiryEntity.IsActive;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string UpdateEnquiryStatus(long EnquiryId, int StatusId)
        {
            try
            {
                var enquiryMaster = _dbContext.EnquiryMasters.Find(EnquiryId);
                enquiryMaster.StatusId = (byte)StatusId;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        ///////////////////QuotationNo////////////////////////////////////////
        public string GenerateQuotationNo(long QuotationId)
        {
            try
            {
                var quotations = _dbContext.Quotations.Find(QuotationId);
                string CompanyCode = "MEEPL";
                string CityCode = "NAG";
                //string Year = DateTime.Now.ToString("yyyy");
                string Year = GetCurrentFinancialYear();
                long quotationCount = GetQuotationCount(Convert.ToInt32(DateTime.Now.Year));
                long SrNumber = 0;
                if (quotationCount == 0)
                {
                    SrNumber = 1;//Doubt 
                    AddQuotationNo(1, DateTime.Now.Year);
                }
                else
                {
                    SrNumber = quotationCount + 1;
                    UpdateQuotationNo(SrNumber, DateTime.Now.Year);
                }
                quotations.QuotationNo = CompanyCode + "/" + CityCode + "/" + Year + "/" + SrNumber;
                quotations.MailedOn = DateTime.UtcNow;
                //quotations.MailedTo = ClientEmail;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public long GetQuotationCount(int Year)
        {
            var quotNumber = (from e in _dbContext.QuotationNumbers
                              where e.Year == Year
                              select new QuotationNumberEntity()
                              {
                                  QuotationCount = e.QuotationCount,
                              }
                  ).FirstOrDefault();
            if (quotNumber != null)
            {
                return quotNumber.QuotationCount;
            }
            else { return 0; }
        }
        public long AddQuotationNo(long QuotationCount, int Year)
        {
            try
            {
                var quotation = new QuotationNumber()
                {
                    QuotationCount = QuotationCount,
                    Year = Year,
                    IsActive = true,
                    EnteredBy = 1,
                    EnteredDate = DateTime.Now
                };
                _dbContext.QuotationNumbers.Add(quotation);
                _dbContext.SaveChanges();
                return quotation.QuotationNumberId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string UpdateQuotationNo(long QuotationCount, int Year)
        {
            try
            {
                var quotNumber = (from e in _dbContext.QuotationNumbers
                                  where e.Year == Year
                                  select new QuotationNumberEntity()
                                  {
                                      QuotationNumberId = e.QuotationNumberId,
                                  }
                 ).FirstOrDefault();

                var quotationMaster = _dbContext.QuotationNumbers.Find(quotNumber.QuotationNumberId);
                quotationMaster.QuotationCount = QuotationCount;
                quotationMaster.Year = Year;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            string FinYear = null;

            if (DateTime.Today.Month > 3)
                FinYear = CurYear + "-" + NexYear;
            else
                FinYear = PreYear + "-" + CurYear;
            return FinYear.Trim();
        }
        ///////////////////QuotationNo////////////////////////////
        public string DeleteEnquiry(long EnquiryId, bool IsActive)
        {
            try
            {
                var enquiryMaster = _dbContext.EnquiryMasters.Find(EnquiryId);
                // if Status is Before Quotation Sent then hard delete
                //_dbContext.EnquiryMasters.Remove(enquiryMaster);
                //else Soft delete
                enquiryMaster.IsActive = IsActive;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return "failure"; //ex.InnerException.Message;
            }

        }
        public EnquiryEntity GetDetails(int EnquiryId)
        {
            return (from e in _dbContext.EnquiryMasters
                    join c in _dbContext.CustomerMasters on e.CustomerMasterId equals c.CustomerMasterId
                    into enquiry
                    from enq in enquiry.DefaultIfEmpty()
                    where e.EnquiryId == EnquiryId && e.IsActive
                    select new EnquiryEntity()
                    {
                        EnquiryId = e.EnquiryId,
                        Remarks = e.Remarks,
                        EnquiryOn = e.EnquiryOn,
                        StatusId = e.StatusId,
                        StatusCode = e.StatusMaster.StatusCode,
                        CustomerMasterId = e.CustomerMasterId,
                        CustomerName = enq.RegistrationName
                    }
             ).FirstOrDefault();
        }
        public List<EnquiryEntity> GetParameterDetails(int EnquiryId)
        {
            try
            {
                return (from epd in _dbContext.EnquiryParameterDetails
                        join esd in _dbContext.EnquirySampleDetails on epd.EnquirySampleID equals esd.EnquirySampleID
                        join ed in _dbContext.EnquiryDetails on esd.EnquiryDetailId equals ed.EnquiryDetailId
                        join em in _dbContext.EnquiryMasters on ed.EnquiryId equals em.EnquiryId
                        where em.EnquiryId == EnquiryId
                        select new EnquiryEntity()
                        {
                            EnquiryParameterDetailID = epd.EnquiryParameterDetailID,
                            EnquirySampleID = esd.EnquirySampleID,
                            EnquiryDetailId = ed.EnquiryDetailId,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<EnquiryLogEntity> GetEnquirLog(int EnquiryId)
        {
            try
            {
                return (from elog in _dbContext.EnquiryLogs
                        join em in _dbContext.EnquiryMasters on elog.EnquiryId equals em.EnquiryId
                        join mdc in _dbContext.ModeOfCommunications on elog.ModeOfCommunicationId equals mdc.ModeOfCommunicationId
                        join ctm in _dbContext.CustomerMasters on em.CustomerMasterId equals ctm.CustomerMasterId
                        where elog.EnquiryId == EnquiryId
                        select new EnquiryLogEntity()
                        {
                            EnquiryLogId = elog.EnquiryLogId,
                            EnquiryId = elog.EnquiryId,
                            ModeOfCommunicationId = elog.ModeOfCommunicationId,
                            CommunicationMode = mdc.CommunicationMode,
                            CommunicationDate = elog.CommunicationDate,
                            CustomerMasterId = em.CustomerMasterId,
                            CustomerName = ctm.RegistrationName,
                            IsActive = elog.IsActive,
                            Remarks = elog.Remarks,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public EnquiryLogEntity GetEnquiryLogDetails(int EnquiryLogId)
        {
            return (from elog in _dbContext.EnquiryLogs
                    join em in _dbContext.EnquiryMasters on elog.EnquiryId equals em.EnquiryId
                    join ctm in _dbContext.CustomerMasters on em.CustomerMasterId equals ctm.CustomerMasterId
                    into enquiryLog
                    from enqLog in enquiryLog.DefaultIfEmpty()
                    where elog.EnquiryLogId == EnquiryLogId
                    select new EnquiryLogEntity()
                    {
                        EnquiryLogId = elog.EnquiryLogId,
                        EnquiryId = elog.EnquiryId,
                        CommunicationDate = elog.CommunicationDate,
                        CustomerMasterId = em.CustomerMasterId,
                        CustomerName = enqLog.RegistrationName,
                        Remarks = elog.Remarks,
                    }
                    ).FirstOrDefault();
        }
        public long AddLog(EnquiryLogEntity enquiryLogEntity)
        {
            try
            {
                var enquiryLog = new EnquiryLog()
                {
                    EnquiryLogId = enquiryLogEntity.EnquiryId,
                    EnquiryId = enquiryLogEntity.EnquiryId,
                    CommunicationDate = enquiryLogEntity.CommunicationDate,
                    ModeOfCommunicationId = enquiryLogEntity.ModeOfCommunicationId,
                    Remarks = enquiryLogEntity.Remarks,
                    IsActive = enquiryLogEntity.IsActive,
                    EnteredBy = enquiryLogEntity.EnteredBy,
                    EnteredDate = enquiryLogEntity.EnteredDate,

                };
                _dbContext.EnquiryLogs.Add(enquiryLog);
                _dbContext.SaveChanges();
                return enquiryLog.EnquiryLogId;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string UpdateLog(EnquiryLogEntity enquiryLogEntity)
        {
            try
            {
                var enquiryMaster = _dbContext.EnquiryLogs.Find(enquiryLogEntity.EnquiryLogId);
                enquiryMaster.EnquiryId = enquiryLogEntity.EnquiryId;
                enquiryMaster.EnquiryLogId = enquiryLogEntity.EnquiryLogId;
                enquiryMaster.CommunicationDate = enquiryLogEntity.CommunicationDate;
                enquiryMaster.ModeOfCommunicationId = enquiryLogEntity.ModeOfCommunicationId;
                enquiryMaster.Remarks = enquiryLogEntity.Remarks;
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