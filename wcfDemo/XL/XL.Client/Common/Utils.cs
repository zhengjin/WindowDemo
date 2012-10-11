using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XL.Client
{
    public static class Utils
    {
        /// <summary>
        /// 主窗口
        /// </summary>
        private static MainForm mf;
        /// <summary>
        /// 服务端发生错误
        /// </summary>
        /// <param name="ex"></param>
        public static void OnException(Exception ex)
        {
            if (ex.Message.Equals("#请重新登录#"))
            {
                Alert("请重新登录");
                ReLogin();
                return;
            }
            Alert(ex.Message);
        }
        /// <summary>
        /// 重新登录
        /// </summary>
        public static void ReLogin()
        {
            var path = Application.ExecutablePath;
            System.Diagnostics.Process.Start(path);
            System.Environment.Exit(0);
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="msg"></param>
        public static void Alert(string msg)
        {
            MessageBox.Show(msg, "系统提示", MessageBoxButtons.OK);
        }
        /// <summary>
        /// 判断是否为设计状态
        /// </summary>
        /// <returns></returns>
        public static bool IsInDesignMode()
        {
            bool returnFlag = false;
            #if DEBUG
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                returnFlag = true;
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper().Equals("DEVENV"))
            {
                returnFlag = true;
            }
            #endif
            return returnFlag;
        }
        /// <summary>
        /// 获取主窗口
        /// </summary>
        /// <returns></returns>
        public static MainForm GetMainForm()
        {
            if (mf == null)
            {
                mf = Application.OpenForms["MainForm"] as MainForm;
            }
            return mf;
        }

    }
}
