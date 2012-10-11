using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


namespace HHSoft.FieldProtect.FrameWork.Utility
{
    public class JsonHelper
    {

        #region 反射一个对象所有属性和属性值和将一个对象的反射结果封装成jsons格式
        /**
          * 对象的全部属性和属性值。用于填写json的{}内数据
          * 生成后的格式类似
          * "属性1":"属性值"
          * 将这些属性名和属性值写入字符串列表返回
          * */
        private static List<string> GetObjectProperty(object o)
        {
            List<string> propertyslist = new List<string>();
            PropertyInfo[] propertys = o.GetType().GetProperties();
            foreach (PropertyInfo p in propertys)
            {
                propertyslist.Add("\"" + p.Name.ToString() + "\":\"" + p.GetValue(o, null) + "\"");
            }
            return propertyslist;
        }
        /**
          * 将一个对象的所有属性和属性值按json的格式要求输入为一个封装后的结果。
          *
          * 返回值类似{"属性1":"属性1值","属性2":"属性2值","属性3":"属性3值"}
          * 
          * */
        private static string OneObjectToJSON(object o)
        {
            string result = "{";
            List<string> ls_propertys = new List<string>();
            ls_propertys = GetObjectProperty(o);
            foreach (string str_property in ls_propertys)
            {
                if (result.Equals("{"))
                {
                    result = result + str_property;
                }
                else
                {
                    result = result + "," + str_property + "";
                }
            }
            return result + "}";
        }
        #endregion

        public static string toJSON<T>(List<T> objlist)
        {
            return toJSON(objlist, string.Empty);
        }

        public static string toJSON<T>(List<T> objlist, string classname)
        {
            string result = "{";
            if (classname.Equals(string.Empty))
            {
                object o = (object)objlist[0];
                classname = o.GetType().ToString();
            }
            result += "\"" + classname + "\":[";

            bool firstline = true;//处理第一行前面不加","号
            foreach (object oo in objlist)
            {
                if (!firstline)
                {
                    result = result + "," + OneObjectToJSON(oo);
                }
                else
                {
                    result = result + OneObjectToJSON(oo) + "";
                    firstline = false;
                }
            }
            return result + "]}";
        }
 
    }
}

 

 

