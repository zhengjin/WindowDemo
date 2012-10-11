using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.Transaction
{
    /// <summary>
    /// IHi接口
    /// </summary>
    [ServiceContract]
    public interface IHi
    {
        /// <summary>
        /// 打招呼方法
        /// </summary>
        /// <param name="name">人名</param>
        /// <returns></returns>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        void WriteHi(string name);
    }

    /// <summary>
    /// Hi类
    /// </summary>
    public class Hi : IHi
    {
        /// <summary>
        /// 打招呼方法
        /// </summary>
        /// <param name="name">人名</param>
        /// <returns></returns>
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void WriteHi(string name)
        {
            if (DateTime.Now.Second % 2 == 0)
                throw new System.Exception("为测试事务而抛出的异常");

            DBDataContext ctx = new DBDataContext();

            ctx.Items.InsertOnSubmit(
                new Item
                {
                    Title = string.Format("Hi: {0}, TransactionId: {1}", name, System.Transactions.Transaction.Current.TransactionInformation.LocalIdentifier),
                    CreatedTime = DateTime.Now
                });

            ctx.SubmitChanges();
        }
    }
}
