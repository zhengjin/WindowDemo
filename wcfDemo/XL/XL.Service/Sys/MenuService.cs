using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XL.ServiceAPI.Sys;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using XL.Models.Sys;
using XL.DataAccess.Sys;
using XL.Service;
using System.ServiceModel;

namespace XL.Service.Sys
{

    public class MenuService:ServiceBase,IMenu
    {
        MenuDA da = new MenuDA();
        public MenuService()
        {
            var id = HttpContext.Current.Request["UserName"];
        }
        public List<MenuModel> GetAllMenu()
        {
            var result = da.GetAllModels();
            return result;
        }
        public bool EditMenu(MenuModel target)
        {
            var result = da.Update(target);
            return result;
        }
    }
}