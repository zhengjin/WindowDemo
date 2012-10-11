using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HHSoft.FieldProtect.Framework.Utility
{
    /// <summary>
    /// 加密，解密帮助类
    /// </summary>
    public class EncryptHelper
    {
        /// <summary>
        /// 对称加密算法抽象类
        /// </summary>
        private static SymmetricAlgorithm mcsp;

        /// <summary>
        /// 构造函数
        /// </summary>
        public EncryptHelper()
        {
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        /// <returns>加密后字符串</returns>
        public static string EncryptString(string value)
        {
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            //// 设置加密Key
            string txtKey = "tkGGRmBErvc=";
            //// 设置加密IV
            string txtIV = "Kl7ZgtM1dvQ=";
            mcsp = SetEnc();
            byte[] byt2 = Convert.FromBase64String(txtKey);
            mcsp.Key = byt2;
            byte[] byt3 = Convert.FromBase64String(txtIV);
            mcsp.IV = byt3;

            ct = mcsp.CreateEncryptor(mcsp.Key, mcsp.IV);

            byt = Encoding.UTF8.GetBytes(value);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="value">要解密的值</param>
        /// <returns>解密后值</returns>
        public static string DecryptString(string value)
        {
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            //// 设置加密Key
            string txtKey = "tkGGRmBErvc=";
            //// 设置加密IV
            string txtIV = "Kl7ZgtM1dvQ=";
            mcsp = SetEnc();
            byte[] byt2 = Convert.FromBase64String(txtKey);
            mcsp.Key = byt2;
            byte[] byt3 = Convert.FromBase64String(txtIV);
            mcsp.IV = byt3;

            ct = mcsp.CreateDecryptor(mcsp.Key, mcsp.IV);

            byt = Convert.FromBase64String(value);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Encoding.UTF8.GetString(ms.ToArray());
        }

        /// <summary>
        /// 使用MD5加密
        /// </summary>
        /// <param name="strToHash">需要加密的string</param>
        /// <returns>加密后的string</returns>
        public static string HashTextMD5(string strToHash)
        {
            MD5CryptoServiceProvider md5 = null;
            byte[] bytValue;
            byte[] bytHash;
            string strPassword;
            md5 = new MD5CryptoServiceProvider();
            bytValue = System.Text.Encoding.UTF8.GetBytes(strToHash);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            strPassword = Convert.ToBase64String(bytHash);
            return strPassword;
        }

        /// <summary>
        /// 解密经客户端加密的密码
        /// </summary>
        /// <param name="value">经客户端加密的密码</param>
        /// <returns>解密后值</returns>
        public static string DecryptCString(string value)
        {
            string sPw = string.Empty;
            int count = value.Length / 4;
            for (int i = 0; i < count; i++)
            {
                sPw = sPw + value.Substring(3 + (4 * i), 1);
            }

            return sPw;
        }

        /// <summary>
        /// 对称加密实例
        /// </summary>
        /// <returns>SymmetricAlgorithm</returns>
        private static SymmetricAlgorithm SetEnc()
        {
            return new DESCryptoServiceProvider();
        }
    }
}
