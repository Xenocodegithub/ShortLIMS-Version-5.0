using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using LIMS_DEMO.Core.CompanyRegistration;

namespace LIMS_DEMO.Core.Repository
{
    public interface IRegistration: IDisposable
    {
        string AddRegistration(RegistrationEntity registrationEntity);
        string UpdateRegistration(RegistrationEntity registrationEntity);
        RegistrationEntity GetRegistrationDetails(int CompanyId);
        List<RegistrationEntity> GetRegistrationList();
        bool SaveCompanyLogo(List<RegistrationEntity> registrationEntities);
        string Delete(int CompanyId);
    }
}
