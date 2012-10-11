using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceDirect.Schedule.Billing.JOB
{
    /// <summary>
    /// 作者：崔扬
    /// 创建日期：2011-2-22
    /// 功能说明：根据参数提供的操作操作类型，生成
    /// </summary>

    public  class UTPCommandBuilder
    {
        /// <summary>
        /// 临时命令行字符串
        /// </summary>
        /// <remarks>保存构造函数生成的命令行字符串。</remarks>
        private string _CommandString;
        private string _JobCommandString;
        private string _CommandID;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <remarks>根据参数值生成命令行字符串。</remarks>
        /// <param name="Command">命令行构建参数。</param>
        public UTPCommandBuilder(UTPCommand Command)
        {
            _CommandString= BuilderCommandString(Command);
            _CommandID = Guid.NewGuid().ToString();
            _JobCommandString = Command.UTPPath + " -COMMAND luid:" + Command.GUID;
        }

        /// <summary>
        /// 命令行字符串
        /// </summary>
        /// <remarks>为类实例设置了对应的属性值后，使用该属性返回合并的命令行字符串。</remarks>
        public string CommandString
        {
            get
            {
                return _CommandString;
            }
        }
        
        /// <summary>
        /// 命令标识符
        /// </summary>
        /// <remarks>为类实例设置了对应的属性值后，使用该属性返回命令标识符。</remarks>
        public string CommandID 
        { 
            get 
            { 
                return _CommandID; 
            } 
        }

        /// <summary>
        /// 作业标识符
        /// </summary>
        /// <remarks>为类实例设置了对应的属性值后，使用该属性返回命令标识符。</remarks>
        public string JobCommandString
        {
            get 
            {
                return _JobCommandString;
            }
        }
        /// <summary>
        /// 根据属性值生成字符串
        /// </summary>
        /// <remarks>根据当前类的属性设置值生成命令行字符串，此方法为私有方法。</remarks>
        /// <param name="Command">命令行构建参数</param>
        private string BuilderCommandString(UTPCommand Command)
        {
            string tmpCommandString;

            try
            {
                //生成命令头
                tmpCommandString =  " -s";

                //构建操作类型部分
                switch (Command.ActiveType)
                {
                    case ActionType.Backup:
                        tmpCommandString = tmpCommandString + " category:backup";
                        break;
                    case ActionType.Billing:
                        tmpCommandString = tmpCommandString + " category:billing";
                        break;
                    case ActionType.Pastdue:
                        tmpCommandString = tmpCommandString + " category:snapshot";
                        break;
                    case ActionType.Snapshot:
                        tmpCommandString = tmpCommandString + " category:pastdue";
                        break;
                }

                //构建公司信息部分
                tmpCommandString = tmpCommandString + " company:" + Command.Company;

                //构建算费周期部分
                tmpCommandString = tmpCommandString + " cycles:" + Command.Cycle;

                if (Command.Status != "All")
                {
                    //构建账户状态部分
                    tmpCommandString = tmpCommandString + " status:" + Command.Status;
                }
                //构建算费镜像部分
                tmpCommandString = tmpCommandString + " copy:" + (Command.ShadowCopies == BillingShadowCopies.Yes ? "y" : "n");

                //构建自动计算用量部分
                tmpCommandString = tmpCommandString + " calc:" + (Command.AutoCalc == CalcUsageBeforeBillingRegister.Yes ? "y" : "n");

                //构建日志部分
                //tmpCommandString = tmpCommandString + " trace:" + (Command.Trace == Trace.Yes ? "y" : "n");
                tmpCommandString = tmpCommandString + " trace:y";
                if (Command.Trace == Trace.Yes)
                {
                    switch (Command.TraceType)
                    {
                        case TraceType.File:
                            tmpCommandString = tmpCommandString + " file " + Command.TraceFilePath;
                            break;
                        case TraceType.Table:
                            tmpCommandString = tmpCommandString + " table " + Command.TraceDBConnectionString;
                            break;
                    }
                }
                tmpCommandString = tmpCommandString + " table " + "\""+"\"";
                //构建计划信息
                tmpCommandString = tmpCommandString + " sid:" + Command.SID.ToString();
                
                //构建用户信息
                tmpCommandString = tmpCommandString + " -u " + "\""+Command.UTPUsername+"\"";

                //构建口令信息
                tmpCommandString = tmpCommandString + " -p " +"\""+ Command.UTPPassword+"\"";

                return tmpCommandString;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
