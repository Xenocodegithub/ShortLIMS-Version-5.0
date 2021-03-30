using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.Core.Repository;

namespace LIMS_DEMO.DAL.Enquiry
{
    public class QuotationDAL : IQuotation
    {
        readonly LIMSEntities _dbContext;

        public QuotationDAL()
        {
            _dbContext = new LIMSEntities();
        }

        public List<QuotationEntity> GetQuotationList(int LabMasterId, DateTime? FromDate, DateTime? ToDate)
        {
            return (from em in _dbContext.EnquiryMasters
                    join q in _dbContext.Quotations on em.EnquiryId equals q.EnquiryId
                    join e in _dbContext.EnquiryMasters on q.EnquiryId equals e.EnquiryId
                    join c in _dbContext.CustomerMasters on e.CustomerMasterId equals c.CustomerMasterId
                    join s in _dbContext.StatusMasters on e.StatusId equals s.StatusId
                    into workOrder
                    from wo in workOrder.DefaultIfEmpty()
                    where e.LabMasterId == LabMasterId 
                    && ((FromDate == null && ToDate == null) || (e.EnquiryOn >= FromDate && e.EnquiryOn <= ToDate)) 
                    && (wo.StatusCode.Trim() == "QuotApprov" || wo.StatusCode.Trim() == "QuotSENT" || wo.StatusCode.Trim() == "WORejected")
                    select new QuotationEntity()
                    {
                        EnquiryId = em.EnquiryId,
                        QuotationId = q.QuotationId,
                        QuotationNo=q.QuotationNo,
                        CustomerName = c.RegistrationName,
                        Status = wo.StatusName,
                        StatusCode = wo.StatusCode
                    }).OrderByDescending(wo => wo.EnquiryId).ToList();
        }

        public QuotationPreviewEntity GetQuotationPreview(long EnquiryId)
        {
            return (from e in _dbContext.EnquiryMasters
                    join l in _dbContext.LabMasters on e.LabMasterId equals l.LabMasterId
                    join q in _dbContext.Quotations on e.EnquiryId equals q.EnquiryId
                    join c in _dbContext.CustomerMasters on e.CustomerMasterId equals c.CustomerMasterId
                    where e.EnquiryId == EnquiryId
                    select new QuotationPreviewEntity()
                    {
                        EnquiryId = e.EnquiryId,
                        QuotationId = q.QuotationId,
                        LabName = l.LabBranchName,
                        RefName = q.QuotationNo,
                        CompanyName = c.RegistrationName,
                        ContactPerson = c.ContactPersonName,
                        Designation = c.ContactDesignation,
                        ClientMobileNo = c.MobileNo,
                        ContactEmail = c.ContactEmail,
                        //PAN = ,
                        //GSTNO = ,
                        //SACNO = ,
                        //BANKERS = ,
                        //PFNO = ,
                        //ESICNO = ,
                        //PTNO = ,
                        //TDSNO = ,
                        //MSMEDA = ,
                        //IECNO = ,
                        LABAddress = (l.Address1 == null ? "" : l.Address1) + " " + (l.Address2 == null ? "" : l.Address2) + " " + (l.CityName == null ? "" : l.CityName) + " " + (l.StateName == null ? "" : l.StateName) + " " + (l.Pincode == null ? "" : l.Pincode),
                        LABPhone = l.ContactNo,
                        LABMobileFax = l.MobileNo,
                        LABEmail = l.Email,
                        RevisedDates = "",
                        ContactMobile = c.ContactMobileNo,
                        ClientFax = c.FaxNo,
                        LABCity = l.CityName
                    }).FirstOrDefault();
        }

        public string GetQuotationRevisedDates(long EnquiryId)
        {
            string strRevisedDates = "";
            var QuotationDate = _dbContext.Quotations.Where(q => q.EnquiryId == EnquiryId).Select(q => new { q.EnteredDate }).FirstOrDefault();

            if (QuotationDate.EnteredDate != null)
            {
                strRevisedDates = QuotationDate.EnteredDate.ToString("dd.MM.yyyy");
            }
            var RevisedDates = _dbContext.QuotationLogs.Where(ql => ql.EnquiryId == EnquiryId).Select(ql => new { ql.RevisedOn }).ToList();
            foreach (var d in RevisedDates)
            {
                if (d.RevisedOn != null)
                {
                    if (strRevisedDates == "")
                    {
                        strRevisedDates = Convert.ToDateTime(d.RevisedOn).ToString("dd.MM.yyyy");
                    }
                    else
                    {
                        strRevisedDates = strRevisedDates + ", " + Convert.ToDateTime(d.RevisedOn).ToString("dd.MM.yyyy");
                    }
                }
            }
            return strRevisedDates;
        }

        public string GetParamters(long EnquirySampleID)
        {
            var parameters = (from ep in _dbContext.EnquiryParameterDetails
                              join pm in _dbContext.ParameterMappings on ep.ParameterMappingId equals pm.ParameterMappingId
                              join p in _dbContext.ParameterMasters on pm.ParameterMasterId equals p.ParameterMasterId
                              where ep.EnquirySampleID == EnquirySampleID && ep.IsActive == true && pm.IsActive == true && p.IsActive == true
                              select new
                              {
                                  p.ParameterName
                              }).ToList();
            if (parameters.Count() > 0)
            {
                string strParameters = "";
                foreach(var parm in parameters)
                {
                    if (strParameters == "")
                    {
                        strParameters = Convert.ToString(parm.ParameterName);
                    }
                    else
                    {
                        strParameters = strParameters + ", " + Convert.ToString(parm.ParameterName);
                    }
                }
                return strParameters;
            }
            else
            {
                return "";
            }
        }

        public HeadOfficeEntity GetHeadOfficeDetails(int LabMasterId)
        {
            HeadOfficeEntity HOEntity = new HeadOfficeEntity();
            int ParentId = _dbContext.LabMasters.Where(l => l.LabMasterId == LabMasterId).Select(l => l.ParentID == null ? l.LabMasterId : l.ParentID).FirstOrDefault().Value;
            HOEntity = _dbContext.LabMasters.Where(l => l.LabMasterId == ParentId).Select(l => new HeadOfficeEntity()
            {
                HOAddress = (l.Address1 == null ? "" : l.Address1) + " " + (l.Address2 == null ? "" : l.Address2) + " " + (l.CityName == null ? "" : l.CityName) + " " + (l.StateName == null ? "" : l.StateName) + " " + (l.Pincode == null ? "" : l.Pincode),
                HOEmail = l.Email,
                HOMobileFax = l.MobileNo,
                HOPhone = l.ContactNo,
            }).FirstOrDefault();

            var Manager = (from ud in _dbContext.UserDetails
                           join u in _dbContext.UserLabs on ud.UserMasterID equals u.UserMasterId
                           join ur in _dbContext.UserRoles on u.UserMasterId equals ur.UserMasterId
                           join r in _dbContext.RoleMasters on ur.RoleId equals r.RoleId
                           where u.LabMasterId == LabMasterId && ud.IsActive == true && u.IsActive == true && ur.IsActive == true && r.IsActive == true && r.RoleName == "Branch Manager"
                           select new HeadOfficeEntity()
                           {
                               ManagerName = (ud.FirstName == null ? "" : ud.FirstName) + " " + (ud.LastName == null ? "" : ud.LastName),
                               ManagerPhone = ud.Mobile
                           }).FirstOrDefault();
            if (Manager != null)
            {
                HOEntity.ManagerName = Manager.ManagerName;
                HOEntity.ManagerPhone = Manager.ManagerPhone;
            }
            return HOEntity;
        }

        public List<QuotationLogEntity> GetQuotationLog(long EnquiryId)
        {
            return (from q in _dbContext.QuotationLogs
                    join e in _dbContext.EnquiryMasters on q.EnquiryId equals e.EnquiryId
                    join quot in _dbContext.Quotations on e.EnquiryId equals quot.EnquiryId
                    join c in _dbContext.CustomerMasters on e.CustomerMasterId equals c.CustomerMasterId
                    where q.EnquiryId == EnquiryId && q.IsActive == true
                    select new QuotationLogEntity()
                    {
                        QuotationLogId = q.QuotationLogId,
                        EnquiryId = q.EnquiryId,
                        QuotedAmount = q.QuotedAmount,
                        IsRevised = q.IsActive,
                        RevisedOn = q.RevisedOn,
                        StatusUpdatedOn = q.StatusUpdatedOn,
                        Remarks = q.Remarks,
                        IsActive = q.IsActive,
                        CustomerName = c.RegistrationName,
                        QuotationNo = quot.QuotationNo,
                        MailedOn= quot.MailedOn,
                    }).OrderBy(q => q.QuotationLogId).ToList();
        }
       
        public QuotationLogEntity GetQuotationLogDetails(long EnquiryId)
        {
            return (from e in _dbContext.EnquiryMasters
                    join q in _dbContext.QuotationLogs on e.EnquiryId equals q.EnquiryId
                    where q.EnquiryId == EnquiryId
                    select new QuotationLogEntity()
                    {
                        QuotationLogId = q.QuotationLogId,
                        EnquiryId = q.EnquiryId,
                        QuotedAmount = q.QuotedAmount,
                        IsRevised = q.IsActive,
                        RevisedOn = q.RevisedOn,
                        StatusUpdatedOn = q.StatusUpdatedOn,
                        Remarks = q.Remarks,
                        IsActive = q.IsActive,

                    }).OrderByDescending(q => q.QuotationLogId)
                      .FirstOrDefault();
        }
        public bool AddQuotationLog(QuotationLogEntity QuoteLog)
        {
            try
            {
                _dbContext.QuotationLogs.Add(new QuotationLog()
                {
                    EnquiryId = QuoteLog.EnquiryId,
                    QuotedAmount = QuoteLog.QuotedAmount,
                    StatusUpdatedOn = QuoteLog.StatusUpdatedOn,
                    Status = QuoteLog.Status,
                    Remarks = QuoteLog.Remarks,
                    IsActive = true,
                    EnteredBy = QuoteLog.EnteredBy,
                    EnteredDate = DateTime.UtcNow
                });
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public QuotationEntity GetQuotationDetails(long EnquiryId)
        {
            return _dbContext.Quotations.Where(q => q.EnquiryId == EnquiryId).Select(q => new QuotationEntity()
            {
                QuotationId = q.QuotationId,
                EnquiryId = q.EnquiryId,
                QuotedAmount = q.QuotedAmount,
                QuotationNo=q.QuotationNo,
            }).FirstOrDefault();
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
