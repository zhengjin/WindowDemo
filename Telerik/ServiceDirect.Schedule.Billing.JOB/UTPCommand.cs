using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceDirect.Schedule.Billing.JOB
{
    /// <summary>
    /// 作者：崔扬
    /// 创建日期：2011-2-22
    /// 功能说明：UTP命令行的模型类，提供命令行构建时所需要的所有参数。
    /// </summary>
    /// 

    //操作类型枚举
    public enum ActionType
    {
        Backup,         //Schedule Backup操作
        Billing,        //Schedule Billing操作
        Snapshot,       //Schedule Snapshot操作
        Pastdue         //Schedule Pastdue操作
    }

    //镜像复制
    //指定需要UTP执行Schedule Billing操作时，是否执行Billing Shadow Copies,默认不执行。
    public enum BillingShadowCopies
    {
        Yes,
        No
    }

    //计算用量
    //指定当UTP执行Schedule Billing操作时，是否执行Calc Usage before billing register，默认不执行。
    public enum CalcUsageBeforeBillingRegister
    {
        Yes,
        No
    }

    //日志
    //指定当UTP执行Schedule Billing操作之间，是否记录被屏蔽的消息，默认为不执行。
    public enum Trace
    {
        Yes,
        No
    }

    //日志类型
    //当指定属性Trace=Yes时必填，用来指定保存消息的类型。
    public enum TraceType
    {
        File,       //文件
        Table,
        None       //数据表
    }



    public class UTPCommand
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        private ActionType _ActionType;
        /// <summary>
        /// 自动计算用量。
        /// </summary>
        private CalcUsageBeforeBillingRegister _AutoCalc;
        /// <summary>
        /// 公司标识。
        /// </summary>
        private string _Company;
        /// <summary>
        /// 算费周期范围。
        /// </summary>
        private string _Cycle;
        /// <summary>
        /// 镜像复制
        /// </summary>
        private BillingShadowCopies _ShadowCopies;
        /// <summary>
        /// 账户状态
        /// </summary>
        private string _Status;
        /// <summary>
        /// 日志
        /// </summary>
        private Trace _Trace;
        /// <summary>
        /// 日志数据库连接字符串
        /// </summary>
        private string _TraceDBConnectionString;
        /// <summary>
        /// 日志文件保存路径
        /// </summary>
        private string _TraceFilePath;
        /// <summary>
        /// 日志类型
        /// </summary>
        private TraceType _TraceType;
        /// <summary>
        /// 口令
        /// </summary>
        private string _UTPPassword;
        /// <summary>
        /// 用户名
        /// </summary>
        private string _UTPUsername;
        /// <summary>
        /// 计划标识
        /// </summary>
        private Guid _SID;

        //构造函数
        //初始化类属性
        public UTPCommand()
        {
            _ActionType=ActionType.Billing;
            _AutoCalc = CalcUsageBeforeBillingRegister.No;
            _ShadowCopies = BillingShadowCopies.No;
            _Trace = Trace.No;
            _TraceType = TraceType.None;
        }

        /// <summary>
        /// 操作类型。
        /// </summary>
        /// <remarks>默认为Billing。</remarks>
        /// <value>1</value>
        public ActionType ActiveType
        {
            get
            {
                return _ActionType;
            }
            set
            {
                _ActionType = value;
            }
        }

        /// <summary>
        /// 公司标识。
        /// </summary>
        /// <remarks>Billing操作的Company参数信息。</remarks>
        public string Company
        {
            get
            {
                return _Company;
            }
            set
            {
                _Company = value;
            }
        }

        /// <summary>
        /// 算费周期范围。
        /// </summary>
        /// <remarks>算昆周期的范围，此参数由两个值构成。使用逗号进行分隔。</remarks>
        public string Cycle
        {
            get
            {
                return _Cycle;
            }
            set
            {
                _Cycle = value;
            }
        }

        /// <summary>
        /// 账户状态
        /// </summary>
        /// <remarks>参与算费操作的账户状态。</remarks>
        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }

        /// <summary>
        /// 镜像复制
        /// </summary>
        /// <remarks>指定当UTP执行Schedule Billing操作时，是否执行Billing Shadow Copies，默认值：No.</remarks>
        /// <value>1</value>
        public BillingShadowCopies ShadowCopies
        {
            get
            {
                return _ShadowCopies;
            }
            set
            {
                _ShadowCopies = value;
            }
        }

        /// <summary>
        /// 自动计算用量。
        /// </summary>
        /// <remarks>指定当UTP执行Schedule Billing操作时，是否执行Calc Usage before billing register，默认值：No.</remarks>
        /// <value>1</value>
        public CalcUsageBeforeBillingRegister AutoCalc
        {
            get
            {
                return _AutoCalc;
            }
            set
            {
                _AutoCalc = value;
            }
        }

        /// <summary>
        /// 日志
        /// </summary>
        /// <remarks>指定当前UTP执行Schedule Billing操作过程中是否记录被屏蔽的消息，默认值：No.</remarks>
        /// <value>1</value>
        public Trace Trace
        {
            get
            {
                return _Trace;
            }
            set
            {
                _Trace = value;
            }
        }

        /// <summary>
        /// 日志类型
        /// </summary>
        /// <remarks>当指定属性Trace=Yes时必填，用来指定保存消息的类型。</remarks>
        /// <value>2</value>
        public TraceType TraceType
        {
            get
            {
                return _TraceType;
            }
            set
            {
                _TraceType = value;
            }
        }

        /// <summary>
        /// 日志文件保存路径
        /// </summary>
        /// <remarks>当指定发生值type=file时选填，指定消息保存到文件时，文件所在路径，默认为UTP可执行程序路径下的.\Log\Scheduler\文件夹下。</remarks>
        public string TraceFilePath
        {
            get
            {
                return _TraceFilePath;
            }
            set
            {
                _TraceFilePath = value;
            }
        }

        /// <summary>
        /// 日志数据库连接字符串
        /// </summary>
        /// <remarks>当指定属性值type：table时选填，指定消息保存到表中时，表所在的数据链接信息，默认为UTP当前所链接的数据库。</remarks>
        public string TraceDBConnectionString
        {
            get
            {
                return _TraceDBConnectionString;
            }
            set
            {
                _TraceDBConnectionString = value;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        /// <remarks>指定登录UTP系统的用户名。</remarks>
        public string UTPUsername
        {
            get
            {
                return _UTPUsername;
            }
            set
            {
                _UTPUsername = value;
            }
        }

        /// <summary>
        /// 口令
        /// </summary>
        /// <remarks>指定登录UTP系统的口令。</remarks>
        public string UTPPassword
        {
            get
            {
                return _UTPPassword;
            }
            set
            {
                _UTPPassword = value;
            }
        }

        /// <summary>
        /// 计划标识
        /// </summary>
        /// <remarks>指定与Schedule Billing Job相关的JOB ID。</remarks>
        public Guid SID
        {
            get
            {
                return _SID;
            }
            set
            {
                _SID = value;
            }
        }

        /// <summary>
        /// UTP程序路径
        /// </summary>
        /// <remarks>保存了UTP EXE文件所在的应用程序路径,此属性值要求包含Utility.exe的全路径。</remarks>
        public string UTPPath
        {
            get;
            set;
        }


        /// <summary>
        /// 计划标识符
        /// </summary>
        /// <remarks>需要保存到UTP数据中的计划标识符。Schedule ID</remarks>
        public string GUID { get; set; }


    }
}
