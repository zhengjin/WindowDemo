using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.ConcurrencyLock
{
    /// <summary>
    /// 演示并发控制(锁)的接口
    /// </summary>
    /// <remarks>
    /// ServiceBehavior - 指定服务协定实现的内部执行行为
    /// 实例模型：单例；并发模式：多线程
    /// 会有并发问题，通过 锁 来解决
    /// </remarks>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Hello : IHello
    {
        private int _counter;
        private string _result;

        private System.Threading.Mutex _mutex = new System.Threading.Mutex();

        // 此构造函数初始化未命名的信号量。所有使用这类信号量的实例的线程都必须具有对该实例的引用。
        // 如果 initialCount 小于 maximumCount，则效果与当前线程调用了 WaitOne（maximumCount 减去 initialCount）次相同。如果不想为创建信号量的线程保留任何入口，请对 maximumCount 和 initialCount 使用相同的数值。
        private System.Threading.Semaphore _semaphore = new System.Threading.Semaphore(1, 1);

        private static readonly object objLock = new object();


        /// <summary>
        /// 计数器
        /// </summary>
        /// <returns></returns>
        public void Counter(LockType lockType)
        {
            switch (lockType)
            {
                case LockType.None:
                    ExecuteNone();
                    break;
                case LockType.Mutex:
                    ExecuteMutex();
                    break;
                case LockType.Semaphore:
                    ExecuteSemaphore();
                    break;
                case LockType.Monitor:
                    ExecuteMonitor();
                    break;
                case LockType.Lock:
                    ExecuteLock();
                    break;
            }
        }

        /// <summary>
        /// 获取计数器被调用的结果
        /// </summary>
        /// <returns></returns>
        public string GetResult()
        {
            return _result;
        }

        /// <summary>
        /// 清空计数器和结果
        /// </summary>
        public void CleanResult()
        {
            _result = "";
            _counter = 0;
        }

        /// <summary>
        /// 循环调用技术器，以模拟并发
        /// 结果中，出现重复计数，则有并发问题，反之，则无并发问题
        /// </summary>
        private void CircleCounter()
        {
            for (int i = 0; i < 10; i++)
            {
                var counter = _counter;

                // 停20毫秒，以模拟并发
                System.Threading.Thread.Sleep(20);

                _counter = ++counter;

                // 保存计数结果
                _result += _counter + "|";
            }
        }

        /// <summary>
        /// 不使用任何并发控制
        /// </summary>
        private void ExecuteNone()
        {
            CircleCounter();
        }

        /// <summary>
        /// Mutex的实现
        /// </summary>
        private void ExecuteMutex()
        {
            try
            {
                _mutex.WaitOne();

                CircleCounter();
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Semaphore的实现
        /// </summary>
        private void ExecuteSemaphore()
        {
            try
            {
                _semaphore.WaitOne();

                CircleCounter();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        /// <summary>
        /// Monitor的实现
        /// </summary>
        private void ExecuteMonitor()
        {
            try
            {
                System.Threading.Monitor.Enter(this);

                CircleCounter();
            }
            finally
            {
                System.Threading.Monitor.Exit(this);
            }
        }

        /// <summary>
        /// Lock的实现
        /// </summary>
        private void ExecuteLock()
        {
            try
            {
                lock (objLock)
                {
                    CircleCounter();
                }
            }
            finally
            {

            }
        }
    }
}
