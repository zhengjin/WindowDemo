using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace XL.Service
{
    public static class CacheStrategy
    {
        //一个小时
        private static int timeOut = 3600;
        /// <summary>
        /// 添加一个对象到缓存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        public static void AddObject(Guid id, object obj)
        {
            var outTime = DateTime.Now.AddSeconds(timeOut);
            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);
            HttpRuntime.Cache.Insert(id.ToString(), obj, null, outTime, Cache.NoSlidingExpiration, CacheItemPriority.High, callBack);
        }
        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        public static void RemoveObject(Guid id)
        {
            HttpRuntime.Cache.Remove(id.ToString());
        }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="id"></param>
        public static object GetObject(Guid id)
        {
            var result = HttpRuntime.Cache.Get(id.ToString());
            return result;
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="id"></param>
        public static bool HasKey(Guid id)
        {
            var result = HttpRuntime.Cache.Get(id.ToString()) != null;
            return result;
        }
        /// <summary>
        /// 缓存被移除的事件
        /// do what you want
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="reason"></param>
        public static void onRemove(string key, object val, CacheItemRemovedReason reason)
        {
            switch (reason)
            {
                case CacheItemRemovedReason.DependencyChanged://依赖项已更改
                    {
                        break;
                    }
                case CacheItemRemovedReason.Expired://过期移除
                    {
                        break;
                    }
                case CacheItemRemovedReason.Removed://修改和删除
                    {
                        break;
                    }
                case CacheItemRemovedReason.Underused://释放内存移除
                    {
                        break;
                    }
                default: break;
            }	
        }
    }
}