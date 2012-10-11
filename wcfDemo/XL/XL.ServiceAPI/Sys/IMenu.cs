using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using XL.Models.Sys;
using System.ServiceModel.Channels;

namespace XL.ServiceAPI.Sys
{
    [ServiceContract(SessionMode = SessionMode.Required)]    
    public interface IMenu
    {
        [OperationContract]
        List<MenuModel> GetAllMenu();
        [OperationContract]
        bool EditMenu(MenuModel target);
    }
}
