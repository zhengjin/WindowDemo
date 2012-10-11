
using System;
using System.Collections.Generic;

namespace HHSoft.FieldProtect.Framework.Utility
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 从Enum中任意取一个Int值，将其转化成枚举类型值
        /// </summary>
        /// <param name="protocolType">枚举类型</param>
        /// <param name="enumIntValue">int值</param>
        /// <returns>object</returns>
        /// <example>ExampleNormalEnum status = (ExampleNormalEnum)EnumHelper.IntValueToEnum( typeof( ExampleNormalEnum ),1); 得到值为 ExampleNormalEnum.Online </example>
        public static object IntValueToEnum(Type protocolType, int enumIntValue)
        {
            object myObject = Enum.Parse(protocolType, Enum.GetName(protocolType, enumIntValue));
            return myObject;
        }

        /// <summary>
        /// 从Enum中任意取一个String值，将其转化成枚举类型值
        /// </summary>
        /// <param name="protocolType">枚举类型</param>
        /// <param name="enumStringValue">字符串</param>
        /// <returns>object</returns>
        /// <example>ExampleNormalEnum status = (ExampleNormalEnum)EnumHelper.StringValueToEnum( typeof( ExampleNormalEnum ),"Offline");得到值为 ExampleNormalEnum.Offline</example>
        public static object StringValueToEnum(Type protocolType, string enumStringValue)
        {
            object myObject = Enum.Parse(protocolType, enumStringValue, true);
            return myObject;
        }

        /// <summary>
        /// 通过给定的字符串数组和枚举类型，返回枚举对象数组集合
        /// </summary>
        /// <param name="protocolType">枚举类型</param>
        /// <param name="enumStringValues">字符串数组</param>
        /// <returns>枚举类型数组</returns>
        public static object[] StringValueToEnums(Type protocolType, string[] enumStringValues)
        {
            object[] array = new object[enumStringValues.Length];
            for (int i = 0; i < enumStringValues.Length; i++)
            {
                array[i] = StringValueToEnum(protocolType, enumStringValues[i]);
            }

            return array;
        }

        /// <summary>
        /// 得到一个Enum中的所有Int值
        /// </summary>
        /// <param name="protocolType">枚举类型</param>
        /// <returns>int数组</returns>
        public static int[] GetEnumIntValues(System.Type protocolType)
        {
            int[] myIntArray = new int[Enum.GetValues(protocolType).Length];
            Array.Copy(Enum.GetValues(protocolType), myIntArray, Enum.GetValues(protocolType).Length);
            return myIntArray;
        }

        /// <summary>
        /// 静态方法,根据枚举类型返回ASCII的字符串值
        /// </summary>
        /// <param name="protocolType">枚举类型</param>
        /// <param name="objectValue">枚举值</param>
        /// <returns>ASCII字符串值</returns>
        /// <example>EnumHelper.EnumValueToASCIIString( typeof( ExampleHexEnum ),ExampleHexEnum.Hide );得到的值为"("</example>
        public static string EnumValueToASCIIString(System.Type protocolType, object objectValue)
        {
            return HexStringToASCIIString(EnumValueToHexString(protocolType, objectValue));
        }

        /// <summary>
        /// 输入16进制的字符串,返回翻译成ASCII的字符串
        /// </summary>
        /// <param name="hexString">十六进制字符串</param>
        /// <returns>AscII码</returns>
        /// <example>EnumHelper.HexStringToASCIIString( "2A" ); 得到值为"*"，注意去掉16进制前置标志符号"0x"</example>
        public static string HexStringToASCIIString(string hexString)
        {
            int myInt16 = int.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier);
            char myChar = (char)myInt16;
            return myChar.ToString();
        }

        /// <summary>
        /// 静态方法,根据枚举类型返回16进制的字符串值
        /// </summary>
        /// <param name="protocolType">枚举类型</param>
        /// <param name="objectValue">枚举对象</param>
        /// <returns>16进制的字符串值</returns>
        /// <example>EnumHelper.EnumValueToHexString(typeof( ExampleHexEnum ),ExampleHexEnum.Hide);得到值"00000028"</example>
        public static string EnumValueToHexString(System.Type protocolType, object objectValue)
        {
            return Enum.Format(protocolType, Enum.Parse(protocolType, Enum.GetName(protocolType, objectValue)), "X");
        }

        /// <summary>
        /// 将16进制转换为 Enum 的值
        /// </summary>
        /// <param name="protocolType">枚举类型</param>
        /// <param name="hexString">十六进制字符串</param>
        /// <returns>object</returns>
        /// <example>EnumHelper.HexStringToEnumValue( typeof( ExampleHexEnum ),"28");得到值 "ExampleHexEnum.Hide"</example>
        public static object HexStringToEnumValue(System.Type protocolType, string hexString)
        {
            object myObject = Enum.Parse(protocolType, Enum.GetName(protocolType, Int16.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier)));
            return myObject;
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="enumValue">枚举值</param>
        /// <returns>描述</returns>
        public static string GetFieldDescription(Type type, int enumValue)
        {
            return ReflectHelper.GetFieldDescription(type, IntValueToEnum(type, enumValue).ToString());
        }

        /// <summary>
        /// 通过枚举类型返回数据源
        /// 前提：枚举的项描述需要填写
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>List泛型</returns>
        public static List<EnumTypeRecord> GetDataSource(Type enumType)
        {
            List<EnumTypeRecord> datasouce = new List<EnumTypeRecord>();
            foreach (string typeName in Enum.GetNames(enumType))
            {
                string descip = ReflectHelper.GetFieldDescription(enumType, typeName.Trim());
                if (!string.IsNullOrEmpty(descip))
                {
                    EnumTypeRecord record = new EnumTypeRecord(descip, Convert.ToInt32(StringValueToEnum(enumType, typeName.Trim())).ToString());
                    datasouce.Add(record);
                }
            }

            return datasouce;
        }

        /// <summary>
        /// 通过枚举类型和枚举字符串数组返回数据源
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="typeNames">类型名称</param>
        /// <returns>List</returns>
        public static List<EnumTypeRecord> GetDataSource(Type enumType, string[] typeNames)
        {
            List<EnumTypeRecord> datasouce = new List<EnumTypeRecord>();
            foreach (string typeName in typeNames)
            {
                if (!string.IsNullOrEmpty(typeName))
                {
                    string descip = ReflectHelper.GetFieldDescription(enumType, typeName.Trim());
                    if (!string.IsNullOrEmpty(descip))
                    {
                        EnumTypeRecord record = new EnumTypeRecord(descip, Convert.ToInt32(StringValueToEnum(enumType, typeName.Trim())).ToString());
                        datasouce.Add(record);
                    }
                }
            }

            return datasouce;
        }
    }

    /// <summary>
    /// 枚举实体类
    /// </summary>
    [Serializable]
    public class EnumTypeRecord
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="displayMember">显示名称</param>
        /// <param name="valueMember">值</param>
        public EnumTypeRecord(string displayMember, string valueMember)
        {
            this.DisplayMember = displayMember;
            this.ValueMember = valueMember;
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayMember
        {
            get;
            set;
        }

        /// <summary>
        /// 值
        /// </summary>
        public string ValueMember
        {
            get;
            set;
        }

        #region 重写Equals和HashCode
        /// <summary>
        /// 用唯一值实现Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != GetType())) return false;
            EnumTypeRecord castObj = (EnumTypeRecord)obj;
            return (castObj != null) &&
                (DisplayMember == castObj.DisplayMember);
        }

        /// <summary>
        /// 用唯一值实现GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * DisplayMember.GetHashCode();
            return hash;
        }
        #endregion
    }
}
