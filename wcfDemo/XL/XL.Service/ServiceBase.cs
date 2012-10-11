using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using System.ServiceModel;
using System.Web.SessionState;
using System.ServiceModel.Channels;

namespace XL.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class ServiceBase
    {
        protected ServiceBase()
        {
            CheckLogin();
        }
        /// <summary>
        /// 判断是否登录
        /// </summary>
        protected virtual void CheckLogin()
        {
            var curId = OperationContext.Current.IncomingMessageHeaders.GetHeader<Guid>("token", "ns");
            if (!CacheStrategy.HasKey(curId))
            {
                throw new Exception("#请重新登录#");
            }
        }
    }
}