using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace WCF.ServiceLib.Transaction
{
    /// <summary>
    /// IHello接口
    /// </summary>
    [ServiceContract]
    public interface IHello
    {
        /// <summary>
        /// 打招呼方法
        /// </summary>
        /// <param name="name">人名</param>
        /// <remarks>
        /// TransactionFlow - 指定服务操作是否愿意接受来自客户端的传入事务
        /// NotAllowed - 禁止事务。默认值
        /// Allowed - 允许事务
        /// Mandatory - 强制事务
        /// </remarks>
        /// <returns></returns>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        void WriteHello(string name);
    }

    /// <summary>
    /// Hello类
    /// </summary>
    public class Hello : IHello
    {
        /// <summary>
        /// 打招呼方法
        /// </summary>
        /// <param name="name">人名</param>
        /// <remarks>
        /// OperationBehavior - 指定服务方法的本地执行行为
        /// 1、TransactionScopeRequired - 如果方法需要事务范围才能执行，则为 true；否则为 false。默认值为 false
        /// 将 TransactionScopeRequired 设置为 true，可以要求操作在事务范围内执行。如果流事务可用，则操作会在该事务内执行。如果流事务不可用，则会创建一个新事务并使用它来执行操作
        /// 2、TransactionAutoComplete - 默认值为 true
        /// true - 当方法完成执行时，将把该事务标志为完成（自动提交事务）
        /// false - 需要调用OperationContext.Current.SetTransactionComplete()方法来手工配置该事务的正确完成；否则，该事务将被标志为失败（手动提交事务）
        /// </remarks>
        /// <returns></returns>
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void WriteHello(string name)
        {
            DBDataContext ctx = new DBDataContext();

            ctx.Items.InsertOnSubmit(
                new Item
                {
                    Title = string.Format("Hello: {0}, TransactionId: {1}", name, System.Transactions.Transaction.Current.TransactionInformation.LocalIdentifier),
                    CreatedTime = DateTime.Now
                });

            ctx.SubmitChanges();
        }
    }
}
