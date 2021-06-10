using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.Core.Repository;

namespace LIMS_DEMO.DAL.Enquiry
{
    public class SampleParameterDAL : ISampleParameter
    {
        readonly LIMSEntities _dbContext;

        public SampleParameterDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public string GenerateDisplaySampleName()
        {
            try
            {
                // int Year = DateTime.Now.Year;
                long sampleCount = GetSampleCount(Convert.ToInt32(DateTime.Now.Year), Convert.ToInt32(DateTime.Today.Month));
                long SrNumber = 0;
                string DigitCode = "0000";//4 digit code
                if (sampleCount == 0 || sampleCount == null)
                {
                    SrNumber = 1;//Doubt 
                    DigitCode = "0001";
                    AddSampleNo(1, DateTime.Now.Year, DateTime.Now.Month);
                }
                else
                {
                    SrNumber = sampleCount + 1;

                    if (SrNumber.ToString().Length == 1)
                    {
                        DigitCode = "000" + SrNumber.ToString();
                    }
                    else if (SrNumber.ToString().Length == 2)
                    {
                        DigitCode = "00" + SrNumber.ToString();
                    }
                    else if (SrNumber.ToString().Length == 3)
                    {
                        DigitCode = "0" + SrNumber.ToString();
                    }
                    UpdateSampleNo(SrNumber, DateTime.Now.Year, DateTime.Now.Month);
                }
              
                var DisplaySampleName = DateTime.Now.ToString("yyMM") + "/" + DigitCode;
                return DisplaySampleName;
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public long GetSampleCount(int Year, int Month)
        {
            var sampleNum = (from e in _dbContext.SampleNumbers
                             where e.Year == Year && e.Month == Month
                             select new SampleNameEntity()
                             {
                                 SampleCount = e.SampleCount,
                             }
                  ).FirstOrDefault();
            if (sampleNum != null)
            {
                return sampleNum.SampleCount;
            }
            else { return 0; }

        }
        public long AddSampleNo(long SampleCount, int Year, int Month)
        {
            try
            {
                var sample = new SampleNumber()
                {
                    SampleCount = SampleCount,
                    Year = Year,
                    Month = Month,
                    IsActive = true,
                    EnteredBy = 1,
                    EnteredDate = DateTime.Now

                };
                _dbContext.SampleNumbers.Add(sample);
                _dbContext.SaveChanges();
                return sample.SampleNumberId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string UpdateSampleNo(long SampleCount, int Year, int Month)
        {
            try
            {
                var sampleNum = (from e in _dbContext.SampleNumbers
                                 where e.Year == Year && e.Month == Month
                                 select new SampleNameEntity()
                                 {
                                     SampleNumberId = e.SampleNumberId,
                                 }
                ).FirstOrDefault();


                var sampleMaster = _dbContext.SampleNumbers.Find(sampleNum.SampleNumberId);
                sampleMaster.SampleCount = SampleCount;
                sampleMaster.Year = Year;
                sampleMaster.Month = Month;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public List<SampleLocationEntity> GetSampleLocationList(int EnquirySampleID)
        {
            try
            {
                return (from sl in _dbContext.SampleLocations
                        where sl.EnquirySampleID == EnquirySampleID
                        select new SampleLocationEntity()
                        {
                            SampleLocationId = sl.SampleLocationId,
                            Location = sl.Location,
                            EnquirySampleID = sl.EnquirySampleID,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       public EnquiryParameterEntity GetFormula(EnquiryParameterEntity objParam)
        {
            try
            {

                if (objParam.Infield == true)
                {
                    EnquiryParameterEntity obj = new EnquiryParameterEntity();
                    obj.ParameterFormulaID = 1;
                    return obj;
                }
                else { 
                  return (from ep in _dbContext.ParameterFormulas
                        join es in _dbContext.ParameterGroupMasters on ep.ParameterGroupId equals es.ParameterGroupId
                        join tm in _dbContext.TestMethods on ep.TestMethodID equals tm.TestMethodId
                        where ep.ParameterMasterId == objParam.ParameterMasterId & ep.ParameterGroupId == objParam.ParameterGroupId & ep.UnitID == objParam.UnitId
                        select new EnquiryParameterEntity()
                        {
                            //ParameterGroupId = ep.ParameterGroupId,
                            ParameterFormulaID = (Int32)ep.ParameterFormulaID,
                            
                            //UnitId = ep.UnitID,
                            //TestMethodId = ep.TestMethodID,
                            //ParameterMasterId = ep.ParameterMasterId,
                            //DisciplineId = ep.DisciplineId,
                        }).FirstOrDefault();
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        public EnquirySampleEntity GetSampleNumber(int EnquiryId, int EnquiryDetailId, int SampleTypeProductId)
        {
            try 
            {
                return (from ep in _dbContext.EnquirySampleDetails
                        join es in _dbContext.EnquiryDetails on ep.EnquiryDetailId equals es.EnquiryDetailId
                        
                        where es.EnquiryId == EnquiryId && es.SampleTypeProductId == SampleTypeProductId && es.EnquiryDetailId == EnquiryDetailId

                        select new EnquirySampleEntity()
                        {
                            SampleName = ep.SampleName,
                            DisplaySampleName = ep.DisplaySampleName,
                        }).FirstOrDefault();
            }
            
            catch (Exception ex)
            {
                return null;
            }
        }
        public EnquirySampleEntity GetWODSampleNumber(int WorkOrderId, int EnquiryDetailId, int SampleTypeProductId)
        {
            try
            {
                return (from ep in _dbContext.EnquirySampleDetails
                        join es in _dbContext.EnquiryDetails
                          on ep.EnquiryDetailId equals es.EnquiryDetailId

                        where es.WorkOrderID == WorkOrderId && es.SampleTypeProductId == SampleTypeProductId && es.EnquiryDetailId == EnquiryDetailId

                        select new EnquirySampleEntity()
                        {
                            SampleName = ep.SampleName,
                            DisplaySampleName = ep.DisplaySampleName,
                        }).FirstOrDefault();
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        public List<EnquiryParameterEntity> GetSampleParameterList(int EnquiryDetailId, int EnquirySampleId, int SampleTypeProductId)
        {
            var enquiryDetail = _dbContext.EnquiryDetails.Where(e => e.EnquiryDetailId == EnquiryDetailId).FirstOrDefault();
            try
            {
                return (from pm in _dbContext.ParameterMappings
                        join pg in _dbContext.ParameterGroupMasters on pm.ParameterGroupId equals pg.ParameterGroupId
                        into pmpg
                        from a in pmpg.DefaultIfEmpty()
                        join p in _dbContext.ParameterMasters on pm.ParameterMasterId equals p.ParameterMasterId
                        into b
                        from c in b.DefaultIfEmpty()
                        join d in _dbContext.DisciplineMasters on pm.DisciplineId equals d.DisciplineId
                        into d
                        from e in d.DefaultIfEmpty()
                        join ep in _dbContext.EnquiryParameterDetails on pm.ParameterMappingId equals ep.ParameterMappingId//for saved pg,sg,matrix at enquiry
                        into f
                        from g in f.Where(x => x.EnquirySampleID == EnquirySampleId).DefaultIfEmpty()
                        join es in _dbContext.EnquirySampleDetails on g.EnquirySampleID equals es.EnquirySampleID
                        into h
                        from i in h.DefaultIfEmpty()
                        join ed in _dbContext.EnquiryDetails on i.EnquiryDetailId equals ed.EnquiryDetailId
                        into parameters
                        from par in parameters.DefaultIfEmpty()
                        where (par.EnquiryDetailId == EnquiryDetailId || (a.SampleTypeProductId == enquiryDetail.SampleTypeProductId/* && a.SubGroupId == enquiryDetail.SubGroupId && a.MatrixId == enquiryDetail.MatrixId*/))
                        && pm.IsActive == true && a.IsActive == true
                        select new EnquiryParameterEntity()
                        {
                            ParameterMappingId = pm.ParameterMappingId,
                            EnquiryParameterDetailID = g.EnquiryParameterDetailID,
                            ParameterGroupId = pm.ParameterGroupId,
                            EnquirySampleID = i.EnquirySampleID,
                            ParameterMasterId = pm.ParameterMasterId,
                            ParameterName = c.ParameterName,
                            Discipline = e.Discipline,
                            ProductGroupId = a.ProductGroupId,
                            Infield = (bool)pm.InField,
                            MatrixId = (Int32)a.MatrixId,
                            SubGroupId = a.SubGroupId,
                            TestMethodId = g.TestMethodId == null || g.TestMethodId == 0 ? pm.TestMethodId : g.TestMethodId,
                            UnitId = g.UnitId == null || g.UnitId == 0 ? pm.UnitId : g.UnitId,
                            Charges = g.Charges == null ? c.Charges == null ? 0 : c.Charges : g.Charges,
                            LabMasterId = g.LabMasterId,
                            isDefaultUnit = pm.IsDefaultUnit == null?false:(bool)pm.IsDefaultUnit,
                            Remarks=g.Remarks,
                        }).GroupBy(n => new {n.ParameterGroupId, n.ParameterMasterId,n.isDefaultUnit}).Select(g => g.FirstOrDefault()).OrderBy(p => p.ParameterName).ToList();
               
            }
            catch(Exception ex)
            {
              return  null;
            }
        }

        public List<EnquiryParameterEntity> GetSampleParameterList(long EnquirySampleId)
        {
            try
            {
                var sampleDetail = _dbContext.EnquirySampleDetails.Where(e => e.EnquirySampleID == EnquirySampleId).FirstOrDefault();              

                return (from ep in _dbContext.EnquiryParameterDetails
                        join es in _dbContext.EnquirySampleDetails 
                          on ep.EnquirySampleID equals es.EnquirySampleID
                        join pm in _dbContext.ParameterMappings 
                          on ep.ParameterMappingId equals pm.ParameterMappingId
                        join pg in _dbContext.ParameterGroupMasters 
                           on pm.ParameterGroupId equals pg.ParameterGroupId
                        join p in _dbContext.ParameterMasters 
                           on pm.ParameterMasterId equals p.ParameterMasterId
                        where es.EnquirySampleID == EnquirySampleId && pm.IsActive == true 
                             && ep.IsActive == true
                        select new EnquiryParameterEntity()
                        {
                            ParameterMappingId = pm.ParameterMappingId,
                            EnquiryParameterDetailID = ep.EnquiryParameterDetailID,
                            ParameterGroupId = pm.ParameterGroupId,
                            EnquirySampleID = ep.EnquirySampleID,
                            ParameterMasterId = pm.ParameterMasterId,
                            ParameterName = p.ParameterName,
                            PCBLimit = ep.PCBLimit
                        }).OrderBy(p => p.ParameterName).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
         }
        public long GetEnquirySampleID()
        {
            try
            {
                long? EnquirySampleID = _dbContext.EnquirySampleDetails.DefaultIfEmpty().Max(e => e == null ? 1 : e.EnquirySampleID);
              
                return (long)EnquirySampleID;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public EnquirySampleEntity AddEnquirySampleDetail(EnquirySampleEntity enquirySampleEntity)
        {
            //long? EnquirySampleID = _dbContext.EnquirySampleDetails.DefaultIfEmpty().Max(e => e == null ? 1 : e.EnquirySampleID );

           // enquirySampleEntity.SampleName = enquirySampleEntity.SampleName + (EnquirySampleID + 1).ToString();

            EnquirySampleDetail sampleDetail = new EnquirySampleDetail();
            sampleDetail.SampleName = enquirySampleEntity.SampleName;
            sampleDetail.DisplaySampleName = enquirySampleEntity.DisplaySampleName;
            sampleDetail.EnquiryDetailId = enquirySampleEntity.EnquiryDetailId;
            sampleDetail.IsActive = true;
            sampleDetail.EnteredBy = enquirySampleEntity.EnteredBy;
            sampleDetail.EnteredDate = DateTime.UtcNow;
            _dbContext.EnquirySampleDetails.Add(sampleDetail);
            _dbContext.SaveChanges();
            enquirySampleEntity.EnquirySampleID = sampleDetail.EnquirySampleID;
            return enquirySampleEntity;
        }
        
        public void UpdateEnquirySampleCharges(long EnquirySampleId,decimal TotalCharges)
        {
            try { 
                var sampleDetail = _dbContext.EnquirySampleDetails.Find(EnquirySampleId);
                sampleDetail.TotalCharges = TotalCharges;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
       

     
        public void UpdateSampleTypeProductId(long EnquiryMasterSampleTypeId, string DisplaySampleName)
        {
            try
            {
                var sampleDetail = _dbContext.EnquiryMasterSampleTypes.Find(EnquiryMasterSampleTypeId);
                sampleDetail.SampleNo = DisplaySampleName;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public void UpdateEnquiryParameterPCB(List<EnquiryParameterEntity> enquiryParameterList)
        {
            try
            {                
                foreach (EnquiryParameterEntity param in enquiryParameterList)
                {
                    var paramDetail = _dbContext.EnquiryParameterDetails.Find(param.EnquiryParameterDetailID);
                    paramDetail.PCBLimit = param.PCBLimit;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void DeleteSample(long EnquirySampleId,bool isActive, string CurrentStatus)
        {
            try
            {
                var sampleDetail = _dbContext.EnquirySampleDetails.Find(EnquirySampleId);
               // var enquiryParaDetail= _dbContext.EnquiryParameterDetails.Find(EnquirySampleId);
               // var enquiryParaDetail = _dbContext.EnquiryParameterDetails.Where(e => e.EnquirySampleID == EnquirySampleId).ToList();
                if (sampleDetail!= null)
                {
                    if (CurrentStatus == "EnquiryRaised" || CurrentStatus == "ContractSubmitted" || CurrentStatus == "QuotationCreated")
                    {
                        // if Status is Before Quotation Sent then hard delete//Doubt
                        //_dbContext.EnquirySampleDetails.Remove(sampleDetail);
                        sampleDetail.IsActive = isActive;
                        //enquiryParaDetail.IsActive= isActive;
                    }
                    else
                    {
                        //else Soft delete //Doubt
                        //sampleDetail.IsActive = isActive;
                        //_dbContext.EnquirySampleDetails.Remove(sampleDetail);
                        //_dbContext.EnquiryParameterDetails.Remove(enquiryParaDetail);
                        sampleDetail.IsActive = isActive;
                        //enquiryParaDetail.IsActive = isActive;
                    }
                    _dbContext.SaveChanges();
                }
                            
            }
            catch (Exception ex)
            {
               // ex.InnerException.Message;
            }

        }
        public EnquirySampleEntity GetEnquirySampleDetail(int EnquirySampleID)
        {
            return _dbContext.EnquirySampleDetails.Where(s => s.EnquirySampleID == EnquirySampleID).Select(s => new EnquirySampleEntity()
            {
                EnquirySampleID = s.EnquirySampleID,
                EnquiryDetailId = s.EnquiryDetailId,
                SampleName = s.SampleName,
                DisplaySampleName=s.DisplaySampleName,
                TotalCharges =s.TotalCharges == null ? 0 : s.TotalCharges
            }).FirstOrDefault();
        }

        public List<EnquirySampleEntity> GetEnquirySampleList(int EnquiryID)
        {
            return (from es in _dbContext.EnquirySampleDetails
                    join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId
                    join stp in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stp.SampleTypeProductId
                    //join p in _dbContext.ProductGroupMasters on ed.ProductGroupId equals p.ProductGroupId
                    //join s in _dbContext.SubGroupMasters on ed.SubGroupId equals s.SubGroupId
                    //join m in _dbContext.MatrixMasters on ed.MatrixId equals m.MatrixId
                    join e in _dbContext.EnquiryMasters on ed.EnquiryId equals e.EnquiryId//Added by Nivedita
                    join status in _dbContext.StatusMasters on e.StatusId equals status.StatusId//Added by Nivedita
                    into parameters
                    from par in parameters.DefaultIfEmpty()
                    where ed.EnquiryId == EnquiryID && es.IsActive == true && ed.IsActive == true
                    select new EnquirySampleEntity()
                    {
                        EnquirySampleID = es.EnquirySampleID,
                        EnquiryDetailId = es.EnquiryDetailId,
                        SampleName = es.SampleName,
                        DisplaySampleName=es.DisplaySampleName,
                        ProcedureId = ed.ProductGroupId,
                        SampleTypeProductName = stp.SampleTypeProductName,
                        //ProductGroupName = p.ProductGroupName,
                        //SubGroupName = s.SubGroupName,
                        ////  MatrixName = par.MatrixName,
                        //MatrixName = ed.MatrixMaster.MatrixName,//Added by Nivedita
                        CurrentStatus = e.StatusMaster.StatusName,//Added by Nivedita
                        //Cost = es.TotalCharges == null ?0:(decimal)es.TotalCharges
                        Cost = _dbContext.EnquiryParameterDetails.Where(epd => epd.EnquirySampleID == es.EnquirySampleID).Select(c => c.Charges==null?0:c.Charges).Sum()==null?0:(decimal)_dbContext.EnquiryParameterDetails.Where(epd => epd.EnquirySampleID == es.EnquirySampleID).Select(c => c.Charges == null ? 0 : c.Charges).Sum(),
                      
                    }).ToList();
        }

       public bool AddEnquiryParameterDetail(List<EnquiryParameterEntity> parameters)
        {
            try
            {
                foreach(var par in parameters)
                {
                    if (par.EnquiryParameterDetailID == null || par.EnquiryParameterDetailID == 0)
                    {
                        _dbContext.EnquiryParameterDetails.Add(new EnquiryParameterDetail()
                        {
                            EnquirySampleID = (Int32)par.EnquirySampleID,
                            ParameterMappingId = (Int32)par.ParameterMappingId,
                            UnitId = (int)par.UnitId,
                            Charges = par.Charges,
                            TestMethodId = par.TestMethodId,
                            LabMasterId = par.LabMasterId,
                            Remarks = par.Remarks,
                            IsActive = true, //par.IsActive,
                            EnteredBy = par.EnteredBy,
                            EnteredDate = DateTime.UtcNow
                        }); ;
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        var paramDetail = _dbContext.EnquiryParameterDetails.Find(par.EnquiryParameterDetailID);
                        paramDetail.UnitId = (int)par.UnitId;
                        paramDetail.Charges = par.Charges;
                        paramDetail.TestMethodId = par.TestMethodId;
                        paramDetail.LabMasterId = par.LabMasterId;
                        paramDetail.Remarks = par.Remarks;
                        paramDetail.IsActive = par.IsActive;
                        paramDetail.ModifiedBy = par.EnteredBy;
                        _dbContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GetSampleParameters(int EnquirySampleID)
        {
            try
            {
                var par = (from e in _dbContext.EnquiryParameterDetails
                           join pm in _dbContext.ParameterMappings on e.ParameterMappingId equals pm.ParameterMappingId
                           into epm
                           from a in epm.DefaultIfEmpty()
                           join p in _dbContext.ParameterMasters on a.ParameterMasterId equals p.ParameterMasterId
                           into ep
                           from pr in ep.DefaultIfEmpty()
                           where e.EnquirySampleID == EnquirySampleID
                           select new
                           {
                               e.EnquiryParameterDetailID,
                               pr.ParameterName,
                               pr.ParameterMasterId,
                               e.ParameterMappingId,
                               e.PCBLimit
                           }).ToList();
                return string.Join(",", par.Select(p => p.ParameterName));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
        public string GetPGSGM(int EnquiryDetailId)
        {
            try
            {
                var par = (from e in _dbContext.EnquiryParameterDetails
                           join pm in _dbContext.ParameterMappings on e.ParameterMappingId equals pm.ParameterMappingId
                           into epm
                           from a in epm.DefaultIfEmpty()
                           join p in _dbContext.ParameterMasters on a.ParameterMasterId equals p.ParameterMasterId
                           into ep
                           from pr in ep.DefaultIfEmpty()
                           where e.EnquirySampleID == EnquiryDetailId
                           select new
                           {
                               pr.ParameterName,
                               pr.ParameterMasterId,
                               e.ParameterMappingId,
                               e.PCBLimit
                           }).ToList();
                return string.Join(",", par.Select(p => p.ParameterName));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string GetParameterRemarks(int EnquirySampleID)
        {
            try
            {
                var par = (from e in _dbContext.EnquiryParameterDetails
                           where e.EnquirySampleID == EnquirySampleID
                           select new
                           {
                               e.Remarks,
                              
                           }).ToList();
                return string.Join(",", par.Select(p => p.Remarks));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public string GetSamplePCBLimit(int EnquirySampleID)
        {
            try
            {
                
                var par = (from e in _dbContext.EnquiryParameterDetails
                           join pm in _dbContext.ParameterMappings on e.ParameterMappingId equals pm.ParameterMappingId
                           into epm
                           from a in epm.DefaultIfEmpty()
                           join p in _dbContext.ParameterMasters on a.ParameterMasterId equals p.ParameterMasterId
                           into ep
                           from pr in ep.DefaultIfEmpty()
                           where e.EnquirySampleID == EnquirySampleID
                           select new
                           {
                               e.PCBLimit
                           }).ToList();
                
                return string.Join("/", par.Select(p => p.PCBLimit));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public string GetFDParameters(int ParameterMappingId)
        {
            try
            {
                var par = (from e in _dbContext.ParameterMasters
                           join pm in _dbContext.ParameterMappings on e.ParameterMasterId equals pm.ParameterMasterId
                             into ep
                           from pr in ep.DefaultIfEmpty()
                           where pr.ParameterMappingId == ParameterMappingId
                           select new 
                           {
                               e.ParameterName,
                              

                           }).ToList();
                return string.Join(",", par.Select(p=> p.ParameterName) );
               
                //return 
            }
            catch (Exception ex)
            {
                return ex.Message;
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
