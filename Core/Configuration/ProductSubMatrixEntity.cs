using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Configuration
{
    public class ProductSubMatrixEntity
    {
        public int ProductGroupId { get; set; }


        public string ProductGroupName { get; set; }


        public Nullable<int> SampleTypeProductId { get; set; }


        public string SampleTypeProductName { get; set; }

        //public string Description { get; set; }

        public string Code { get; set; }

        public string SampleTypeProductCode { get; set; }
        //public bool IsDisposal { get; set; }


        //public string DisposedDay { get; set; }

        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        //SubGroup

        public int SubGroupId { get; set; }
        public string SubGroupName { get; set; }
        public string SubGroupCode { get; set; }

        //Matrix
        public string MatrixName { get; set; }
        public string MatrixCode { get; set; }
        public int MatrixId { get; set; }
    }
}