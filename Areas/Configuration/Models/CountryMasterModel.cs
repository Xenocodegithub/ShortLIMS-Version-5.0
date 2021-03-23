using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class CountryMasterModel
    {
        [Key]
        public int CountryId { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
        public bool All { get; set; }
    }
}