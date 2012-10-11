using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Data.Common;

namespace HHSoft.FieldProtect.DataAccess
{
    /// <summary>
    /// 数据库操作类(Oracle)
    /// </summary>
    public class OracleDbOperation : DbOperation<OracleDataAdapter, OracleCommandBuilder, OracleParameter>
    {
        public OracleDbOperation(string conStr)
            : base(conStr)
        {

        }
    }
}
