using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.FrameWork.Utility
{
    public static class TypeConvert
    {
        public static object Convert(object value, Type type)
        {
            if (value == null)
            {
                return null;
            }
            if (type == typeof(string))
            {
                return value.ToString();
            }
            else if (type == typeof(double) || type == typeof(double?))
            {
                double result;
                if (double.TryParse(value.ToString(), out result))
                {
                    return result;
                }
                else
                {
                    if (type == typeof(double))
                    {
                        return default(double);
                    }
                    else if (type == typeof(double?))
                    {
                        return default(double?);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                decimal result;
                if (decimal.TryParse(value.ToString(), out result))
                {
                    return result;
                }
                else
                {
                    if (type == typeof(decimal))
                    {
                        return default(decimal);
                    }
                    else if (type == typeof(decimal?))
                    {
                        return default(decimal?);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else if (type == typeof(int) || type == typeof(int?))
            {
                int result;
                if (int.TryParse(value.ToString(), out result))
                {
                    return result;
                }
                else
                {
                    if (type == typeof(int))
                    {
                        return default(int);
                    }
                    else if (type == typeof(int?))
                    {
                        return default(int?);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                DateTime result;
                if (DateTime.TryParse(value.ToString(), out result))
                {
                    return result;
                }
                else
                {
                    if (type == typeof(DateTime))
                    {
                        return default(DateTime);
                    }
                    else if (type == typeof(DateTime?))
                    {
                        return default(DateTime?);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                bool result;
                if (bool.TryParse(value.ToString(), out result))
                {
                    return result;
                }
                else
                {
                    if (type == typeof(bool))
                    {
                        return default(bool);
                    }
                    else if (type == typeof(bool?))
                    {
                        return default(bool?);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else if (type.IsEnum)
            {
                return Enum.Parse(type, value.ToString());
            }
            else
            {
                throw new Exception("类型转换时发现未知类型，请联系管理员！类型：" + type.FullName);
            }
        }

        public static T Convert<T>(object value)
        {
            T t = default(T);
            Type type = typeof(T);
            string valueString = null;
            object result = null;
            if (value != null)
            {
                valueString = value.ToString();
            }
            if (type == typeof(string))
            {
                return (T)(object)value;
            }
            else if (type == typeof(int) || type == typeof(int?))
            {
                int temp;
                if (int.TryParse(valueString, out temp))
                {
                    result = (object)temp;
                }
            }
            else if (type == typeof(double) || type == typeof(double?))
            {
                double temp;
                if (double.TryParse(valueString, out temp))
                {
                    result = (object)temp;
                }
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                decimal temp;
                if (decimal.TryParse(valueString, out temp))
                {
                    result = (object)temp;
                }
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                DateTime temp;
                if (DateTime.TryParse(valueString, out temp))
                {
                    result = (object)temp;
                }
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                bool temp;
                if (bool.TryParse(valueString, out temp))
                {
                    result = (object)temp;
                }
            }
            else if (type.IsEnum)
            {
                result = Enum.Parse(type, valueString);
            }
            else
            {
                throw new Exception("类型转换时发现未知类型，请联系管理员！类型：" + type.FullName);
            }

            if (result == null)
            {
                return t;
            }
            else
            {
                return (T)result;
            }
        }
    }
}
