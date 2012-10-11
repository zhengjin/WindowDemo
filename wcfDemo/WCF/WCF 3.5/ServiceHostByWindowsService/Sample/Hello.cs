using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.ServiceModel;
using System.ServiceProcess;

namespace WCF.ServiceHostByWindowsService.Sample
{
    /// <summary>
    /// 初始化 System.Configuration.Install.Installer 类的新实例。
    /// </summary>
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ProjectInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "WCF.ServiceHostByWindowsService";
            service.Description = "WCF服务宿主在WindowsService[webabcd测试用]";
            base.Installers.Add(process);
            base.Installers.Add(service);
        }
    }

    /// <summary>
    /// Windows服务类
    /// </summary>
    public class WindowsService : ServiceBase
    {
        /// <summary>
        /// 提供服务的主机
        /// </summary>
        public ServiceHost serviceHost = null;

        /// <summary>
        /// 主函数
        /// </summary>
        public static void Main()
        {
            ServiceBase.Run(new WindowsService());
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public WindowsService()
        {
            base.ServiceName = "WCF.ServiceHostByWindowsService";
        }

        /// <summary>
        /// 启动Windows服务
        /// </summary>
        /// <param name="args">args</param>
        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            // 为WCF.ServiceLib.Sample.Hello创建ServiceHost
            serviceHost = new ServiceHost(typeof(WCF.ServiceLib.Sample.Hello));

            serviceHost.Open();

            #region ServiceHost的几个事件（顾名思义）
            /*
            serviceHost.Opening += ...
            serviceHost.Opened += ...
            serviceHost.Closing += ...
            serviceHost.Faulted += ...
            serviceHost.UnknownMessageReceived += ...
             */
            #endregion
        }

        /// <summary>
        /// 停止Windows服务
        /// </summary>
        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
