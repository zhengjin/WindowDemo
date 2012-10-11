using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.Message
{
    /// <summary>
    /// Duplex类
    /// </summary>
    public class Duplex : IDuplex
    {
        /// <summary>
        /// Hello
        /// </summary>
        /// <param name="name">名字</param>
        public void HelloDuplex(string name)
        {
            // 声明回调接口
            IDuplexCallback callback = OperationContext.Current.GetCallbackChannel<IDuplexCallback>();
            
            // 调用回调接口中的方法
            callback.HelloDuplexCallback(name);
        }
    }
}
