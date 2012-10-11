using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectCore
{
    public class DbLoggerBLL
    {
        public static NLog.Logger SysLogger = NLog.LogManager.GetLogger("Database");
        public static NLog.Logger FileLoger = NLog.LogManager.GetLogger("ServiceDirect.MobileWorkOrder.BLL.UserBLL");
    }
}
