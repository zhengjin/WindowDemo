using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XL.Models;
using XL.DataAccess;
using System.Transactions;

namespace XL.Logic
{
    public class ArticleL
    {
        public int AddArticle(ArticleM article)
        {
            var tran = new TransactionScope();
            try
            {
                //把文章数据写入数据库
                var articleDa = new ArticleDA();
                articleDa.AddArticle(article);
                //为新增加的文章生成静态页面
                
                //文章作者增加积分
                //写日志
                //提交事务
                tran.Complete();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {                
                tran.Dispose();
            }
            return 1;
        }
        //其他业务逻辑略,略
    }
}
