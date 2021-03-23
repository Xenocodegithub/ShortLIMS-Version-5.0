using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Configuration
{
    public class DisciplineMasterEntity
    {
        public string All { get; set; }
        public byte DisciplineId { get; set; }
        public string Discipline { get; set; }
        public string PrintDesc { get; set; }
        public string IconPath { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    }
}