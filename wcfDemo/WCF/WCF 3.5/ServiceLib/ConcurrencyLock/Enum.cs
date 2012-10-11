using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace WCF.ServiceLib.ConcurrencyLock
{
    /// <summary>
    /// 锁 类型的枚举
    /// </summary>
    [DataContract]
    public enum LockType
    {
        /// <summary>
        /// 不使用任何并发控制
        /// </summary>
        [EnumMember]
        None,
        /// <summary>
        /// Mutex
        /// </summary>
        [EnumMember]
        Mutex,
        /// <summary>
        /// Semaphore
        /// </summary>
        [EnumMember]
        Semaphore,
        /// <summary>
        /// Monitor
        /// </summary>
        [EnumMember]
        Monitor,
        /// <summary>
        /// Lock
        /// </summary>
        [EnumMember]
        Lock
    }
}
