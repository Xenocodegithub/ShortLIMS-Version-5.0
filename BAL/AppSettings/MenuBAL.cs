using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.AppSettings;
using LIMS_DEMO.DAL.AppSettings;
namespace LIMS_DEMO.BAL.AppSettings
{
    public class MenuBAL
    {
        public MenuBAL()
        {
            CoreFactory.menu = new MenuDAL();
        }

        public IList<MenuEntity> Get(int iUserId)
        {
            return CoreFactory.menu.Get(iUserId);
        }

    }
}