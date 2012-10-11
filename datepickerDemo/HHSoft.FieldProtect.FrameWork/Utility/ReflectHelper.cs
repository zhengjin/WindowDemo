using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace HHSoft.FieldProtect.Framework.Utility
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public class ReflectHelper
    {
        /// <summary>
        /// 程序集缓存
        /// </summary>
        private static Hashtable assemblyCaches;

        /// <summary>
        /// 返回Type
        /// </summary>
        /// <param name="typeName">类型名</param>
        /// <returns>type</returns>
        public static Type GetType(string typeName)
        {
            return ResolveType(typeName);
        }

        #region 反射创建对象
        /// <summary>
        /// 通过Type字符串发射获取对象
        /// </summary>
        /// <param name="className">要得到的对象的类型名</param>
        /// <returns>指定类型的对象的实例</returns>
        public static object CreateInstance(string className)
        {
            return CreateInstance(GetType(className));
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="args">参数集合 object[] </param>
        /// <returns>object</returns>
        public static object CreateInstance(string className, object[] args)
        {
            return CreateInstance(GetType(className), args);
        }

        /// <summary>
        /// 通过Type类型反射获取对象
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>object</returns>
        public static object CreateInstance(Type type)
        {
            return System.Activator.CreateInstance(type);
        }

        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="args">arg[]</param>
        /// <returns>object</returns>
        public static object CreateInstance(Type type, object[] args)
        {
            return System.Activator.CreateInstance(type, args);
        }

        #endregion

        /// <summary>
        /// 调用反射对象的方法
        /// </summary>
        /// <param name="data">对其调用方法的对象</param>
        /// <param name="name">方法的名称</param>
        /// <returns>一个对象，包含被调用方法的返回值</returns>
        public static object InvokeMethod(object data, string name)
        {
            return InvokeMethod(data, name, new Type[0], null);
        }

        /// <summary>
        /// 调用反射对象的方法
        /// </summary>
        /// <param name="data">对其调用方法的对象</param>
        /// <param name="name">方法的名称</param>
        /// <param name="types">此方法所需参数的个数、顺序和类型的 Type 对象数组。或者不使用参数的方法的 Type 类型的空数组（即 Type[] types = new Type[0]）。 </param>
        /// <param name="parameters">此方法所需参数的个数、顺序和类型的对象数组。如果没有任何参数，则 parameters 应为空引用。</param>
        /// <returns>一个对象，包含被调用方法的返回值</returns>
        public static object InvokeMethod(object data, string name, Type[] types, object[] parameters)
        {
            MethodInfo method = data.GetType().GetMethod(name, types);
            if (method != null)
            {
                return method.Invoke(data, parameters);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 设置反射对象的属性
        /// 此方法所需参数的个数、顺序和类型的对象数组。如果没有任何参数，则 parameters 应为空引用。
        /// </summary>
        /// <param name="data">对其设置属性的对象</param>
        /// <param name="name">属性的名称</param>
        /// <param name="value">属性的返回类型</param>
        public static void InvokePropertySet(object data, string name, object value)
        {
            PropertyInfo property = data.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
            if (property != null)
            {
                MethodInfo method = property.GetSetMethod();
                if (method != null)
                {
                    method.Invoke(data, new object[] { value });
                }
            }
        }

        /// <summary>
        /// 获取反射对象的属性
        /// </summary>
        /// <param name="data">获取其属性的对象</param>
        /// <param name="name">属性的名称</param>
        /// <returns>一个对象，包含属性的值</returns>
        public static object InvokePropertyGet(object data, string name)
        {
            if (data != null && name != null)
            {
                PropertyInfo property = data.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
                if (property == null)
                {
                    property = data.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
                }

                if (property != null)
                {
                    MethodInfo method = property.GetGetMethod();
                    if (method != null)
                    {
                        return method.Invoke(data, null);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 获取一个值，通过该值指示 Type 是否具有需要的属性名称。
        /// </summary>
        /// <param name="data">获取其属性的对象</param>
        /// <param name="name">属性的名称</param>
        /// <returns>如果 Type 具有需要的属性，则为 true；否则为 false。 </returns>
        public static bool HasProperty(object data, string name)
        {
            if (data != null && name != null)
            {
                PropertyInfo property = data.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                if (property == null)
                {
                    property = data.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
                }

                if (property != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 根据程序集字符串，反编译获取Type
        /// </summary>
        /// <param name="assemblyString">程序集字符串，
        /// 例如：HHSoft.Frameworks.Configuration.EventConfig,HHSoft.Frameworks.Configuration</param>
        /// <returns>System.Type</returns>
        public static Type ResolveType(string assemblyString)
        {
            if (assemblyCaches == null)
            {
                assemblyCaches = new Hashtable();
            }

            if (assemblyString != null && assemblyString.Length > 0)
            {
                string[] s = assemblyString.Split(',');
                Assembly assem = (Assembly)assemblyCaches[s[1].Trim()];
                if (assem == null)
                {
                    assem = Assembly.Load(s[1].Trim());
                    if (!assemblyCaches.Contains(s[1].Trim()))
                    {
                        lock (assemblyCaches.SyncRoot)
                        {
                            assemblyCaches.Add(s[1].Trim(), assem);
                        }
                    }
                }

                return assem.GetType(s[0]);
            }

            return null;
        }

        #region 从对象中获取指定字段

        /// <summary>
        /// 从对象中获取指定字段的属性对象
        /// </summary>
        /// <param name="source">数据源对象</param>
        /// <param name="field">指定字段</param>
        /// <returns>object</returns>
        public static object GetObject(object source, string field)
        {
            object result = null;
            PropertyInfo property = source.GetType().GetProperty(field);
            if (property != null)
            {
                MethodInfo methodInfo = property.GetGetMethod();
                if (methodInfo != null)
                {
                    result = methodInfo.Invoke(source, null);
                }
            }

            return result;
        }
        #endregion

        #region 其他反射方法

        /// <summary>
        /// 返回指定类型的字段描述
        /// </summary>
        /// <param name="destType">类型</param>
        /// <param name="filedName">字段名称</param>
        /// <returns>字段描述</returns>
        public static string GetFieldDescription(Type destType, string filedName)
        {
            string result = string.Empty;
            FieldInfo filedInfo = destType.GetField(filedName.Trim());
            try
            {
                if (filedInfo != null)
                {
                    DescriptionAttribute dna = (DescriptionAttribute)Attribute.GetCustomAttribute(filedInfo, typeof(DescriptionAttribute));
                    result = dna.Description;
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception("返回指定类型的字段描述异常", ex);
            }

            return result;
        }

        /// <summary>
        /// 获取字段描述
        /// </summary>
        /// <param name="destType">类型</param>
        /// <param name="fieldNames">字段名称</param>
        /// <returns>描述</returns>
        public static string GetFieldsDescription(Type destType, string[] fieldNames)
        {
            string result = string.Empty;
            for (int i = 0; i < fieldNames.Length; i++)
            {
                result += GetFieldDescription(destType, fieldNames[i].ToString().Trim()) + ",";
            }

            if (result.EndsWith(","))
            {
                result = result.Substring(0, result.Length - 1);
            }

            return result;
        }

        /// <summary>
        /// 获取属性描述信息
        /// </summary>
        /// <param name="destType">类型</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>描述</returns>
        public static string GetPropertyDescription(Type destType, string propertyName)
        {
            string result = string.Empty;
            PropertyInfo propertyInfo = destType.GetProperty(propertyName.Trim());
            try
            {
                if (propertyInfo != null)
                {
                    DescriptionAttribute dna = (DescriptionAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(DescriptionAttribute));
                    result = dna.Description;
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception("获取属性描述信息", ex);
            }

            return result;
        }

        #endregion

        public static T Clone<T>(T item)
        {
            Type type = item.GetType();
            T result = (T)CreateInstance(type);
            foreach (var propertyInfo in type.GetProperties())
            {
                object value = propertyInfo.GetValue(item, null);
                propertyInfo.SetValue(result, value, null);
            }
            return result;
        }

        public static bool Compare(object a, object b)
        {
            Type typeA = a.GetType();
            Type typeB = b.GetType();
            foreach (var propertyInfoB in typeB.GetProperties())
            {
                string propertyName = propertyInfoB.Name;
                PropertyInfo propertyInfoA = typeA.GetProperty(propertyName);
                if (propertyInfoA != null)
                {
                    if (!propertyInfoB.GetValue(b, null).Equals(propertyInfoA.GetValue(a, null)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
