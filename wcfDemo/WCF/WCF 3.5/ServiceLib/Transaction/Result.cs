using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.Transaction
{
    /// <summary>
    /// 结果接口
    /// </summary>
    [ServiceContract]
    public interface IResult
    {
        [OperationContract]
        List<Item> GetResult();
    }

    /// <summary>
    /// 结果类
    /// </summary>
    public class Result : IResult
    {
        /// <summary>
        /// 返回数据库结果
        /// </summary>
        /// <returns></returns>
        public List<Item> GetResult()
        {
            DBDataContext ctx = new DBDataContext();

            var result = from l in ctx.Items
                         orderby l.CreatedTime descending
                         select l;

            return result.ToList();
        }
    }
}
