using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;

namespace UTPWCFDataService
{
    public class UTPDataProxy : DataService<UTPDataEntities>
    {
        // 仅调用此方法一次以初始化涉及服务范围的策略。
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: 设置规则以指明哪些实体集和服务操作是可见的、可更新的，等等。
            // 示例:
            // config.SetEntitySetAccessRule("MyEntityset", EntitySetRights.AllRead);
            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            config.SetEntitySetAccessRule("View_CustomerInformation", EntitySetRights.AllRead);
        }
    }
}
