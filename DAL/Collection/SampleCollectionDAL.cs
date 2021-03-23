using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Collection;
using LIMS_DEMO.Core.Repository;
namespace LIMS_DEMO.DAL.Collection
{
    public class SampleCollectionDAL : ISampleCollection
    {
        readonly LIMSEntities _dbContext;

        public SampleCollectionDAL()
        {
            _dbContext = new LIMSEntities();
        }
       
        public long AddNotification(string Msg, string RoleName, SampleCollectionEntity samplecollectionEntity)
        {
            try
            {
                var notific = new NotificationDetail()
                {
                    RoleId = null,
                    NotificationName = samplecollectionEntity.SampleNo,
                    Message = Msg,
                    DateTime = (DateTime)samplecollectionEntity.CollectionDate,
                    IsActive = true,
                    EnteredBy = samplecollectionEntity.EnteredBy,
                    EnteredDate = DateTime.Now,
                    RoleName = RoleName,
                };
                _dbContext.NotificationDetails.Add(notific);
                _dbContext.SaveChanges();
                return notific.NotificationDetailId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string GetExpectedCollDate(int WorkOrderSampleCollectionDateId)
        {
            var date = (from scoll in _dbContext.SampleCollections
                    join woscd in _dbContext.WorkOrderSampleCollectionDates on scoll.WorkOrderSampleCollectionDateId equals woscd.WorkOrderSampleCollectionDateId
                    where scoll.WorkOrderSampleCollectionDateId == WorkOrderSampleCollectionDateId
                    select new
                    {
                      CollectedOn = woscd.ExpectSampleCollDate.ToString()== null ?"-" : woscd.ExpectSampleCollDate.ToString(),
                    }).FirstOrDefault();

            return date.CollectedOn;
        }
        public long GetFDDetails(int SampleCollectionId)
        {
            SampleCollectionEntity samplecollectionEntity = new SampleCollectionEntity();
            try
            {
                var fdwater = _dbContext.FieldWasteWaters.Where(e => e.SampleCollectionId == SampleCollectionId).FirstOrDefault();
                var fdstack = _dbContext.FDStackEmissions.Where(e => e.SampleCollectionId == SampleCollectionId).FirstOrDefault();
                var fdair = _dbContext.FieldAmbientAirMonitorings.Where(e => e.SampleCollectionId == SampleCollectionId).FirstOrDefault();
                var fbuilding = _dbContext.FieldBuildingMaterials.Where(e => e.SampleCollectionId == SampleCollectionId).FirstOrDefault();
                var fcoalcoke = _dbContext.FieldCoalCokeSolidFuels.Where(e => e.SampleCollectionId == SampleCollectionId).FirstOrDefault();
                var fairmonitoring = _dbContext.FieldMicrobiologicalMonitoringOfAirs.Where(e => e.SampleCollectionId == SampleCollectionId).FirstOrDefault();
                var ffood = _dbContext.FieldFoodAndAgriCultures.Where(e => e.SampleCollectionId == SampleCollectionId).FirstOrDefault();
                var fnoisemonitoring = _dbContext.FieldNoiseLevelMonitorings.Where(e => e.SampleCollectionId == SampleCollectionId).FirstOrDefault();
                var fsolidHazardous = _dbContext.FieldSolidHazardousWasteSoilOils.Where(e => e.SampleCollectionId == SampleCollectionId).FirstOrDefault();
                var fworkplace = _dbContext.FieldWorkplaceEnvironmentAndFugitiveEmissions.Where(e => e.SampleCollectionId == SampleCollectionId).FirstOrDefault();

                if (fworkplace != null)
                {
                    samplecollectionEntity.FDId = fworkplace.WorkplaceID;
                    return samplecollectionEntity.FDId;
                }
                if (fsolidHazardous != null)
                {
                    samplecollectionEntity.FDId = fsolidHazardous.SolidHazardousWasteSoilOilId;
                    return samplecollectionEntity.FDId;
                }
                if (fnoisemonitoring != null)
                {
                    samplecollectionEntity.FDId = fnoisemonitoring.FieldNoiseId;
                    return samplecollectionEntity.FDId;
                }
                if (ffood != null)
                {
                    samplecollectionEntity.FDId = ffood.FieldFoodAndAgriCultureId;
                    return samplecollectionEntity.FDId;
                }
                if (fairmonitoring != null)
                {
                    samplecollectionEntity.FDId = fairmonitoring.MicrobiologicalID;
                    return samplecollectionEntity.FDId;
                }
                if (fcoalcoke != null)
                {
                    samplecollectionEntity.FDId = fcoalcoke.FieldId;
                    return samplecollectionEntity.FDId;
                }

                if (fbuilding != null)
                {
                    samplecollectionEntity.FDId = fbuilding.FieldBuildingMaterialId;
                    return samplecollectionEntity.FDId;
                }
                if (fdwater != null)
                {
                    samplecollectionEntity.FDId = fdwater.WasteWaterID;
                    return samplecollectionEntity.FDId;
                }
                if (fdair != null)
                {
                    samplecollectionEntity.FDId = fdair.FieldId;
                    return samplecollectionEntity.FDId;
                }
                if (fdstack != null)
                {
                    samplecollectionEntity.FDId = fdstack.FDStackEmissionId;
                    return samplecollectionEntity.FDId;
                }


                return samplecollectionEntity.FDId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string UpdateWorkOrderSampleCollectionDate(int WorkOrderSampleCollectionDateId, DateTime CollectionDate)
        {
            try
            {
                var sampleCal = _dbContext.WorkOrderSampleCollectionDates.Find(WorkOrderSampleCollectionDateId);
                sampleCal.ActualSampleCollDate = CollectionDate;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string AddSampleCollection(SampleCollectionEntity samplecollectionEntity)
        {
            try
            {
                _dbContext.SampleCollections.Add(new SampleCollection()
                {
                    SampleCollectionId = samplecollectionEntity.SampleCollectionId,
                    EnquirySampleID = samplecollectionEntity.EnquirySampleID,
                    //StatusId = samplecollectionEntity.StatusId,
                    SampleDescriptionId = samplecollectionEntity.SampleDescriptionId,//same Matrix 
                    SampleDescription = samplecollectionEntity.SampleDescription,
                    SampleTypeId = samplecollectionEntity.SampleTypeId,
                    //SampleDeviceId = samplecollectionEntity.SampleDeviceId,
                    //SampleQtyId = samplecollectionEntity.SampleQtyId,
                    ProcedureId = samplecollectionEntity.ProcedureId,
                    EnvCondtId = samplecollectionEntity.EnvCondtId,
                    CollectionDate = samplecollectionEntity.CollectionDate,
                    SampleCollectionTime = samplecollectionEntity.SampleCollectionTime,//doubt
                    Duration = samplecollectionEntity.Duration,
                    SampleLocation = samplecollectionEntity.SampleLocation,
                    IsWitness = samplecollectionEntity.IsWitness,
                    WitnessName = samplecollectionEntity.WitnessName,
                    IndustryType = samplecollectionEntity.IndustryType,
                    Source = samplecollectionEntity.Source,
                    Remark = samplecollectionEntity.Remark,//doubt
                    SampleNo = samplecollectionEntity.SampleNo,//to be Changed to SampleName wrt Iteration Number,
                    StartTime = samplecollectionEntity.StartTime,
                    EndTime = samplecollectionEntity.EndTime,
                });
                _dbContext.SaveChanges();

                _dbContext.ARCs.Add(new ARC()
                {
                    SampleCollectionId = samplecollectionEntity.SampleCollectionId,
                    // UserRoleId=1,
                    IsActive = true,
                    EnteredBy = samplecollectionEntity.EnteredBy,
                    EnteredDate = DateTime.Now,
                });
                _dbContext.SaveChanges();

                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public SampleCollectionEntity GetSampleCollectionDetails(int SampleCollectionId)
        {
            return (from loc in _dbContext.LocationSampleCollections
                    join scoll in _dbContext.SampleCollections on loc.LocationSampleCollectionID equals scoll.LocationSampleCollectionID
                    join sl in _dbContext.SampleLocations on loc.SampleLocationId equals sl.SampleLocationId
                    join es in _dbContext.EnquirySampleDetails on scoll.EnquirySampleID equals es.EnquirySampleID
                    join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId
                    join stp in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stp.SampleTypeProductId
                    //join em in _dbContext.EnquiryMasters on ed.EnquiryId equals em.EnquiryId

                    join em in _dbContext.EnquiryMasters on ed.EnquiryId equals em.EnquiryId into em
                    from emt in em.DefaultIfEmpty()

                    //join ctm in _dbContext.CustomerMasters on emt.CustomerMasterId equals ctm.CustomerMasterId

                    join ctm in _dbContext.CustomerMasters on emt.CustomerMasterId equals ctm.CustomerMasterId into ctm
                    from a in ctm.DefaultIfEmpty()


                    join sts in _dbContext.StatusMasters on scoll.StatusId equals sts.StatusId

                    join wo in _dbContext.WorkOrders on scoll.WorkOrderID equals wo.WorkOrderId
                    into collection
                    from coll in collection.DefaultIfEmpty()

                    join ctmwo in _dbContext.CustomerMasters on coll.CustomerMasterId equals ctmwo.CustomerMasterId into temp
                    from last in temp.DefaultIfEmpty()

                    where scoll.SampleCollectionId == SampleCollectionId
                    select new SampleCollectionEntity()
                    {
                        WorkOrderSampleCollectionDateId = scoll.WorkOrderSampleCollectionDateId,
                        LocationSampleCollectionID = (Int32)scoll.LocationSampleCollectionID,
                        SampleLocationId= loc.SampleLocationId,
                        Location = sl.Location,
                        SampleLocation = scoll.SampleLocation,
                        SampleCollectionId = scoll.SampleCollectionId,
                        EnquirySampleID = scoll.EnquirySampleDetail.EnquirySampleID,
                        EnquiryDetailId = es.EnquiryDetailId,
                        //EnquiryId = (long)ed.EnquiryId.Value,
                        SampleNo = scoll.SampleNo,
                        EnquiryId = emt.EnquiryId == null ? 0 : emt.EnquiryId,
                        EmployeeId = scoll.EmployeeId,//doubt
                        // SampleCollectedBy = es.SampleCollectedBy,
                        SampleCollectedBy = loc.SampleCollectedBy,
                        Remark = scoll.Remark,//doubt
                        SampleCollectionTime = scoll.SampleCollectionTime,//doubt
                        Duration = scoll.Duration,
                        StartTime=scoll.StartTime,
                        EndTime=scoll.EndTime,
                        SampleDescriptionId = scoll.SampleDescriptionId,//same as Matrix group
                        SampleDescription = scoll.SampleDescription,
                        SampleTypeId = scoll.SampleTypeId,
                        SampleDeviceId = scoll.SampleDeviceId,
                        SampleQtyId = scoll.SampleQtyId,
                        ProcedureId = scoll.ProcedureId,
                        EnvCondtId = scoll.EnvCondtId,
                        StatusId = scoll.StatusMaster.StatusId,
                        CurrentStatus = sts.StatusName,
                        StatusCodeLab = sts.StatusCode,//FoLabStatus
                        StatusCodeField = scoll.FieldStatusCode,//ForFieldStatus
                        WorkOrderID = scoll.WorkOrder.WorkOrderId,
                        WorkOrderNo = scoll.WorkOrder.WorkOrderNo, //WorkOrderNo
                        // ExpectSampleCollDate = (DateTime)scoll.WorkOrder.ExpectSampleCollDate,//Sample to be Collected On
                        CollectionDate = scoll.CollectionDate,
                      //CollectedOn = scoll.WorkOrder.ExpectSampleCollDate.ToString() == null ? "-" : scoll.WorkOrder.ExpectSampleCollDate.ToString(),
                        //ContactNO = ctm.ContactMobileNo,//Doubt
                        //CustomerName = ctm.RegistrationName,//Doubt

                        ContactNO = a.ContactMobileNo == "" || a.ContactMobileNo == null ? last.ContactMobileNo : a.ContactMobileNo,
                        //CustomerName = ctm.RegistrationName,
                        CustomerName = a.RegistrationName == "" || a.RegistrationName == null ? last.RegistrationName : a.RegistrationName,

                        // Address = ctm.Address1,//Doubt
                        Address = a.Address1 == "" || a.Address1 == null ? last.Address1 : a.Address1,
                        SampleTypeProductId= (Int32)ed.SampleTypeProductId,
                        SampleTypeProductName = stp.SampleTypeProductName,
                        //ProductGroupId = (Int32)ed.ProductGroupId,
                        //ProductGroupName = ed.ProductGroupMaster.ProductGroupName,
                        //SubGroupId = (Int32)ed.SubGroupId,
                        //SubGroupName = ed.SubGroupMaster.SubGroupName,
                        //MatrixId = ed.MatrixId,
                        //MatrixName = ed.MatrixMaster.MatrixName,
                        Source = scoll.Source,
                        IsWitness = scoll.IsWitness,
                        WitnessName = scoll.WitnessName,
                        IndustryType = scoll.IndustryType,
                        Iteration = scoll.Iteration,
                      
                    }
                   ).FirstOrDefault();

        }
        public string Update(SampleCollectionEntity samplecollectionEntity)
        {
            try
            {
                var sampleCollection = _dbContext.SampleCollections.Find(samplecollectionEntity.SampleCollectionId);
                sampleCollection.SampleCollectionId = samplecollectionEntity.SampleCollectionId;
                sampleCollection.EnquirySampleID = samplecollectionEntity.EnquirySampleID;
               // sampleCollection.SampleDescriptionId = samplecollectionEntity.SampleDescriptionId;
                sampleCollection.SampleDescription = samplecollectionEntity.SampleDescription;
                //sampleCollection.StatusId = samplecollectionEntity.StatusId;
                //sampleCollection.SampleDeviceId = samplecollectionEntity.SampleDeviceId;
                //sampleCollection.SampleQtyId = samplecollectionEntity.SampleQtyId;
                //sampleCollection.ProcedureId = samplecollectionEntity.ProcedureId;
                sampleCollection.EnvCondtId = samplecollectionEntity.EnvCondtId;
                sampleCollection.SampleTypeId = samplecollectionEntity.SampleTypeId;
                sampleCollection.CollectionDate = samplecollectionEntity.CollectionDate;
                sampleCollection.SampleCollectionTime = samplecollectionEntity.SampleCollectionTime;
                sampleCollection.Duration = samplecollectionEntity.Duration;
                sampleCollection.SampleLocation = samplecollectionEntity.SampleLocation;
                sampleCollection.IsWitness = samplecollectionEntity.IsWitness;
                sampleCollection.WitnessName = samplecollectionEntity.WitnessName;
                sampleCollection.IndustryType = samplecollectionEntity.IndustryType;
                sampleCollection.Source = samplecollectionEntity.Source;
                sampleCollection.Remark = samplecollectionEntity.Remark;
                sampleCollection.StartTime = samplecollectionEntity.StartTime;
                sampleCollection.EndTime = samplecollectionEntity.EndTime;
                //sampleCollection.SampleNo = samplecollectionEntity.SampleNo; //to be Changed to SampleName,
                _dbContext.SaveChanges();

                if (samplecollectionEntity.mode== "SampleSubmit")
                {
                    _dbContext.ARCs.Add(new ARC()
                    {
                        SampleCollectionId = samplecollectionEntity.SampleCollectionId,
                        //UserRoleId = 1,
                        IsActive = true,
                        EnteredBy = samplecollectionEntity.EnteredBy,
                        EnteredDate = DateTime.Now,
                    });
                    _dbContext.SaveChanges();
                }
               
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string AddQuantityPreservative(List<QuantityPreservativeEntity> quantity, long EnquirySampleID,int WorkOrderID)
        {

            try
            {
                List<long> SampleCollectionDateIds = _dbContext.SampleCollections.Where(e => e.WorkOrderID == WorkOrderID && e.EnquirySampleID == EnquirySampleID).Select(e => e.SampleCollectionId).ToList();
                List<int> qtpDetails = _dbContext.QuantityPreservatives.Where(e => SampleCollectionDateIds.Contains(e.SampleCollectionId)).Select(e => e.QtyPreservativeId).ToList();

                if (qtpDetails.Count() == 0)
                {
                    for (int i = 0; i < SampleCollectionDateIds.Count; i++)
                    {
                        foreach (var item in quantity)
                        {
                            _dbContext.QuantityPreservatives.Add(new QuantityPreservative()
                             {
                                    //QtyPreservativeId = item.QtyPreservativeId,
                                    SampleCollectionId = SampleCollectionDateIds[i],
                                    ISGivenPreservative = item.ISGivenPreservative,
                                    SampleQtyId = (Int32)item.SampleQtyId,
                                    No = item.No,
                                    IsActive = item.IsActive,
                                    EnteredBy = item.EnteredBy,
                                    EnteredDate = item.EnteredDate,
                            }); 
                            _dbContext.SaveChanges();
                        }
                    }
                }
               
                //foreach (var item in quantity)
                //{
                //    if (item.QtyPreservativeId == 0)
                //    {
                //        _dbContext.QuantityPreservatives.Add(new QuantityPreservative()
                //        {
                //            //QtyPreservativeId = item.QtyPreservativeId,
                //            SampleCollectionId = item.SampleCollectionId,
                //            ISGivenPreservative = item.ISGivenPreservative,
                //            SampleQtyId = (Int32)item.SampleQtyId,
                //            No=item.No,
                //            IsActive = item.IsActive,
                //            EnteredBy = item.EnteredBy,
                //            EnteredDate = item.EnteredDate,
                //        }); ;
                //        _dbContext.SaveChanges();
                //    }
                //    else
                //    {
                //        var quantityPreserv = _dbContext.QuantityPreservatives.Find(item.QtyPreservativeId);
                //        quantityPreserv.QtyPreservativeId = item.QtyPreservativeId;
                //        quantityPreserv.SampleCollectionId = item.SampleCollectionId;
                //        quantityPreserv.ISGivenPreservative = item.ISGivenPreservative;
                //        quantityPreserv.No = item.No;
                //        quantityPreserv.SampleQtyId = (Int32)item.SampleQtyId;
                //        _dbContext.SaveChanges();
                //    }
                //}

                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string AddSampleDevice(List<SampleDeviceEntity> device)
        {

            try
            {
                foreach (var item in device)
                {
                    if (item.SampleCollectionDevicesId == 0)
                    {
                        _dbContext.SampleCollectionDevices.Add(new SampleCollectionDevice()
                        {
                            //SampleCollectionDevicesId= item.SampleCollectionDevicesId,
                            SampleCollectionId = item.SampleCollectionId,
                            SampleDeviceId = item.SampleDeviceId,
                            IsActive = item.IsActive,
                            EnteredBy = item.EnteredBy,
                            EnteredDate = item.EnteredDate,
                        }); ;
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        var sampleDevice = _dbContext.SampleCollectionDevices.Find(item.SampleCollectionDevicesId);
                        sampleDevice.SampleCollectionDevicesId = item.SampleCollectionDevicesId;
                        sampleDevice.SampleCollectionId = item.SampleCollectionId;
                        sampleDevice.SampleDeviceId = item.SampleDeviceId;
                        _dbContext.SaveChanges();
                    }
                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string AddSampleProcedure(List<ProcedureEntity> procedure)
        {
            try
            {
                foreach (var item in procedure)
                {
                    if (item.SampleCollectionProcedureId == 0)
                    {
                        _dbContext.SampleCollectionProcedures.Add(new SampleCollectionProcedure()
                        {
                            SampleCollectionId = item.SampleCollectionId,
                            ProcedureId = item.ProcedureId,
                            IsActive = item.IsActive,
                            EnteredBy = item.EnteredBy,
                            EnteredDate = item.EnteredDate,
                        }); ;
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        var sampleProcedure = _dbContext.SampleCollectionProcedures.Find(item.SampleCollectionProcedureId);
                        sampleProcedure.SampleCollectionProcedureId = item.SampleCollectionProcedureId;
                        sampleProcedure.SampleCollectionId = item.SampleCollectionId;
                        sampleProcedure.ProcedureId = item.ProcedureId;
                        _dbContext.SaveChanges();
                    }
                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public List<SampleCollectionEntity> GetCollectionList(int UserMasterID, int CollectedBy)
        {
            string _userName = Convert.ToString(UserMasterID);
            try
            {
                return (
                        from loc in _dbContext.LocationSampleCollections
                        join sc in _dbContext.SampleLocations on loc.SampleLocationId equals sc.SampleLocationId
                        join scoll in _dbContext.SampleCollections on loc.LocationSampleCollectionID equals scoll.LocationSampleCollectionID
                        join es in _dbContext.EnquirySampleDetails on scoll.EnquirySampleID equals es.EnquirySampleID
                 
                        join sts in _dbContext.StatusMasters on scoll.StatusId equals sts.StatusId
                        join wo in _dbContext.WorkOrders on scoll.WorkOrderID equals wo.WorkOrderId

                        join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId
                        join stp in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stp.SampleTypeProductId

                        join em in _dbContext.EnquiryMasters on ed.EnquiryId equals em.EnquiryId into em
                        from emt in em.DefaultIfEmpty()

                        join ctm in _dbContext.CustomerMasters on emt.CustomerMasterId equals ctm.CustomerMasterId into ctm
                        from a in ctm.DefaultIfEmpty()

                        join ctmwo in _dbContext.CustomerMasters on wo.CustomerMasterId equals ctmwo.CustomerMasterId into temp
                        from last in temp.DefaultIfEmpty()

                        where scoll.IsActive == true && es.IsActive == true && scoll.EmployeeId == _userName && loc.SampleCollectedBy == CollectedBy

                        select new SampleCollectionEntity()
                        {
                            LocationSampleCollectionID = (Int32)scoll.LocationSampleCollectionID,
                            SampleCollectionId = scoll.SampleCollectionId,
                            EnquirySampleID = scoll.EnquirySampleDetail.EnquirySampleID,
                            EnquiryDetailId = es.EnquiryDetailId,
                            SampleName = es.SampleName,//to be Changed to SampleName wrt Iteration Number,
                            DisplaySampleName = loc.LocationSampleName,
                            Location = sc.Location,
                            ULRNo = scoll.ULRNo,//doubt for backend storage only
                            FieldDeterminationId = scoll.FieldDeterminationId,//Doubt
                            Iteration = scoll.Iteration,
                            CollectionDate = scoll.CollectionDate,
                            //EnquiryId = emt.EnquiryId,
                            EnquiryId = emt.EnquiryId == null ? 0 : emt.EnquiryId,
                            // ContactNO = ctm.ContactMobileNo,
                            ContactNO = a.ContactMobileNo == "" || a.ContactMobileNo == null ? last.ContactMobileNo : a.ContactMobileNo,
                            //  CustomerName = ctm.RegistrationName,
                            CustomerName = a.RegistrationName == "" || a.RegistrationName == null ? last.RegistrationName : a.RegistrationName,
                            StatusId = scoll.StatusMaster.StatusId,
                            CurrentStatus = sts.StatusName,
                            StatusCodeLab = sts.StatusCode,
                            StatusCodeField = scoll.FieldStatusCode,//ForFieldStatus
                            WorkOrderID = scoll.WorkOrder.WorkOrderId,
                            //ExpectSampleCollDate = (DateTime)wo.ExpectSampleCollDate,//Sample to be Collected On
                            CollectedOn = wo.ExpectSampleCollDate.ToString() == null ? "-" : wo.ExpectSampleCollDate.ToString(),
                            // ProductGroupId = (Int32)ed.ProductGroupId,
                            //ProductGroupName = ed.ProductGroupMaster.ProductGroupName,
                            //MatrixId = ed.MatrixId,
                            SampleTypeProductId = (Int32)ed.SampleTypeProductId,
                            SampleTypeProductName = stp.SampleTypeProductName,
                            //MatrixName = ed.MatrixMaster.MatrixName,
                        }).OrderByDescending(wo => wo.SampleCollectionId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<QuantityPreservativeEntity> GetSampleQty(int SampleTypeProductId)
        {
            try
            {
                return (from sqm in _dbContext.SampleQtyMasters
                        where sqm.SampleTypeProductId == SampleTypeProductId
                        select new QuantityPreservativeEntity()
                        {
                            SampleQtyId = sqm.SampleQtyId,
                            SampleTypeProductId =(Int32)sqm.SampleTypeProductId,
                            SampleQty = sqm.SampleQty,
                            Preservation = sqm.Preservation,
                            No = 0,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ProcedureEntity> GetCollectionProcedureList(int SampleCollectionId)
        {
            try
            {
                return (from sqm in _dbContext.SampleCollectionProcedures
                        join sdm in _dbContext.ProcedureMasters on sqm.ProcedureId equals sdm.ProcedureId
                        where sqm.SampleCollectionId == SampleCollectionId
                        select new ProcedureEntity()
                        {
                            SampleCollectionProcedureId = sqm.SampleCollectionProcedureId,
                            Procedure = sdm.ProcedureName,
                            ProcedureId = sqm.ProcedureId,
                           
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public List<SampleDeviceEntity> GetSampleDevice(int SampleTypeProductId)
        //{
        //    try
        //    {
        //        return (from sqm in _dbContext.SampleCollectionDevices
        //                where sqm.SampleTypeProductId == SampleTypeProductId
        //                select new SampleDeviceEntity()
        //                {
        //                    SampleQtyId = sqm.SampleQtyId,
        //                    SampleTypeProductId = (Int32)sqm.SampleTypeProductId,
        //                    SampleQty = sqm.SampleQty,
        //                    Preservation = sqm.Preservation,

        //                }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public string UpdateCollectionStatus(long SampleCollectionId, int StatusId)
        {
            try
            {
                var sampleColl = _dbContext.SampleCollections.Find(SampleCollectionId);
                sampleColl.StatusId = (byte)StatusId;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string UpdateExpectSampleCollDate(int WorkOrderID, DateTime CollectionDate)
        {
            try
            {
                var workOrder = _dbContext.WorkOrders.Find(WorkOrderID);
                workOrder.ExpectSampleCollDate = CollectionDate;
                _dbContext.SaveChanges();
                return workOrder.ExpectSampleCollDate.ToString();
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public int GetFieldIdByMatrixName(string MatrixName, int SampleCollectionId)
        {

            //try
            //{
            //    //if (MatrixName == "Water" || MatrixName == "Wastewater ")
            //    //{
            //    //    return _dbContext.FieldWasteWaters.Where(w => w.SampleCollectionId == SampleCollectionId).Select(w => new { w.WasteWaterID }).FirstOrDefault().WasteWaterID;
            //    //}
            //    //else if(MatrixName == "Coal")
            //    //{
            //    //    return _dbContext.FieldCoalCokeSolidFuels.Where(w => w.SampleCollectionId == SampleCollectionId).Select(w => new { w.FieldId }).FirstOrDefault().FieldId;

            //    //}
            //    return _dbContext.FieldWasteWaters.Where(w => w.SampleCollectionId == SampleCollectionId).Select(w => new { w.WasteWaterID }).FirstOrDefault().WasteWaterID;
            //}
            //catch
            //{
            //    return 0;
            //}
            return 0;

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
