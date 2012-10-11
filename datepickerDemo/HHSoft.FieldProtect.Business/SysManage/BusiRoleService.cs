using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.DataEntity.SysManage;
using System.Data.OracleClient;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.Framework.Control;
using HHSoft.FieldProtect.Framework.Utility;
using System.Collections;
using HHSoft.FieldProtect.DataEntity.WorkFlow;
using HHSoft.FieldProtect.Business.Common;

namespace HHSoft.FieldProtect.Business.SysManage
{
    public class BusiRoleService
    {
        public BusiRoleService()
        {
        }

        public IList<Function> GetFunction(string roleId, bool isSystem)
        {
            string strSql = string.Empty;
            if (isSystem)
            {
                strSql = "select * from function where Instr(FunctionLevel,'{0}') <> 0  order by functioncode";
            }
            else
            {
                strSql = "select b.* from roleandfunction a inner join  function b"
                       + " on a.functioncode = b.functioncode where a.roleid in ({0})";
                strSql = string.Format(strSql, roleId.Trim());
            }
            OracleDataReader dr = OracleHelper.ExecuteReader(strSql);
            IList<Function> datalist = new List<Function>();
            while (dr.Read())
            {
                Function function = new Function();
                function.FunctionCode = dr["FunctionCode"].ToString();
                function.FunctionName = dr["FunctionName"].ToString();
                function.FunctionUrl = dr["FunctionUrl"].ToString();
                function.OrderNo = int.Parse(dr["OrderNo"].ToString());
                datalist.Add(function);
            }
            dr.Close();
            return datalist;
        }
        /// <summary>
        /// 按行政级别获取可分配菜单对象
        /// </summary>
        /// <param name="comType"></param>
        /// <returns></returns>
        public IList<Function> GetFunctionByLevel(CompanyTypeEnum comType)
        {
            string strSql = "select * from function where Instr(FunctionLevel,'{0}') <> 0  "
              + " and substr(functioncode,0,2)<>20  order by functioncode";
            strSql = string.Format(strSql, ((int)comType).ToString());

            OracleDataReader dr = OracleHelper.ExecuteReader(strSql);
            IList<Function> datalist = new List<Function>();
            while (dr.Read())
            {
                Function function = new Function();
                function.FunctionCode = dr["FunctionCode"].ToString();
                function.FunctionName = dr["FunctionName"].ToString();
                function.FunctionUrl = dr["FunctionUrl"].ToString();
                //function.IsFristPage = dr["FirstPage"].ToString().Equals("1");
                function.OrderNo = int.Parse(dr["OrderNo"].ToString());
                datalist.Add(function);
            }
            dr.Close();
            return datalist;
        }
        /// <summary>
        /// 根据登录用户获取菜单权限。
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IList<Function> GetFunction(LoginUser user)
        {
            IList<Function> datalist = new List<Function>();

            string strSql = string.Empty;
            //// 1 所有人默认菜单(个人管理)
            string allUserSql = "select * from function where Instr(FunctionLevel,'{0}') <> 0 and FunctionCode like '20%'";
            allUserSql = string.Format(allUserSql, ((int)user.CompanyType).ToString());

            //// 2 系统管理员
            string sysUserSql = string.Empty;
            if (user.RoleKey.Contains("SYSMANAGE"))
            {
                sysUserSql = "select * from function where Instr(FunctionLevel,'{0}') <> 0";
                sysUserSql = string.Format(sysUserSql, ((int)user.CompanyType).ToString());
            }

            //// 3 根据角色配置的菜单
            string roleUserSql = string.Empty;
            if (user.RoleId != string.Empty)
            {
                roleUserSql = "select b.* from roleandfunction a inner join  function b on a.functioncode = b.functioncode where a.roleid  in ({0})";
                roleUserSql = string.Format(roleUserSql, user.RoleId);
            }

            //// 4 流程用户(只针对市县用户)
            string wfUserSql = string.Empty;

            if (user.CompanyType == CompanyTypeEnum.SHI || user.CompanyType == CompanyTypeEnum.XIAN)
            {
                string nodeStr = string.Empty;

                List<WfNode> nodeList = this.GetNodeListByUser(user);

                var beginNode = from item in nodeList where item.WorkFlowNode == WorkFlowNode.TB select item;

                if (CommonManage.SystemStyle == SystemStyle.Stage)
                {
                    if (nodeList.Count > 0)
                    {
                        nodeStr = string.Join(",", (from item in nodeList
                                                    where item.FunctionCode != string.Empty
                                                    select item.FunctionCode).ToArray());
                        if (beginNode.Count<WfNode>() > 0)
                        {
                            nodeStr += ",3010";
                        }
                        wfUserSql = string.Format("select * from function where FunctionCode in (30,{0})", nodeStr);
                    }
                }

                if (CommonManage.SystemStyle == SystemStyle.WorkFlow)
                {
                    if (beginNode.Count<WfNode>() > 0)
                    {
                        nodeStr = ",3018";
                    }
                    wfUserSql = string.Format("select * from function where FunctionCode in (30,3019,3020,3021{0})", nodeStr);

                }
            }

            if (allUserSql != string.Empty) strSql += allUserSql + " union ";
            if (sysUserSql != string.Empty) strSql += sysUserSql + " union ";
            if (roleUserSql != string.Empty) strSql += roleUserSql + " union ";
            if (wfUserSql != string.Empty) strSql += wfUserSql + " union ";

            if (strSql != string.Empty) strSql = strSql.Substring(0, strSql.Length - 6);

            OracleDataReader dr = OracleHelper.ExecuteReader(strSql);

            while (dr.Read())
            {
                Function function = new Function();
                function.FunctionCode = dr["FunctionCode"].ToString();
                function.FunctionName = dr["FunctionName"].ToString();
                function.FunctionUrl = dr["FunctionUrl"].ToString();
                function.IsFristPage = dr["FristPage"].Equals("1");
                function.OrderNo = int.Parse(dr["OrderNo"].ToString());
                datalist.Add(function);
            }
            dr.Close();
            return datalist;
        }

        /// <summary>
        /// 获取登录用户的环节权限
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<WfNode> GetNodeListByUser(LoginUser user)
        {
            List<WfNode> nodeList = new List<WfNode>();

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
            {
                strDeptWhere = string.Format(" or instr(',' || NodeDepartCode || ',', ',{0},') <> 0", user.DepartCode);
            }

            string strSql = "select * from wf_node where instr(',' || NodeUserId || ',', ',{0},') <> 0 "
                   + " {1} {2} ";
            strSql = string.Format(strSql, user.UserId, strDeptWhere, strRoleWhere);

            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                WfNode node = new WfNode();
                node.NodeId = dr["nodeId"].ToString();
                node.NodeType = (NodeType)EnumHelper.StringValueToEnum(typeof(NodeType), dr["nodeType"].ToString());
                node.FunctionCode = dr["FunctionCode"].ToString();
                nodeList.Add(node);
            }
            dr.Close();
            return nodeList;
        }

        public Function GetFunctionById(string functionCode)
        {
            Function function = new Function();
            string strSql = "select * from function where FunctionCode = {0}";
            strSql = string.Format(strSql, functionCode);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                function.FunctionCode = dt.Rows[0]["FunctionCode"].ToString();
                function.FunctionName = dt.Rows[0]["FunctionName"].ToString();
                function.FunctionUrl = dt.Rows[0]["FunctionUrl"].ToString();
                function.OrderNo = int.Parse(dt.Rows[0]["OrderNo"].ToString());
            }
            return function;
        }

        public bool ManageRoles(Role role)
        {
            string[] strSql = null;
            switch (role.Action)
            {
                case ActionEnum.Insert:
                    strSql = new string[1];
                    strSql[0] = "Insert into Role(RoleId,RoleName,RoleKey,RoleType,RoleLevel,CCODE,Description) Values ({0},'{1}','{2}','{3}','{4}','{5}','{6}')";
                    strSql[0] = string.Format(strSql[0], "Seq_Role.Nextval", role.RoleName, role.RoleKey, role.RoleType, role.RoleLevel, role.CompanyCode, role.Description);
                    break;
                case ActionEnum.Update:
                    strSql = new string[1];
                    strSql[0] = "Update Role Set RoleName = '{1}',RoleKey = '{2}',Description = '{3}' Where RoleId = {0}";
                    strSql[0] = string.Format(strSql[0], role.RoleId, role.RoleName, role.RoleKey, role.Description);
                    break;
                case ActionEnum.Delete:
                    strSql = new string[3];
                    strSql[0] = "delete from Role Where RoleId = {0}";
                    strSql[0] = string.Format(strSql[0], role.RoleId);
                    strSql[1] = "delete from usersandrole Where RoleId = {0}";
                    strSql[1] = string.Format(strSql[1], role.RoleId);
                    strSql[2] = "delete from roleandfunction Where RoleId = {0}";
                    strSql[2] = string.Format(strSql[2], role.RoleId);
                    break;
            }
            return OracleHelper.ExecuteCommand(strSql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public string ValidateDelete(string roleId)
        {
            string strSql = string.Empty;
            strSql = "select Count(*) from usersandrole where RoleId = " + roleId + "";
            if (OracleHelper.ExecuteDataTable(strSql).Rows[0][0].ToString() != "0")
            {
                return "1";////角色下存在用户
            }
            return "0";
        }
        /// <summary>
        /// 管理角色的菜单权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="functionAry"></param>
        /// <returns></returns>
        public bool ManageRolesFunction(string roleId, string[] functionAry)
        {
            string[] strSql = new string[functionAry.Length + 1];

            strSql[0] = "delete from roleandfunction where RoleId = " + roleId;

            for (int i = 0; i < functionAry.Length; i++)
            {
                if (!string.IsNullOrEmpty(functionAry[i]))
                {
                    strSql[i + 1] = "Insert Into roleandfunction (RoleId,FunctionCode) Values ('{0}','{1}')";
                    strSql[i + 1] = string.Format(strSql[i + 1], roleId, functionAry[i].Trim());
                }
            }
            return OracleHelper.ExecuteCommand(strSql);
        }

        /// <summary>
        /// 根据登录用户获取角色的集合
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IList<Role> GetRoles(LoginUser user)
        {
            IList<Role> datalist = new List<Role>();

            string strSql = "select * from role where ((roletype = 1 and instr(RoleLevel,{0}) <> 0) or (roletype = 0 and ccode = '{1}')) order by RoleId";
            strSql = string.Format(strSql, ((int)user.CompanyType).ToString(), user.CompanyCode);
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                Role roleEntity = new Role();
                roleEntity.RoleId = dr["RoleId"].ToString();
                roleEntity.RoleName = dr["RoleName"].ToString();
                roleEntity.DisplayName = dr["RoleName"].ToString();
                roleEntity.RoleKey = dr["RoleKey"].ToString();
                roleEntity.RoleType = dr["RoleType"].ToString();
                roleEntity.Description = dr["Description"].ToString();
                datalist.Add(roleEntity);
            }
            dr.Close();
            return datalist;
        }


        /// <summary>
        /// 获得角色的集合
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IList<Role> GetRoles(Role role)
        {
            IList<Role> datalist = new List<Role>();
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(role.FilterRoleKey)) strWhere = "a.RoleKey not in '" + role.FilterRoleKey + "' And ";
            if (!string.IsNullOrEmpty(role.RoleType)) strWhere = "a.RoleType = '" + role.RoleType + "' And ";
            if (!string.IsNullOrEmpty(role.RoleName)) strWhere += "a.RoleName like '%" + role.RoleName + "%' And ";
            if (!string.IsNullOrEmpty(role.RoleKey)) strWhere += "a.roleKey like '%" + role.RoleKey + "%' And ";
            if (!string.IsNullOrEmpty(role.RoleId)) strWhere += "a.roleId In (" + role.RoleId + ") And ";
            if (!string.IsNullOrEmpty(role.CompanyCode)) strWhere += "a.ccode like '" + role.CompanyCode + "%' And ";

            if (!string.IsNullOrEmpty(role.RoleLevel))
            {
                if (role.RoleLevel.IndexOf(",") == -1)
                {
                    if (role.RoleLevel.Equals(((int)CompanyTypeEnum.SHI).ToString()))
                    {
                        strWhere += "length(b.ShortCCode) = 4 And ";
                    }
                    if (role.RoleLevel.Equals(((int)CompanyTypeEnum.XIAN).ToString()))
                    {
                        strWhere += "length(b.ShortCCode) = 6 And ";
                    }
                }
            }

            string strSql = "select * from role a left join company b on a.cCode = b.cCode where {0} 1 = 1  order by  a.roletype desc, a.ccode";
            strSql = string.Format(strSql, strWhere);
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                Role roleEntity = new Role();
                roleEntity.RoleId = dr["RoleId"].ToString();
                roleEntity.RoleName = dr["RoleName"].ToString();
                roleEntity.RoleKey = dr["RoleKey"].ToString();
                roleEntity.RoleType = dr["RoleType"].ToString();
                roleEntity.Description = dr["Description"].ToString();
                if (dr["RoleType"].ToString() == "0")
                {
                    roleEntity.FullName = string.Format("{0}--{1}", dr["cName"].ToString(), dr["RoleName"].ToString());
                }
                if (dr["RoleType"].ToString() == "1")
                {
                    roleEntity.FullName = dr["RoleName"].ToString();
                }
                if (!role.AllowMove)
                    roleEntity.AllowMove = (roleEntity.RoleType == "0");

                datalist.Add(roleEntity);
            }
            dr.Close();
            return datalist;
        }

        /// <summary>
        /// 更新系统角色的级别
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="nodeLevel"></param>
        /// <returns></returns>
        public bool UpdateRoleLevel(string roleId, string nodeLevel)
        {
            string strSql = "update role set roleLevel = '{1}' where roletype = 1 and roleid in ({0})";
            strSql = string.Format(strSql, roleId, nodeLevel);
            return OracleHelper.ExecuteCommand(strSql);
        }

        public string GetFunctionByRole(string roleId)
        {
            string functionStr = string.Empty;
            string strSql = "select * from roleandfunction where RoleId = {0}";
            strSql = string.Format(strSql, roleId);
            OracleDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                functionStr += dr["FunctionCode"].ToString() + ",";
            }
            dr.Close();
            if (functionStr.EndsWith(","))
                functionStr = functionStr.Substring(0, functionStr.Length - 1);
            return functionStr;
        }

        public DataTable GetRolesById(string roleId)
        {
            string strSql = "select * from Role where RoleId = '{0}'";
            strSql = string.Format(strSql, roleId);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public DataTable GetRolesByName(string roleId, string roleName)
        {
            string strSql = string.Empty;
            if (roleId == "0")
            {
                strSql = "select * from Role where RoleName = '{0}'";
                strSql = string.Format(strSql, roleName);
            }
            else
            {
                strSql = "select * from Role where roleid <> '{0}' and RoleName = '{1}'";
                strSql = string.Format(strSql, roleId, roleName);
            }
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public DataTable GetRolesByKey(string roleKey)
        {
            string strSql = "select * from Role where roleKey = '{0}'";
            strSql = string.Format(strSql, roleKey);
            return OracleHelper.ExecuteDataTable(strSql);
        }
    }
}
