using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.DropDowns;
using LIMS_DEMO.Core;
using LIMS_DEMO.DAL;

namespace LIMS_DEMO.Core.Repository
{
    public interface IEmployeeMaster
    {
      string AddEmployee(EmployeeMasterEntity employeeMasterEntity);
      EmployeeMasterEntity GetDetails(int UserDetailId);
      List<EmployeeMasterEntity> GetEmployeeMasterList();
      string Update(EmployeeMasterEntity userEntity);
    }
}
