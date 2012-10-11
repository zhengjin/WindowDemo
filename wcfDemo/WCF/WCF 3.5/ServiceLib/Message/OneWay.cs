using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.Message
{
    /// <summary>
    /// OneWay类
    /// </summary>
    public class OneWay : IOneWay
    {
        /// <summary>
        /// 不使用OneWay(同步调用)
        /// 抛出Exception异常
        /// </summary>
        public void WithoutOneWay()
        {
            throw new System.Exception("抛出Exception异常");
        }

        /// <summary>
        /// 使用OneWay(异步调用)
        /// 抛出Exception异常
        /// </summary>
        public void WithOneWay()
        {
            throw new System.Exception("抛出Exception异常");
        }
    }
}
