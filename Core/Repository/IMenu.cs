using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.AppSettings;
namespace LIMS_DEMO.Core.Repository
{
    public interface IMenu
    {
        IList<MenuEntity> Get(int iUserId);
        IList<MenuEntity> GetMenu(int iUserId);
    }
}