﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Invoice
{
    public class InvoiceEntity
    {

        public int WorkOrderSampleCollectionDateId { get; set; }
        public Nullable<int> LabMasterId { get; set; }
        public string FileName { get; set; }
        public string WOUpload { get; set; }
        public System.DateTime WORecieveDate { get; set; }
        public string RegistrationName { get; set; }
        public int? CompanyTypeId { get; set; }
        public string EnquiryNo { get; set; }
        public string WorkOrderNo { get; set; }
        public long EnquiryId { get; set; }
        public int WorkOrderId { get; set; }
        public string ApprovedAmount { get; set; }
        public string CurrentStatus { get; set; }
        public string Remarks { get; set; }
        public string Date { get; set; }
        public int? AssignToId { get; set; }
        public Nullable<bool> IsIGST { get; set; }
    }
}