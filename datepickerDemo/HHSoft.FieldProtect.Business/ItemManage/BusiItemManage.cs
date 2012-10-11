using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.DataEntity.SysManage;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.DataEntity.WorkFlow;
using HHSoft.FieldProtect.Framework.Utility;
using HHSoft.FieldProtect.DataEntity.ItemManage;
using System.Collections;
using HHSoft.FieldProtect.Business.Common;
using HHSoft.FieldProtect.FrameWork.Utility;
using System.Web;
using System.IO;
using System.Data.OracleClient;
using System.Web.UI.WebControls;

namespace HHSoft.FieldProtect.Business.ItemManage
{
    public class BusiItemManage
    {
        public BusiItemManage() { }


        #region 文件信息

        /// <summary>
        /// 获取项目文件
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public List<Item_File> GetItemFile(string itemCode)
        {
            string strSql = " select a.*, decode(a.filecode, 0, '其它文件', b.filename) FileType"
                        + " from item_file a  left join wf_file b on a.filecode = b.filecode"
                        + " Where a.ItemCode = '{0}' order by stage,a.filecode";
            strSql = string.Format(strSql, itemCode);
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            List<Item_File> fileList = new List<Item_File>();
            while (dr.Read())
            {
                Item_File file = new Item_File();
                file.ItemCode = dr["ItemCode"].ToString();
                file.FileCode = dr["FileCode"].ToString();
                file.FileCodeName = dr["FileType"].ToString();
                file.FileName = dr["FileName"].ToString();
                file.Stage = (ItemStage)EnumHelper.StringValueToEnum(typeof(ItemStage), dr["Stage"].ToString());
                file.NodeId = (WorkFlowNode)EnumHelper.StringValueToEnum(typeof(WorkFlowNode), dr["NodeId"].ToString());
                file.UserId = dr["UserId"].ToString();
                file.UserName = dr["UserName"].ToString();
                file.CreateDate = TypeConvert.Convert<DateTime>(dr["CreateDate"]);

                fileList.Add(file);
            }
            dr.Close();
            return fileList;
        }

        /// <summary>
        /// 获取项目文件(按环节)
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="wfNode"></param>
        /// <returns></returns>
        public List<Item_File> GetItemFile(string itemCode, WorkFlowNode wfNode)
        {
            string strSql = " select a.*, decode(a.filecode, 0, '其它文件', b.filename) FileType"
                        + " from item_file a  left join wf_file b on a.filecode = b.filecode"
                        + " Where a.ItemCode = '{0}' and a.NodeId = '{1}' order by a.nodeid,b.orderno";
            strSql = string.Format(strSql, itemCode, ((int)wfNode).ToString());
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            List<Item_File> fileList = new List<Item_File>();
            while (dr.Read())
            {
                Item_File file = new Item_File();
                file.ItemCode = dr["ItemCode"].ToString();
                file.FileCode = dr["FileCode"].ToString();
                file.FileCodeName = dr["FileType"].ToString();
                file.FileName = dr["FileName"].ToString();
                file.Stage = (ItemStage)EnumHelper.StringValueToEnum(typeof(ItemStage), dr["Stage"].ToString());
                file.NodeId = (WorkFlowNode)EnumHelper.StringValueToEnum(typeof(WorkFlowNode), dr["NodeId"].ToString());
                file.UserId = dr["UserId"].ToString();
                file.UserName = dr["UserName"].ToString();
                file.CreateDate = TypeConvert.Convert<DateTime?>(dr["CreateDate"]);

                fileList.Add(file);
            }
            dr.Close();
            return fileList;
        }
        /// <summary>
        /// 获取项目文件(按多个环节)
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public List<Item_File> GetItemFileByNode(string itemCode, string node)
        {
            string strSql = " select a.*, decode(a.filecode, 0, '其它文件', b.filename) FileType"
                        + " from item_file a  left join wf_file b on a.filecode = b.filecode"
                        + " Where a.ItemCode = '{0}' and a.NodeId in ({1}) order by a.nodeid,a.filecode";
            strSql = string.Format(strSql, itemCode, node);
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            List<Item_File> fileList = new List<Item_File>();
            while (dr.Read())
            {
                Item_File file = new Item_File();
                file.ItemCode = dr["ItemCode"].ToString();
                file.FileCode = dr["FileCode"].ToString();
                file.FileCodeName = dr["FileType"].ToString();
                file.FileName = dr["FileName"].ToString();
                file.Stage = (ItemStage)EnumHelper.StringValueToEnum(typeof(ItemStage), dr["Stage"].ToString());
                file.NodeId = (WorkFlowNode)EnumHelper.StringValueToEnum(typeof(WorkFlowNode), dr["NodeId"].ToString());
                file.UserId = dr["UserId"].ToString();
                file.UserName = dr["UserName"].ToString();
                file.CreateDate = TypeConvert.Convert<DateTime?>(dr["CreateDate"]);

                fileList.Add(file);
            }
            dr.Close();
            return fileList;
        }

        /// <summary>
        /// 获取项目文件(按阶段)
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="stage">多个阶段用逗号分割</param>
        /// <returns></returns>
        public List<Item_File> GetItemFile(string itemCode, string stage)
        {
            string strSql = " select a.*, decode(a.filecode, 0, '其它文件', b.filename) FileType"
                        + " from item_file a left join wf_file b on a.filecode = b.filecode"
                        + " Where a.ItemCode = '{0}' and a.Stage in ({1}) order by a.nodeid,a.filecode";
            strSql = string.Format(strSql, itemCode, stage);
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            List<Item_File> fileList = new List<Item_File>();
            while (dr.Read())
            {
                Item_File file = new Item_File();
                file.ItemCode = dr["ItemCode"].ToString();
                file.FileCode = dr["FileCode"].ToString();
                file.FileCodeName = dr["FileType"].ToString();
                file.FileName = dr["FileName"].ToString();
                file.Stage = (ItemStage)EnumHelper.StringValueToEnum(typeof(ItemStage), dr["Stage"].ToString());
                file.NodeId = (WorkFlowNode)EnumHelper.StringValueToEnum(typeof(WorkFlowNode), dr["NodeId"].ToString());
                file.UserId = dr["UserId"].ToString();
                file.UserName = dr["UserName"].ToString();
                file.CreateDate = TypeConvert.Convert<DateTime?>(dr["CreateDate"]);

                fileList.Add(file);
            }
            dr.Close();
            return fileList;
        }

        /// <summary>
        /// 获取项目文件
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public List<Item_File> GetItemFile(string itemCode, WorkFlowNode wfNode, string fileCode)
        {
            return GetItemFile(itemCode, wfNode, fileCode, string.Empty);
        }

        /// <summary>
        /// 获取项目文件
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public List<Item_File> GetItemFile(string itemCode, WorkFlowNode wfNode, string fileCode, string xh)
        {
            List<object> parameters = new List<object>();
            string strSql = " select a.*, decode(a.filecode, 0, '其它文件', b.filename) FileType"
                        + " from item_file a  left join wf_file b on a.filecode = b.filecode"
                        + " Where a.ItemCode = '{0}' and a.NodeId = '{1}' and a.filecode = '{2}'";
            parameters.Add(itemCode);
            parameters.Add((int)wfNode);
            parameters.Add(fileCode);
            if (!string.IsNullOrEmpty(xh))
            {
                strSql += " and a.xh='{3}'";
                parameters.Add(xh);
            }
            strSql = string.Format(strSql, parameters.ToArray());
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            List<Item_File> fileList = new List<Item_File>();
            while (dr.Read())
            {
                Item_File file = new Item_File();
                file.ItemCode = dr["ItemCode"].ToString();
                file.FileCode = dr["FileCode"].ToString();
                file.FileCodeName = dr["FileType"].ToString();
                file.FileName = dr["FileName"].ToString();
                file.Stage = (ItemStage)EnumHelper.StringValueToEnum(typeof(ItemStage), dr["Stage"].ToString());
                file.NodeId = (WorkFlowNode)EnumHelper.StringValueToEnum(typeof(WorkFlowNode), dr["NodeId"].ToString());
                file.UserId = dr["UserId"].ToString();
                file.UserName = dr["UserName"].ToString();
                file.CreateDate = TypeConvert.Convert<DateTime?>(dr["CreateDate"]);

                fileList.Add(file);
            }
            dr.Close();
            return fileList;
        }

        public List<Item_File> GetItemFileAfterExclude(string itemCode, WorkFlowNode wfNode, List<FileCode> excludeFileCodes)
        {
            string strSql = " select a.*, decode(a.filecode, 0, '其它文件', b.filename) FileType"
                        + " from item_file a  left join wf_file b on a.filecode = b.filecode"
                        + " Where a.ItemCode = '{0}' and a.NodeId = '{1}' and a.filecode not in ({2})";
            string[] fileCodes = (from record in excludeFileCodes select "'" + ((int)record).ToString() + "'").Distinct().ToArray();
            strSql = string.Format(strSql, itemCode, (int)wfNode, string.Join(",", fileCodes));
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            List<Item_File> fileList = new List<Item_File>();
            while (dr.Read())
            {
                Item_File file = new Item_File();
                file.ItemCode = dr["ItemCode"].ToString();
                file.FileCode = dr["FileCode"].ToString();
                file.FileCodeName = dr["FileType"].ToString();
                file.FileName = dr["FileName"].ToString();
                file.Stage = (ItemStage)EnumHelper.StringValueToEnum(typeof(ItemStage), dr["Stage"].ToString());
                file.NodeId = (WorkFlowNode)EnumHelper.StringValueToEnum(typeof(WorkFlowNode), dr["NodeId"].ToString());
                file.UserId = dr["UserId"].ToString();
                file.UserName = dr["UserName"].ToString();
                file.CreateDate = TypeConvert.Convert<DateTime?>(dr["CreateDate"]);

                fileList.Add(file);
            }
            dr.Close();
            return fileList;
        }
        #endregion

        #region 工程信息

        /// <summary>
        /// 获取最新的工程信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public Xm_Gcxx GetItemGcxx(string itemCode)
        {
            Xm_Gcxx gcInfo = null;
            string strSql = "select * from v_xm_gcxx where itemCode = '{0}'";
            strSql = string.Format(strSql, itemCode);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 1)
            {
                gcInfo = (Xm_Gcxx)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_Gcxx));
            }
            return gcInfo;
        }
        /// <summary>
        /// 获取某个阶段的工程信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="stage"></param>
        /// <param name="xh"></param>
        /// <returns></returns>
        public Xm_Gcxx GetItemGcxx(string itemCode, ItemStage stage, string xh)
        {
            Xm_Gcxx gcInfo = null;
            string strSql = "select * from xm_gcxx where itemCode = '{0}' and stage = '{1}' and xh = '{2}'";
            strSql = string.Format(strSql, itemCode, (int)stage, xh);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 1)
            {
                gcInfo = (Xm_Gcxx)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_Gcxx));
            }
            return gcInfo;
        }

        #endregion

        #region 资金信息

        /// <summary>
        /// 获取最新的资金信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public Xm_Xmzj GetItemMoney(string itemCode)
        {
            Xm_Xmzj zjInfo = null;
            string strSql = "select * from v_xm_xmzj where itemcode = '{0}'";
            strSql = string.Format(strSql, itemCode);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 1)
            {
                zjInfo = (Xm_Xmzj)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_Xmzj));
            }
            return zjInfo;
        }

        /// <summary>
        /// 获取某一阶段的资金信息(实体类)
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="nodeId"></param>
        /// <param name="xh"></param>
        /// <returns></returns>
        public Xm_Xmzj GetItemMoney(string itemCode, int nodeId, string xh)
        {
            Xm_Xmzj zjInfo = null;
            string strSql = "select * from xm_xmzj where itemcode = '{0}' and nodeId = '{1}' and xh = '{2}'";
            strSql = string.Format(strSql, itemCode, nodeId, xh);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 1)
            {
                zjInfo = (Xm_Xmzj)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_Xmzj));
            }
            return zjInfo;
        }
        /// <summary>
        /// 获取某一阶段的资金信息(DataTable)
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="nodeId"></param>
        /// <param name="xh"></param>
        /// <returns></returns>
        public DataTable GetItemMoneyDt(string itemCode, int nodeId, string xh)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_xmzj a");
            sbSql.AppendLine("where a.itemcode = '{0}' and a.nodeid = '{1}' and a.xh = '{2}'");
            string strSql = string.Format(sbSql.ToString(), itemCode, nodeId, xh);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 获取项目的资金集合
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public IList<Xm_Xmzj> GetItemMoneyList(string itemCode, string nodeId)
        {
            string strSql = "select * from xm_xmzj Where ItemCode = '{0}' and NodeId in ({1}) order by stage,xh";
            strSql = string.Format(strSql, itemCode, nodeId.Trim());

            DataTable dt = OracleHelper.ExecuteDataTable(strSql);

            List<Xm_Xmzj> dataList = new DataTableOperation().ConvertFromDataTableToEntities<Xm_Xmzj>(dt);

            return dataList;
        }

        #endregion

        #region 单位信息

        /// <summary>
        /// 根据项目编号获取项目单位信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="comType"></param>
        /// <returns></returns>
        public IList<Xm_Xmdw> GetItemCompany(string itemCode)
        {
            IList<Xm_Xmdw> dataList = new List<Xm_Xmdw>();

            string strSql = "select * from xm_xmdw where itemCode = '{0}' order by type";
            strSql = string.Format(strSql, itemCode);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            dataList = new DataTableOperation().ConvertFromDataTableToEntities<Xm_Xmdw>(dt);
            return dataList;
        }
        /// <summary>
        /// 根据项目编号获取项目单位信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="comType"></param>
        /// <returns></returns>
        public Xm_Xmdw GetItemCompany(string itemCode, ItemCompanyType comType)
        {
            Xm_Xmdw company = null;
            string strSql = "select * from xm_xmdw where itemCode = '{0}' and type = '{1}'";
            strSql = string.Format(strSql, itemCode, ((int)comType).ToString());
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                company = (Xm_Xmdw)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_Xmdw));
            }
            return company;
        }

        #endregion

        #region 项目信息
        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public Xm_Xmxx GetItemInfo(string itemCode)
        {
            Xm_Xmxx ItemInfo = null;
            string strSql = "select * from xm_xmxx where itemcode = '{0}'";
            strSql = string.Format(strSql, itemCode);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                ItemInfo = (Xm_Xmxx)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_Xmxx));                
            }
            return ItemInfo;
        }
        /// <summary>
        /// 获取项目流程Id
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string GetItemFlowId(string itemCode)
        {
            string flowId = string.Empty;
            string strSql = "select FlowId from xm_xmxx where itemCode in (" + itemCode + ")";
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                flowId = dt.Rows[0][0].ToString();
            }
            return flowId;
        }

        #endregion


        public Users GetItemUser(string itemCode, WorkFlowNode node)
        {
            Users userInfo = new Users();
            string strSql = "select b.ccode, b.realname, b.telephone from wf_instance a"
                         +" left join users b on a.userid = b.userid"                         
                         +" where a.itemcode = '{0}' and a.nodeid = '{1}' order by a.orderno desc";
            strSql = string.Format(strSql, itemCode, ((int)node).ToString());
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                userInfo.CompanyCode = dt.Rows[0]["ccode"].ToString();
                userInfo.RealName = dt.Rows[0]["realname"].ToString();
                userInfo.TelePhone = dt.Rows[0]["telephone"].ToString();
            }
            return userInfo;
        }

        /// <summary>
        /// 返回该用户在该菜单中所能操作的环节
        /// </summary>
        /// <param name="user"></param>
        /// <param name="funCode"></param>
        /// <returns></returns>
        public string GetNodeIdByLoginUser(LoginUser user, string funCode)
        {
            string nodeId = string.Empty;
            string wfUserSql = string.Empty;
            if (user.CompanyType == CompanyTypeEnum.SHI || user.CompanyType == CompanyTypeEnum.XIAN)
            {
                ////角色条件
                string strRoleWhere = string.Empty;
                string[] roleAry = user.RoleId.Split(new char[] { ',' });
                for (int i = 0; i < roleAry.Length; i++)
                {
                    if (roleAry[i] != string.Empty)
                    {
                        strRoleWhere += string.Format(" or instr(',' || NodeRoleId || ',', ',{0},') <> 0", roleAry[i]);
                    }
                }
                ////部门条件
                string strDeptWhere = string.Empty;
                if (!string.IsNullOrEmpty(user.DepartCode))
                    strDeptWhere = string.Format(" or instr(',' || NodeDepartCode || ',', ',{0},') <> 0", user.DepartCode);

                wfUserSql = "select * from wf_node a left join wf_workflow b on a.flowid = b.flowid"
                       + " where (instr(',' || NodeUserId || ',', ',{0},') <> 0 "
                       + " {1} {2}) and a.functionCode = {3} and b.ccode = '{4}'";
                wfUserSql = string.Format(wfUserSql, user.UserId, strDeptWhere, strRoleWhere, funCode,
                    CommonHelper.GetSHICode(user.CompanyCode));
            }
            DataTable dt = OracleHelper.ExecuteDataTable(wfUserSql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                nodeId += dt.Rows[i]["nodeid"].ToString();
                nodeId += ",";
            }
            if (nodeId != string.Empty) nodeId = nodeId.Substring(0, nodeId.Length - 1);
            return nodeId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Dictionary<string,string> GetNodeByLoginUser(LoginUser user)
        {
            string wfUserSql = string.Empty;
            Dictionary<string, string> nodeList = new Dictionary<string, string>();
            if (user.CompanyType == CompanyTypeEnum.SHI || user.CompanyType == CompanyTypeEnum.XIAN)
            {
                ////角色条件
                string strRoleWhere = string.Empty;
                string[] roleAry = user.RoleId.Split(new char[] { ',' });
                for (int i = 0; i < roleAry.Length; i++)
                {
                    if (roleAry[i] != string.Empty)
                    {
                        strRoleWhere += string.Format(" or instr(',' || NodeRoleId || ',', ',{0},') <> 0", roleAry[i]);
                    }
                }
                ////部门条件
                string strDeptWhere = string.Empty;
                if (!string.IsNullOrEmpty(user.DepartCode))
                    strDeptWhere = string.Format(" or instr(',' || NodeDepartCode || ',', ',{0},') <> 0", user.DepartCode);

                wfUserSql = "select * from wf_node a left join wf_workflow b on a.flowid = b.flowid"
                       + " where (instr(',' || NodeUserId || ',', ',{0},') <> 0 "
                       + " {1} {2}) and b.ccode = '{3}' and a.nodetype = 1";
                wfUserSql = string.Format(wfUserSql, user.UserId, strDeptWhere, strRoleWhere,
                    CommonHelper.GetSHICode(user.CompanyCode));
            }            
            IDataReader dr = OracleHelper.ExecuteReader(wfUserSql);
            while (dr.Read())
            {
                if (!nodeList.ContainsKey(dr["nodeId"].ToString()))
                {
                    nodeList.Add(dr["nodeId"].ToString(), EnumHelper.GetFieldDescription(typeof(WorkFlowNode), int.Parse(dr["nodeId"].ToString())));
                }
            }
            dr.Close();
            return nodeList;
        }

        /// <summary>
        /// 获取项目集合
        /// </summary>
        /// <param name="cCode"></param>
        /// <param name="nodeId"></param>
        /// <param name="wfState"></param>
        /// <returns></returns>
        public DataTable GetItemData(string cCode, string nodeId, string wfState)
        {
            string shortCode = CommonHelper.GetShortCode(cCode);

            OracleParameter oraShortcCode = new OracleParameter("ShortcCode", OracleType.VarChar, 20);
            oraShortcCode.Value = shortCode;
            OracleParameter oraNodeId = new OracleParameter("NodeId", OracleType.VarChar, 50);
            oraNodeId.Value = nodeId;
            OracleParameter oraWfState = new OracleParameter("WfState", OracleType.VarChar, 20);
            oraWfState.Value = wfState;
            OracleParameter returnCursor = new OracleParameter("ReturnCursor", OracleType.Cursor);
            returnCursor.Direction = ParameterDirection.Output;

            OracleParameter[] oracleParameters = { oraShortcCode, oraNodeId, oraWfState, returnCursor };

            DataTable dt = OracleHelper.ExecuteDataTable("QueryPackages.ItemWorkQuery", oracleParameters);

            return dt;
        }

        /// <summary>
        /// 获取项目集合(备选、申报)
        /// </summary>
        /// <param name="cCode"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public DataTable GetItemData(string cCode, string nodeId)
        {
            string strSql = string.Empty;
            strSql = "select a.*,b.zjze,c.gm_zgm from xm_xmxx a "
                +" left join v_xm_xmzj b on a.itemcode = b.itemcode"
                +" left join v_xm_gcxx c on a.itemcode = c.itemcode"
                +" where a.ccode = {0} and  itemstate = 0 and a.nodeid = {1} order by itemCode";
            strSql = string.Format(strSql, cCode, nodeId);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public DataTable GetItemData()
        {
            string strSql = string.Empty;
            strSql = "select a.*,b.zjze,c.gm_zgm from xm_xmxx a "
                + " left join v_xm_xmzj b on a.itemcode = b.itemcode"
                + " left join v_xm_gcxx c on a.itemcode = c.itemcode"
                + " where itemstate = 0 order by itemCode";
            //strSql = string.Format(strSql, cCode, nodeId);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 获取用户的待办工作列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public DataTable GetWaitTask(string UserId,string NodeId)
        {
            OracleParameter oraUserId = new OracleParameter("UserId", OracleType.VarChar, 20);
            oraUserId.Value = UserId;

            OracleParameter oraNodeId = new OracleParameter("NodeId", OracleType.VarChar, 20);
            oraNodeId.Value = NodeId;

            OracleParameter returnCursor = new OracleParameter("ReturnCursor", OracleType.Cursor);
            returnCursor.Direction = ParameterDirection.Output;

            OracleParameter[] oracleParameters = { oraUserId, oraNodeId, returnCursor };

            DataTable dt = OracleHelper.ExecuteDataTable("QueryPackages.UserWorkWait", oracleParameters);

            return dt;
        }

        /// <summary>
        /// 获取用户的已办工作列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public DataTable GetAlreadyTask(string UserId,ItemState itemState,string beginDate, string endDate)
        {
            OracleParameter oraUserId = new OracleParameter("UserId", OracleType.VarChar, 20);
            oraUserId.Value = UserId;

            OracleParameter oraItemState = new OracleParameter("ItemState", OracleType.VarChar, 20);
            oraItemState.Value = ((int)itemState).ToString();

            OracleParameter oraBeginDate = new OracleParameter("BeginDate", OracleType.VarChar, 20);
            oraBeginDate.Value = beginDate;

            OracleParameter oraEndDate = new OracleParameter("EndDate", OracleType.VarChar, 20);
            oraEndDate.Value = DateTime.Parse(endDate).AddDays(1).ToString("yyyy-MM-dd");

            OracleParameter returnCursor = new OracleParameter("ReturnCursor", OracleType.Cursor);
            returnCursor.Direction = ParameterDirection.Output;

            OracleParameter[] oracleParameters = { oraUserId, oraItemState,oraBeginDate,oraEndDate, returnCursor };

            DataTable dt = OracleHelper.ExecuteDataTable("QueryPackages.UserWorkAlready", oracleParameters);

            return dt;
        }
        
        /// <summary>
        /// 标记已读
        /// </summary>
        /// <param name="itemCode"></param>
        public void ItemRead(string itemCode)
        {                        
            OracleHelper.ExecuteCommand("update xm_xmxx set read = 1 where itemCode in (" + itemCode + ")");
        }  


        /// <summary>
        /// 是否存在相应环节的审批记录
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool HaveSplist(string itemCode, WorkFlowNode node)
        {
            string strSql = " select * from wf_instance where itemcode = '{0}' and state = 1 and nodeid = {1}";
            strSql = string.Format(strSql, itemCode, ((int)node).ToString());
            return OracleHelper.ExecuteDataTable(strSql).Rows.Count > 0;
        }
        /// <summary>
        /// 是否该环节的审批信息
        /// </summary>
        /// <param name="itemCode">项目编号</param>
        /// <param name="NodeId">当前环节</param>
        /// <returns></returns>
        public bool IsShowSpList(string itemCode,int NodeId)
        {
            bool show = false;
            string strSql = "select * from (select * from wf_instance where "
            + " itemcode = '{0}' and state = 1 order by orderno desc) where rownum = 1";
            strSql = string.Format(strSql, itemCode);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 1)
            {
                show = int.Parse(dt.Rows[0]["PerNode"].ToString()) == NodeId;
            }
            return show;
        }
        
        
        /// <summary>
        /// 保存项目文件
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="nodeId"></param>
        /// <param name="itemFileList"></param>
        /// <returns></returns>
        public void SaveItemFile(string itemCode, WorkFlowNode node, List<Item_File> itemFileList, ref ArrayList strSql)
        {
            string tmpSql = string.Empty;

            //string[] fileCodeAry = (from item in itemFileList select item.FileCode).Distinct().ToArray();

            tmpSql = "delete from item_file where itemcode in ({0}) and nodeid = '{1}'";
            tmpSql = string.Format(tmpSql, itemCode.Trim(), ((int)node).ToString());

            //if (fileCodeAry.Length > 0) tmpSql += " and filecode in (" + string.Join(",", fileCodeAry) + ")";
            if (node == WorkFlowNode.ShiShi)
                tmpSql += string.Format(" and filecode not in ({0},{1})",
                    ((int)FileCode.变更申请书).ToString(), ((int)FileCode.变更批复文件).ToString(),
                    ((int)FileCode.预算补充文件).ToString());

            strSql.Add(tmpSql);

            foreach (Item_File itemFile in itemFileList)
            {
                tmpSql = "insert into item_file(itemcode,filecode,filename,stage,nodeid,xh,userid,userName,createdate)"
                    + " Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',to_date('{8}','yyyy-mm-dd hh24:mi:ss'))";
                tmpSql = string.Format(tmpSql, itemFile.ItemCode, itemFile.FileCode, itemFile.FileName,
                    ((int)itemFile.Stage).ToString(), ((int)itemFile.NodeId).ToString(), itemFile.XH,
                    itemFile.UserId, itemFile.UserName, itemFile.CreateDate);
                strSql.Add(tmpSql);
            }   
        }

        /// <summary>
        /// 保存项目单位信息
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public void SaveItemCompany(string itemCode, ItemCompanyType comType, Xm_Xmdw itemCompany, ref ArrayList strSql)
        {
            string tmpSql = string.Empty;

            tmpSql = "delete from xm_xmdw where itemCode in ({0}) and type = {1}";
            tmpSql = string.Format(tmpSql, itemCode, ((int)comType).ToString());
            strSql.Add(tmpSql);
            if (itemCompany != null)
            {
                tmpSql = "insert into xm_xmdw (itemcode, type, name, code, linkphone, linkman)"
                  + " values ('{0}','{1}','{2}','{3}','{4}','{5}')";
                tmpSql = string.Format(tmpSql, itemCompany.ItemCode, ((int)itemCompany.Type).ToString(),
                    itemCompany.Name, itemCompany.Code, itemCompany.LinkPhone, itemCompany.LinkMan);
                strSql.Add(tmpSql);
            }
        }
        /// <summary>
        /// 保存项目单位信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="companyList"></param>
        /// <param name="strSql"></param>
        public void SaveItemCompany(string itemCode, List<Xm_Xmdw> companyList, ref ArrayList strSql)
        {
            string companyType = string.Join(",",(from record in companyList select ((int)record.Type).ToString()).Distinct().ToArray());

            string tmpSql = string.Empty;

            tmpSql = "delete from xm_xmdw where itemCode in ({0}) and type in ({1})";
            tmpSql = string.Format(tmpSql, itemCode, companyType);
            strSql.Add(tmpSql);
            foreach(Xm_Xmdw itemCompany in companyList)
            {
                tmpSql = "insert into xm_xmdw (itemcode, type, name, code, linkphone, linkman)"
                  + " values ('{0}','{1}','{2}','{3}','{4}','{5}')";
                tmpSql = string.Format(tmpSql, itemCompany.ItemCode, ((int)itemCompany.Type).ToString(),
                    itemCompany.Name, itemCompany.Code, itemCompany.LinkPhone, itemCompany.LinkMan);
                strSql.Add(tmpSql);
            }
        }

        /// <summary>
        /// 保存项目单位信息
        /// </summary>
        /// <param name="itemCompany"></param>
        /// <returns></returns>
        public bool SaveItemCompany(Xm_Xmdw itemCompany)
        {
            ArrayList strSql = new ArrayList();
            string tmpSql = string.Empty;
            if (itemCompany != null)
            {
                tmpSql = "delete from xm_xmdw where itemCode in ({0}) and type = {1}";
                tmpSql = string.Format(tmpSql, itemCompany.ItemCode, ((int)itemCompany.Type).ToString());
                strSql.Add(tmpSql);                 
                strSql.Add(SqlBuilder.BuildInsertSql(itemCompany));
                return OracleHelper.ExecuteCommand(strSql);
            }
            return false;
        }
        
        /// <summary>
        /// 保存项目资金
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="node"></param>
        /// <param name="itemZJ"></param>
        /// <returns></returns>
        public void SaveItemMoney(string itemCode, WorkFlowNode node, Xm_Xmzj itemZJ, ref ArrayList strSql)
        {
            string tmpSql = string.Empty;

            tmpSql = "delete from xm_xmzj where itemCode in ({0}) and nodeId = '{1}'";
            tmpSql = string.Format(tmpSql, itemCode.Trim(), ((int)node).ToString());
            strSql.Add(tmpSql);
            if (itemZJ != null)
            {
                strSql.Add(SqlBuilder.BuildInsertSql(itemZJ));
                //tmpSql = "insert into xm_xmzj(itemcode,stage,nodeid,xh,zjze,sbgzf,tdpzgcf,ntslgcf,dlgcf,qtgcf,bkyjf,qtfy,fee1,fee2,fee3,fee4,fee5,fee6,fee7,fee8)"
                //           + " Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
                //tmpSql = string.Format(tmpSql, itemZJ.ItemCode, ((int)itemZJ.Stage).ToString(), ((int)itemZJ.NodeId).ToString(), itemZJ.Xh, itemZJ.Zjze,
                //    itemZJ.Sbgzf, itemZJ.Tdpzgcf, itemZJ.Ntslgcf, itemZJ.Dlgcf, itemZJ.Qtgcf, itemZJ.Bkyjf, itemZJ.Qtfy);
                //strSql.Add(tmpSql);
            }
        }

        /// <summary>
        /// 项目工程量
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="node"></param>
        /// <param name="itemGcxx"></param>
        /// <param name="strSql"></param>
        public void SaveItemGcxx(string itemCode, Xm_Gcxx itemGcxx, ref ArrayList strSql)
        {
            string tmpSql = string.Empty;

            tmpSql = "delete from xm_gcxx where itemCode in ({0}) and Stage = '{1}' and Xh = '{2}'";
            tmpSql = string.Format(tmpSql, itemCode.Trim(), ((int)itemGcxx.Stage).ToString(), itemGcxx.Xh);
            strSql.Add(tmpSql);

            tmpSql = SqlBuilder.BuildInsertSql(itemGcxx);
            strSql.Add(tmpSql);

        }

        public bool ExecuteCommand(ArrayList strSql)
        {
            return OracleHelper.ExecuteCommand(strSql);
        }

        /// <summary>
        /// 是否可以上传(文件)
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool IsUpload(string itemCode, string fileName)
        {
            string strSql = "select count(*) from item_file where itemCode in ({0}) and FileName = '{1}'";
            strSql = string.Format(strSql, itemCode, fileName);
            return OracleHelper.ExecuteDataTable(strSql).Rows[0][0].ToString().Equals("0");
        }

        public string GetWfResult(string itemCode, WorkFlowNode node)
        {
            string result = string.Empty;
            string strSql = "select * from wf_instance where itemCode  = '{0}' and nodeid = {1} and state = 1 order by orderno desc";
            strSql = string.Format(strSql, itemCode, ((int)node).ToString());
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["ResultDesc"].ToString();
            }
            return result;
        }

        /// <summary>
        /// 判断项目编号在某一环节下是否存在审批记录
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="itemCode"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool HasSpRecord(string flowId, string itemCode, WorkFlowNode node)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat("select * from wf_instance where flowid = '{0}' and itemcode = '{1}' and nodeid = {2} and state = 1",
                flowId, itemCode, ((int)node).ToString());
            return OracleHelper.ExecuteDataTable(sbSql.ToString()).Rows.Count > 0;
        }

        #region GIS数据操作
        /// <summary>
        /// 保存项目地块信息
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public bool SaveDkInfo(string gisFold, List<Xm_Dkxx> dataList, ItemStage itemStage, string Xh)
        {
            if (!CommonManage.GisEnable)
            {
                return true;
            }
            string tmpSql = string.Empty;
            tmpSql = "delete from xm_dkxx where itemCode = '{0}' and stage = '{1}' and xh = '{2}'";
            tmpSql = string.Format(tmpSql, dataList[0].ItemCode, ((int)itemStage).ToString(), Xh);
            if (!OracleHelper.ExecuteCommand(tmpSql)) return false;

            string dkId = string.Empty;
            foreach (Xm_Dkxx dkInfo in dataList)
            {
                ArrayList strSql = new ArrayList();

                tmpSql = "select '{0}' || lpad(nvl(max(replace(dkid, '{0}', '')), 0) + 1, 4, '0') from xm_dkxx where itemcode = '{0}'";
                tmpSql = string.Format(tmpSql, dkInfo.ItemCode);
                dkId = OracleHelper.ExecuteDataTable(tmpSql).Rows[0][0].ToString();

                tmpSql = "insert into xm_dkxx (dkid, itemcode, stage, xh, dkbh, dkmc, tfh, scdkmj, jsdkmj, txlx, qsxz, jzds, thjb, dlbm, dlmc, ysid,zbx,jdfd,tylx,jldw,dh,jd,zhcs)"
                + "Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')";
                tmpSql = string.Format(tmpSql, dkId, dkInfo.ItemCode, ((int)itemStage).ToString(), Xh,
                    dkInfo.Dkbh, dkInfo.Dkmc, dkInfo.Tfh, dkInfo.Scdkmj, dkInfo.Jsdkmj, dkInfo.Txlx,
                    dkInfo.Qsxz, dkInfo.Jzds, dkInfo.Thjb, dkInfo.Dlbm, dkInfo.Dlmc, dkInfo.Ysid, dkInfo.Zbx, dkInfo.Jdfd, dkInfo.Tylx,
                    dkInfo.Jldw, dkInfo.Dh, dkInfo.Jd, dkInfo.Zhcs);
                strSql.Add(tmpSql);
                int i = 1;
                foreach (Xm_Jzd jzdInfo in dkInfo.JzdList)
                {
                    tmpSql = "insert into xm_dkjzd (dkid, xh, jzdbh, dkqh, x, y) values ('{0}','{1}','{2}','{3}','{4}','{5}')";
                    tmpSql = string.Format(tmpSql, dkId, i.ToString(), jzdInfo.JzdBh, jzdInfo.Dkqh, jzdInfo.X, jzdInfo.Y);
                    strSql.Add(tmpSql);
                    i++;
                }
                if (!OracleHelper.ExecuteCommand(strSql)) return false;
            }
            try
            {
                List<Xm_Dkxx> dkAllList = this.getDkInfoByItemCode(dataList[0].ItemCode);
                this.CreatXmlData(gisFold, dataList[0].ItemCode, dkAllList);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 保存项目地块信息(实施阶段)
        /// </summary>
        /// <param name="itemCode"></param>
        public void SaveDkInfo(string itemCode)
        {
            string gisFold = string.Format("{0}/{1}",
                   EnumHelper.GetFieldDescription(typeof(FileFolder), (int)FileFolder.Gis), "Query");
            string strSql = string.Empty;
            strSql = "select count(*) from xm_dkxx where ItemCode = '{0}' and Stage = '{1}'";
            strSql = string.Format(strSql, itemCode, ((int)ItemStage.ShiShi).ToString());
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows[0][0].ToString().Equals("0"))
            {
                List<Xm_Dkxx> dataList = this.getDkInfo(itemCode, (int)ItemStage.KeYan, 1);
                this.SaveDkInfo(gisFold, dataList, ItemStage.ShiShi, "1");
            }
        }

        public List<Xm_Dkxx> getDkInfo(string itemCode, int itemStage, int xh)
        {
            List<Xm_Dkxx> dataList = new List<Xm_Dkxx>();
            string strSql = "select * from xm_dkxx where ItemCode = '{0}' and Stage = '{1}' and Xh = '{2}' order by dkid";
            strSql = string.Format(strSql, itemCode, itemStage.ToString(), xh.ToString());
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                this.fillDkInfo(dr, ref dataList);
            }
            dr.Close();
            return dataList;
        }

        public List<Xm_Dkxx> getDkInfoByItemCode(string itemCode)
        {
            List<Xm_Dkxx> dataList = new List<Xm_Dkxx>();
            string strSql = "select * from ("
            + " select * from xm_dkxx where ItemCode = '{0}' and stage in ({1},{3}) union "
            + " select * from xm_dkxx where ItemCode = '{0}' and stage = {2} and xh "
            + " in (select max(xh)  from xm_dkxx where ItemCode = '{0}' and stage = {2} )) order by stage,dkid";
            strSql = string.Format(strSql, itemCode, ((int)ItemStage.KeYan).ToString(),
                ((int)ItemStage.ShiShi).ToString(), ((int)ItemStage.YanShou).ToString());
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                this.fillDkInfo(dr, ref dataList);
            }
            dr.Close();
            return dataList;
        }

        public List<Xm_Dkxx> getDkInfoByDkId(string dkId)
        {
            List<Xm_Dkxx> dataList = new List<Xm_Dkxx>();
            string strSql = "select * from xm_dkxx where dkId = '{0}' order by dkid";
            strSql = string.Format(strSql, dkId);
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                this.fillDkInfo(dr, ref dataList);
            }
            dr.Close();
            return dataList;
        }

        public List<Xm_Jzd> getJzdInfo(string dkId)
        {
            List<Xm_Jzd> dataList = new List<Xm_Jzd>();
            string strSql = "select * from xm_dkjzd where dkid = '{0}' order by xh";
            strSql = string.Format(strSql, dkId);
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                Xm_Jzd jzdInfo = new Xm_Jzd();
                jzdInfo.Dkid = dr["Dkid"].ToString();
                jzdInfo.JzdBh = dr["JzdBh"].ToString();
                jzdInfo.Dkqh = dr["Dkqh"].ToString();
                jzdInfo.X = dr["X"].ToString();
                jzdInfo.Y = dr["Y"].ToString();
                dataList.Add(jzdInfo);
            }
            dr.Close();
            return dataList;
        }

        private void fillDkInfo(IDataReader dr, ref List<Xm_Dkxx> dataList)
        {
            Xm_Dkxx dkInfo = new Xm_Dkxx();
            dkInfo.Dkid = dr["Dkid"].ToString();
            dkInfo.ItemCode = dr["ItemCode"].ToString();
            dkInfo.Stage = (ItemStage)EnumHelper.StringValueToEnum(typeof(ItemStage), dr["stage"].ToString());
            dkInfo.Xh = dr["Xh"].ToString();
            dkInfo.Dkbh = dr["Dkbh"].ToString();
            dkInfo.Dkmc = dr["Dkmc"].ToString();
            dkInfo.Tfh = dr["Tfh"].ToString();
            dkInfo.Scdkmj = dr["Scdkmj"].ToString();
            dkInfo.Jsdkmj = dr["Jsdkmj"].ToString();
            dkInfo.Txlx = dr["Txlx"].ToString();
            dkInfo.Qsxz = dr["Qsxz"].ToString();
            dkInfo.Jzds = dr["Jzds"].ToString();
            dkInfo.Thjb = dr["Thjb"].ToString();
            dkInfo.Dlbm = dr["Dlbm"].ToString();
            dkInfo.Dlmc = dr["Dlmc"].ToString();
            dkInfo.Ysid = dr["Ysid"].ToString();
            dkInfo.Zbx = dr["Zbx"].ToString();
            dkInfo.Jdfd = dr["Jdfd"].ToString();
            dkInfo.Tylx = dr["Tylx"].ToString();
            dkInfo.Jldw = dr["Jldw"].ToString();
            dkInfo.Dh = dr["Dh"].ToString();
            dkInfo.Jd = dr["Jd"].ToString();
            dkInfo.Zhcs = dr["Zhcs"].ToString();
            dkInfo.JzdList = this.getJzdInfo(dkInfo.Dkid);
            dataList.Add(dkInfo);
        }    

        /// <summary>
        /// 获取最新的地块数据
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public List<Xm_Dkxx> getLastDkInfo(string itemCode)
        {
            List<Xm_Dkxx> dataList = new List<Xm_Dkxx>();
            string strSql = "select * from v_xm_dkxx where itemCode = '{0}'";

            strSql = string.Format(strSql, itemCode);

            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                this.fillDkInfo(dr, ref dataList);
            }
            dr.Close();
            return dataList;
        }

        public string[] getLastDkStage(string itemCode)
        {
            string[] returnAry = new string[2];
            returnAry[0] = ((int)ItemStage.KeYan).ToString();
            returnAry[1] = "1";
            string strSql = "select * from v_xm_dkxx where itemcode = '{0}'";
            strSql = string.Format(strSql, itemCode.Trim());
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                returnAry[0] = dt.Rows[0]["stage"].ToString();
                returnAry[1] = dt.Rows[0]["xh"].ToString();
            }
            return returnAry;
        } 


        #region 生成Xml文件
        /// <summary>
        /// 生成Xml文件
        /// </summary>
        public void CreatXmlData(string gisFold, string itemCode, List<Xm_Dkxx> dataList)
        {
            string FolderUrl = HttpContext.Current.Server.MapPath(gisFold);

            if (!System.IO.Directory.Exists(FolderUrl))
            {
                Directory.CreateDirectory(FolderUrl);
            }
            ////获取项目数据
            Xm_Xmxx itemData = this.GetItemInfo(itemCode);

            string fileUrl = FolderUrl + "\\" + itemCode + ".xml";

            string rootNode = "gbxm";

            XmlHelper.CreateXmlDocument(fileUrl, rootNode);

            Dictionary<string, string> node = new Dictionary<string, string>();
            node.Add("xmbh", itemCode);
            node.Add("xmmc", itemData.ItemName);
            node.Add("xzqh", itemData.Ccode);
            //node.Add("xzmc", itemData.Sbdw);

            XmlHelper.InsertNode(fileUrl, rootNode, node);

            ////插入三个阶段的数据
            this.InsertStageNode(fileUrl, rootNode, ItemStage.KeYan, dataList);
            this.InsertStageNode(fileUrl, rootNode, ItemStage.ShiShi, dataList);
            this.InsertStageNode(fileUrl, rootNode, ItemStage.YanShou, dataList);

        }

        /// <summary>
        /// 插入阶段数据的XML结点
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <param name="rootNode"></param>
        /// <param name="stage"></param>
        /// <param name="dataList"></param>
        private void InsertStageNode(string fileUrl, string rootNode, ItemStage stage, List<Xm_Dkxx> dataList)
        {
            var stageList = from item in dataList where item.Stage == stage select item;
            if (stageList.Count<Xm_Dkxx>() == 0) return;

            string stageName = this.changeName(stage);

            Dictionary<string, string> paraNode = new Dictionary<string, string>();
            paraNode.Add("zbx", dataList[0].Zbx);
            paraNode.Add("jdfd", dataList[0].Jdfd);
            paraNode.Add("tylx", dataList[0].Tylx);
            paraNode.Add("jldw", dataList[0].Jldw);
            paraNode.Add("dh", dataList[0].Dh);
            paraNode.Add("jd", dataList[0].Jd);
            paraNode.Add("zhzbcs", dataList[0].Zhcs);

            XmlHelper.InsertNode(fileUrl, rootNode, stageName, null, paraNode);
            int i = 1;
            foreach (Xm_Dkxx DK in stageList)
            {
                Dictionary<string, string> dkAttr = new Dictionary<string, string>();
                dkAttr.Add("id", i.ToString());
                Dictionary<string, string> dkSubNode = new Dictionary<string, string>();
                dkSubNode.Add("dkbh", DK.Dkbh);
                dkSubNode.Add("dkmc", DK.Dkmc);
                dkSubNode.Add("dkmj", DK.Jsdkmj);
                dkSubNode.Add("dkds", DK.Jzds);
                dkSubNode.Add("dktf", DK.Tfh);

                XmlHelper.InsertNode(fileUrl, string.Format("/{0}/{1}", rootNode, stageName), "dk", dkAttr, dkSubNode);

                ////查询地块中的包含区域的个数
                var QhList = (from item in DK.JzdList select item.Dkqh).Distinct();
                foreach (string qh in QhList)
                {
                    var JzdList = from item in DK.JzdList where item.Dkqh == qh select item;
                    Dictionary<string, string> JzqyNode = new Dictionary<string, string>();
                    JzqyNode.Add("qybh", qh);
                    Dictionary<string, string> JzqyAttributes = new Dictionary<string, string>();
                    JzqyAttributes.Add("id", qh);
                    XmlHelper.InsertNode(fileUrl, string.Format("/{0}/{1}/dk[@id='{2}']", rootNode, stageName, i.ToString()), "jzqy", JzqyAttributes, JzqyNode);
                    foreach (Xm_Jzd jzd in JzdList)
                    {
                        Dictionary<string, string> JzdNode = new Dictionary<string, string>();
                        JzdNode.Add("jzdbh", jzd.JzdBh);
                        JzdNode.Add("jzdjd", jzd.X);
                        JzdNode.Add("jzdwd", jzd.Y);
                        XmlHelper.InsertNode(fileUrl, string.Format("/{0}/{1}/dk[@id='{2}']/jzqy[@id='{3}']", rootNode, stageName, i.ToString(), qh), "jzd", null, JzdNode);
                    }
                }
                i++;
            }

        }

        private string changeName(ItemStage stage)
        {
            switch (stage)
            {
                case ItemStage.KeYan:
                    return "lxdkjh";
                case ItemStage.ShiShi:
                    return "ssdkjh";
                case ItemStage.YanShou:
                    return "ysdkjh";
                default:
                    return string.Empty;
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// 绑定流程信息(意见)
        /// </summary>
        /// <param name="wfList"></param>
        /// <param name="textContorl"></param>
        /// <param name="wfNode"></param>
        public void BindWfContorl(IList<Wfinstance> wfList, TextBox textContorl, WorkFlowNode wfNode)
        {
            var wQuery = (from item in wfList
                          where item.NodeId == ((int)wfNode).ToString() ////&& item.State == "1"
                          orderby item.Orderno descending
                          select item);
            if (wQuery.Count<Wfinstance>() != 0)
            {
                textContorl.Text = wQuery.First().ResultDesc;
            }
        }

        /// <summary>
        /// 绑定单位信息
        /// </summary>
        /// <param name="dwList"></param>
        /// <param name="textContorl"></param>
        /// <param name="comType"></param>
        public void BindCompanyContorl(IList<Xm_Xmdw> dwList, TextBox textContorl, ItemCompanyType comType)
        {
            var cQuery = (from item in dwList where item.Type == comType select item);
            if (cQuery.Count<Xm_Xmdw>() != 0)
            {
                textContorl.Text = cQuery.First().Name;
            }
        }

        public DataTable QueryXmdw(string itemCode, ItemCompanyType type)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_xmdw a");
            sbSql.AppendLine("where a.itemcode = '{0}' and a.type = '{1}'");
            string strSql = string.Format(sbSql.ToString(), itemCode, (int)type);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public string[] GetGisStage(string itemCode)
        {
            string[] aryValue = new string[2];            
            string strSql = "select stage,max(xh) from gis_data where itemcode = '{0}' group by stage order by stage";
            strSql = string.Format(strSql, itemCode);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                aryValue[0] += dt.Rows[i][0].ToString().Trim() + ",";
                aryValue[1] += dt.Rows[i][1].ToString().Trim() + ",";
            }
            if (!string.IsNullOrEmpty(aryValue[0])) aryValue[0] = aryValue[0].Substring(0, aryValue[0].Length - 1);
            if (!string.IsNullOrEmpty(aryValue[1])) aryValue[1] = aryValue[1].Substring(0, aryValue[1].Length - 1);
            return aryValue;
        }
    }
}
