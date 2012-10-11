using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataAccess;
using System.Data;
using HHSoft.FieldProtect.DataEntity.WorkFlow;
using HHSoft.FieldProtect.Framework.Utility;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.DataEntity.SysManage;
using System.Collections;
using HHSoft.FieldProtect.DataEntity.PersonalManage;
using HHSoft.FieldProtect.Business.PersonalManage;
using HHSoft.FieldProtect.Business.Common;

namespace HHSoft.FieldProtect.WorkFlow
{
    public class WorkFlowCenter
    {
        private string strFlowId;

        public WorkFlowCenter(string flowId)
        {
            this.strFlowId = flowId;
        }
        public WorkFlowCenter()
        {

        }

        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <param name="wfItem"></param>
        /// <returns></returns>
        public WfReturn CreateInstance(WfItem wfItem)
        {
            WfReturn result = new WfReturn();
            ItemState itemState = ItemState.Beginning;
            WfState wfState = WfState.Normal;
            string orderNo = string.Empty;

            try
            {
                ////判断该项目的状态是否可以操作
                DataTable dt = OracleHelper.ExecuteDataTable("select * from xm_xmxx where ItemCode in (" + wfItem.ItemCode + ")");
                WfState ItemWfState = (WfState)EnumHelper.StringValueToEnum(typeof(WfState), dt.Rows[0]["wfState"].ToString());

                if (ItemWfState != WfState.Normal)
                {
                    result.Success = false;
                    result.ResultDesc = string.Format("该项目处于{0}状态,无法操作",
                        EnumHelper.GetFieldDescription(typeof(WfState), (int)ItemWfState));
                    return result;
                }

                ////当前环节信息(0为开始环节)
                WfNode curNode = this.GetNodeInfo(this.strFlowId, wfItem.NodeId);
                if (curNode == null)
                {
                    result.Success = false;
                    result.ResultDesc = "获取当前环节信息失败!";
                    return result;
                }

                ////下一环节信息
                WfNode nextNode = null;
                string perNodeId = string.Empty;
                string nextNodeId = string.Empty;
                ////存在跳转ID
                if (!string.IsNullOrEmpty(wfItem.SwicthNode))
                {
                    perNodeId = wfItem.SwicthNode;
                    nextNodeId = wfItem.SwicthNode;
                }
                else
                {
                    perNodeId = curNode.PerNode;
                    nextNodeId = curNode.NextNode;
                }

                ////退回操作--上一结点
                if (wfItem.Result == WfResult.Return)
                    nextNode = this.GetNodeInfo(this.strFlowId, perNodeId);
                ////同意操作--下一结点
                if (wfItem.Result == WfResult.Agree)
                    nextNode = this.GetNodeInfo(this.strFlowId, nextNodeId);

                ArrayList strSql = new ArrayList();
                string tmpSql = string.Empty;

                if (curNode.PerNode == ((int)WorkFlowNode.Begin).ToString())////开始结点
                {
                    //// 1 删除该项目所有进程信息
                    tmpSql = "delete from wf_instance where itemcode = '{0}'";
                    tmpSql = string.Format(tmpSql, wfItem.ItemCode);
                    strSql.Add(tmpSql);
                    //// 2 插入开始环节的已完成记录
                    tmpSql = "Insert into wf_instance(flowid,itemcode,orderno,nodeid,perNode,nextNode,userid,username,result,resultdesc,begindate,enddate,state)"
                        + " values ('{0}','{1}',1,'{2}','{3}','{4}','{5}','{6}','{7}','{8}',sysdate,sysdate,1)";
                    tmpSql = string.Format(tmpSql, strFlowId, wfItem.ItemCode, curNode.NodeId,
                        curNode.PerNode, curNode.NextNode, wfItem.UserId, wfItem.UserName,
                        ((int)wfItem.Result).ToString(), wfItem.ResultDesc);
                    strSql.Add(tmpSql);
                    //// 3 插入下一环节的未完成记录
                    tmpSql = "Insert into wf_instance(flowid,itemcode,orderno,nodeid,perNode,nextNode,userid,username,result,resultdesc,begindate,enddate,state)"
                       + " values ('{0}','{1}',2,'{2}','{3}','{4}','','','','',sysdate,'',0)";
                    tmpSql = string.Format(tmpSql, strFlowId, wfItem.ItemCode, nextNode.NodeId,
                        nextNode.PerNode, nextNode.NextNode);
                    strSql.Add(tmpSql);
                    itemState = ItemState.Progressing;
                    wfState = WfState.Normal;
                }
                else ////任务结点
                {
                    //// 更新项目进程表中当前环节的状态
                    tmpSql = "update wf_instance set enddate = sysdate, state = 1,userid = '{2}',username = '{3}',result = '{4}', resultdesc = '{5}'"
                          + " where itemCode in ({0}) and nodeid = {1} and state = 0";
                    tmpSql = string.Format(tmpSql, wfItem.ItemCode, wfItem.NodeId, wfItem.UserId, wfItem.UserName, ((int)wfItem.Result).ToString(), wfItem.ResultDesc);
                    strSql.Add(tmpSql);

                    //// 插入下一环节的进程信息
                    if (wfItem.Result != WfResult.Delete)
                    {
                        tmpSql = "delete from wf_instance where itemCode in ({0}) and nodeid = {1} and state = 0";
                        tmpSql = string.Format(tmpSql, wfItem.ItemCode, nextNode.NodeId);
                        strSql.Add(tmpSql);

                        ////针对批量操作
                        string[] itemAry = wfItem.ItemCode.Split(',');

                        for (int i = 0; i < itemAry.Length; i++)
                        {
                            orderNo = getOrdernoByItem(itemAry[i]);
                            //// 插入下一环节的未完成记录
                            tmpSql = "Insert into wf_instance(flowid,itemcode,orderno,nodeid,perNode,nextNode,userid,username,result,resultdesc,begindate,enddate,state)"
                               + " values ('{0}','{1}','{2}','{3}','{4}','{5}','','','','',sysdate,'',0)";
                            tmpSql = string.Format(tmpSql, strFlowId, itemAry[i], orderNo, nextNode.NodeId,
                                nextNode.PerNode, nextNode.NextNode);
                            strSql.Add(tmpSql);
                        }
                    }
                    if (nextNode != null)
                    {
                        if (nextNode.NodeType == NodeType.EndNode)
                        {
                            tmpSql = "update wf_instance set enddate = sysdate, state = 1,userid = '{2}',username = '{3}',result = '{4}', resultdesc = '{5}'"
                                   + " where itemCode in ({0}) and nodeid = {1} and state = 0";
                            tmpSql = string.Format(tmpSql, wfItem.ItemCode, nextNode.NodeId, wfItem.UserId,
                                wfItem.UserName, ((int)wfItem.Result).ToString(), wfItem.ResultDesc);
                            strSql.Add(tmpSql);
                        }
                    }

                    switch (wfItem.Result)
                    {
                        case WfResult.Agree:
                            if (nextNode.NodeType == NodeType.EndNode)
                            {
                                wfState = WfState.Normal;
                                itemState = ItemState.Ending;
                            }
                            else
                            {
                                wfState = WfState.Normal;
                                itemState = ItemState.Progressing;
                            }
                            break;
                        case WfResult.Return:
                            wfState = WfState.Normal;
                            itemState = ItemState.Progressing;
                            break;
                        case WfResult.Delete:
                            wfState = WfState.Delete;
                            itemState = ItemState.Ending;
                            break;
                    }
                }

                if (wfItem.Result == WfResult.Delete)
                {
                    tmpSql = "update xm_xmxx set itemstate = '{1}', wfstate = '{2}', ItemDesc = '{3}',read = 0 where ItemCode in ({0})";
                    tmpSql = string.Format(tmpSql, wfItem.ItemCode, ((int)itemState).ToString(), ((int)wfState).ToString(), wfItem.ResultDesc);
                    strSql.Add(tmpSql);
                }
                else
                {
                    //// 更新项目主表的状态标识
                    tmpSql = "update xm_xmxx set itemstage = '{1}', nodeid = '{2}', itemstate = '{3}', wfstate = '{4}',read = 0  where ItemCode in ({0})";
                    tmpSql = string.Format(tmpSql, wfItem.ItemCode, nextNode.Stage, nextNode.NodeId, ((int)itemState).ToString(), ((int)wfState).ToString());
                    strSql.Add(tmpSql);
                }

                bool succ = OracleHelper.ExecuteCommand(strSql);

                if (succ)
                {
                    if (curNode.NotifyType != string.Empty && nextNode != null)////有消息发送设置
                    {
                        ////发送消息                   
                        this.notifyUser(curNode, nextNode, wfItem);
                    }
                    result.Success = true;
                    result.ResultDesc = "成功";
                    if (nextNode != null)
                    {
                        result.Stage = nextNode.Stage;
                        result.NodeName = nextNode.NodeName;
                        this.changeNodeQx(wfItem.ItemCode.Split(',')[0], ref nextNode);
                        result.DepartList = this.getItemName(1, nextNode.NodeDepartCode);
                        result.RoleList = this.getItemName(2, nextNode.NodeRoleId);
                        result.UserList = this.getItemName(3, nextNode.NodeUserId);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 线外控制流程
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool ContorlWf(string itemCode, WfResult result,string itemDesc)
        {
            bool succ = false;
            ItemState itemState = ItemState.Beginning;
            WfState wfState = WfState.Normal;

            if (result == WfResult.Stop) ////暂停
            {
                itemState = ItemState.Progressing;
                wfState = WfState.Stop;
            }

            if (result == WfResult.Start)///启动
            {
                itemState = ItemState.Progressing;
                wfState = WfState.Normal;
            }

            if (result == WfResult.Delete) ////终止
            {
                itemState = ItemState.Ending;
                wfState = WfState.Delete;
            }

            string strSql = "update xm_xmxx set ItemState = '{1}', WfState = '{2}',ItemDesc = '{3}' where ItemCode in ({0})";
            strSql = string.Format(strSql, itemCode, ((int)itemState).ToString(), ((int)wfState).ToString(), itemDesc);
            succ = OracleHelper.ExecuteCommand(strSql);
            return succ;
        }

        /// <summary>
        /// 收回项目
        /// </summary>
        /// <param name="itemCode">项目编号</param>
        /// <param name="userid">收回用户</param>
        /// <returns></returns>
        public string UndoInstance(string itemCode, string userid)
        {
            string tmpSql = string.Empty;
            tmpSql = "select * from ("
                   +" select * from wf_instance where flowid = '{0}' and  itemcode = '{1}' "
                   +" and userId = {2} and state = 1 order by orderno desc) where rownum = 1";
            tmpSql = string.Format(tmpSql, strFlowId, itemCode, userid);
            DataTable dt = OracleHelper.ExecuteDataTable(tmpSql);
            if (dt.Rows.Count != 1) return string.Empty;

            string orderno = dt.Rows[0]["orderno"].ToString();
            string nodeid = dt.Rows[0]["nodeid"].ToString(); 
            
            ArrayList strSql = new ArrayList();
            tmpSql = "delete from wf_instance where flowid = {0} and itemCode = {1} and state = 0";
            tmpSql = string.Format(tmpSql, strFlowId, itemCode);
            strSql.Add(tmpSql);
            tmpSql = "update wf_instance set state = 0 where flowid = {0} and itemCode = {1} and orderno = {2}";
            tmpSql = string.Format(tmpSql, strFlowId, itemCode, orderno);
            strSql.Add(tmpSql);
            tmpSql = "update xm_xmxx set nodeid = {1},itemStage = {2} where itemCode = {0}";
            tmpSql = string.Format(tmpSql, itemCode, nodeid, 
                ((int)CommonManage.nodeToStage((WorkFlowNode)EnumHelper.StringValueToEnum(typeof(WorkFlowNode), nodeid))).ToString());
            strSql.Add(tmpSql);

            if (nodeid == ((int)WorkFlowNode.TB).ToString())
            {
                //tmpSql = "update xm_xmxx set ItemState = 0,flowId ='',itemstage ='',sbsj ='' where itemCode = {0}";
                tmpSql = "update xm_xmxx set ItemState = 0 where itemCode = {0}";
                tmpSql = string.Format(tmpSql, itemCode);
                strSql.Add(tmpSql);
            }
            if (OracleHelper.ExecuteCommand(strSql))
            {
                return nodeid;
            }
            return string.Empty;
        }

        #region 发送消息
        /// <summary>
        /// 站内发送短消息
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="wfItem"></param>
        private void notifyUser(WfNode curNode, WfNode nextNode, WfItem wfItem)
        {
            string[] notifyAry = curNode.NotifyType.Split(new char[] { ',' });
            string msg = string.Empty;            
            List<string> userId = new List<string>();
            string[] itemAry = wfItem.ItemCode.Split(',');

            for (int i = 0; i < itemAry.Length; i++)
            {
                string itemName = this.getItemName(itemAry[i]);

                List<string> beginuserId = new List<string>();
                List<string> nextuserId = new List<string>();
                List<string> historyuserId = new List<string>();

                if (curNode.NodeType == NodeType.BeginNode)////开始结点
                {
                    //beginuserId = wfItem.UserId;
                    //historyuserId = wfItem.UserId;
                }
                else
                {
                    beginuserId = this.getBeginUserId(itemAry[i]);
                    historyuserId = this.getHistoryUserId(itemAry[i]);
                }
                if (nextNode != null)
                    nextuserId = this.getUserId(itemAry[i], nextNode.NodeDepartCode, nextNode.NodeRoleId, nextNode.NodeUserId);

                for (int j = 0; j < notifyAry.Length; j++)
                {
                    WfNotifyType notifyEnum = (WfNotifyType)EnumHelper.StringValueToEnum(typeof(WfNotifyType), notifyAry[j]);
                    switch (notifyEnum)
                    {
                        case WfNotifyType.Begin:
                            msg = curNode.BeginText.Replace("{wf_username}", wfItem.UserName)
                                .Replace("{wf_time}", DateTime.Now.ToString())
                                .Replace("{wf_title}", itemName)
                                .Replace("{wf_stage}", 
                                string.Format("{0}-{1}",
                                EnumHelper.GetFieldDescription(typeof(ItemStage), int.Parse(nextNode.Stage)),
                                EnumHelper.GetFieldDescription(typeof(WorkFlowNode),int.Parse(nextNode.NodeId))));
                            userId = beginuserId;
                            break;
                        case WfNotifyType.Next:
                            msg = curNode.NextText.Replace("{wf_title}", itemName)
                                .Replace("{wf_timeout}", nextNode.TimeOut.Equals("0") ? "无" : string.Format("{0}天", nextNode.TimeOut));
                            userId = nextuserId;
                            break;
                        case WfNotifyType.History:
                            msg = curNode.HistoryText.Replace("{wf_username}", wfItem.UserName)
                                .Replace("{wf_time}", DateTime.Now.ToString())
                                .Replace("{wf_title}", itemName);
                            userId = historyuserId;
                            break;
                    }
                    if (msg != string.Empty || userId.Count > 0)
                    {                        
                        this.SendMessage(userId, msg);
                    }
                }
            }
        }

        private void SendMessage(List<string> userId, string msg)
        {
            XTXX xtxx = new XTXX();  

            xtxx.XXBH = Guid.NewGuid().ToString();
            xtxx.XXBT = "系统消息";
            xtxx.XXNR = msg;
            xtxx.FSR = "0";
            xtxx.FSSJ = DateTime.Now;
            xtxx.XXLX = "1";            
            xtxx.FSRSC = "1";

            List<XTXXJS> xtxxjss = new List<XTXXJS>();

            foreach (string sjr in userId)
            {
                xtxxjss.Add(new XTXXJS()
                {
                    JSXXBH = xtxx.XXBH,
                    JSR = sjr,
                    JSRXZDM = "",
                    YDZT = "0",
                    JSRSC = "0"
                });
            }
            MessageManage mess = new MessageManage();
            mess.AddMessage(xtxx, xtxxjss);
        }

        #endregion

        /// <summary>
        /// 获取项目名称
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        private string getItemName(string itemCode)
        {
            string itemName = string.Empty;
            string strSql = "select ItemName from xm_xmxx where ItemCode = '{0}'";
            strSql = string.Format(strSql, itemCode);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                itemName = dt.Rows[0][0].ToString();
            }
            return itemName;
        }

        /// <summary>
        /// 根据项目编号过滤环节的权限部分
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="node"></param>
        private void changeNodeQx(string itemCode, ref WfNode node)
        {
            string cCode = itemCode.Substring(0, 6);
            string cShiCode = CommonHelper.GetSHICode(cCode);
            string strSql = string.Empty;

            if (!string.IsNullOrEmpty(node.NodeDepartCode))
            {
                strSql = "select wm_concat(deptCode) from department where deptcode in ({0})"
                     + " and (ccode = '{1}' or ccode = '{2}')";
                strSql = string.Format(strSql, node.NodeDepartCode, cCode, cShiCode);
                node.NodeDepartCode = OracleHelper.ExecuteDataTable(strSql).Rows[0][0].ToString();
            }
            if (!string.IsNullOrEmpty(node.NodeUserId))
            {
                strSql = "select wm_concat(userid) from users where userid in ({0})"
                     + " and (ccode = '{1}' or ccode = '{2}')";
                strSql = string.Format(strSql, node.NodeUserId, cCode, cShiCode);
                node.NodeUserId = OracleHelper.ExecuteDataTable(strSql).Rows[0][0].ToString();
            }
            if (!string.IsNullOrEmpty(node.NodeRoleId))
            {
                strSql = "select wm_concat(roleid) from role where roleId in ({0})"
                     + " and (roleType = 1 or ccode = '{1}' or ccode = '{2}')";
                strSql = string.Format(strSql, node.NodeRoleId, cCode, cShiCode);
                node.NodeRoleId = OracleHelper.ExecuteDataTable(strSql).Rows[0][0].ToString();
            }
        }

        /// <summary>
        /// 根据权限分配获取用户ID的字符串
        /// </summary>
        /// <param name="deptcode"></param>
        /// <param name="roleid"></param>
        /// <param name="userid"></param>
        private List<string> getUserId(string itemCode, string deptcode, string roleid, string userid)
        {            
            List<string> userList = new List<string>();           
            
            string sQxSql = string.Empty;
            if (deptcode != string.Empty) sQxSql += "select userid from users where  deptcode in (" + deptcode + ") union ";
            if (roleid != string.Empty) sQxSql += "select userid from usersandrole where  roleid in (" + roleid + ") union ";
            if (userid != string.Empty) sQxSql += "select userid from users where  userid in (" + userid + ") union ";
            if (sQxSql != string.Empty) sQxSql = sQxSql.Substring(0, sQxSql.Length - 6);
                        
            if (sQxSql == string.Empty) return userList;            

            string cCode = itemCode.Substring(0, 6);
            string cShiCode = CommonHelper.GetSHICode(cCode);           
            string struserSql = "select * from users where userid in ({0}) and (ccode = '{1}' or ccode = '{2}')";
            struserSql = string.Format(struserSql, sQxSql, cCode, cShiCode);
            DataTable dtUser = OracleHelper.ExecuteDataTable(struserSql);
            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
               userList.Add(dtUser.Rows[i][0].ToString());                    
            }
            
            return userList;
        }

        /// <summary>
        /// 获取环节配置信息
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public WfNode GetNodeInfo(string workFlowId, string nodeId)
        {
            WfNode node = null;
            string strSql = "select * from wf_node where FlowId = {0} and NodeId = {1}";
            strSql = string.Format(strSql, workFlowId, nodeId);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 1)
            {
                node = new WfNode();
                node.NodeId = dt.Rows[0]["nodeId"].ToString();                
                node.NodeDesc = dt.Rows[0]["NodeDesc"].ToString();
                node.Stage = dt.Rows[0]["Stage"].ToString();
                node.PerNode = dt.Rows[0]["Pernode"].ToString();
                node.NextNode = dt.Rows[0]["NextNode"].ToString();
                node.NodeLevel = dt.Rows[0]["NodeLevel"].ToString();
                node.TimeOut = dt.Rows[0]["TimeOut"].ToString();
                node.NodeDepartCode = dt.Rows[0]["NodeDepartCode"].ToString();
                node.NodeRoleId = dt.Rows[0]["NodeRoleId"].ToString();
                node.NodeUserId = dt.Rows[0]["NodeUserId"].ToString();
                node.NodeFileCode = dt.Rows[0]["NodeFileCode"].ToString();
                node.NodeType = (NodeType)EnumHelper.StringValueToEnum(typeof(NodeType), dt.Rows[0]["NodeType"].ToString());
                node.NotifyType = dt.Rows[0]["NotifyType"].ToString();
                node.BeginText = dt.Rows[0]["notifyBeginText"].ToString();
                node.NextText = dt.Rows[0]["notifyNextText"].ToString();
                node.HistoryText = dt.Rows[0]["notifyHistoryText"].ToString();
            }
            return node;
        }

        /// <summary>
        /// 获取流程的进程信息
        /// </summary>
        /// <param name="workFlowId"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public IList<Wfinstance> GetInstance(string workFlowId,string itemCode)
        {
            IList<Wfinstance> datalist = new List<Wfinstance>();
            string strSql = "select * from wf_instance where FlowId = '{0}' and itemCode = '{1}' order by orderno";
            strSql = string.Format(strSql, workFlowId, itemCode);
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                Wfinstance entity = new Wfinstance();          
                entity.FlowId = dr["FlowId"].ToString();
                entity.ItemCode = dr["ItemCode"].ToString();
                entity.Orderno = int.Parse(dr["Orderno"].ToString());
                entity.NodeId = dr["NodeId"].ToString();
                entity.PerNode = dr["PerNode"].ToString();
                entity.NextNode = dr["NextNode"].ToString();
                entity.UserId = dr["UserId"].ToString();
                entity.UserName = dr["UserName"].ToString();
                entity.Result = dr["Result"].ToString();
                entity.ResultDesc = dr["ResultDesc"].ToString();
                entity.BeginDate = dr["BeginDate"].ToString();
                entity.EndDate = dr["EndDate"].ToString();
                entity.State = dr["State"].ToString();
                datalist.Add(entity);
            }
            dr.Close();
            return datalist; 
        }

        /// <summary>
        /// 获取权限部分的名称
        /// </summary>
        /// <param name="type"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        private string getItemName(int type, string itemid)
        {
            string returnName = string.Empty;
            if (itemid == string.Empty) return returnName;
            string strSql = string.Empty;
            switch (type)
            {
                case 1:////部门
                    strSql = "select CName,DeptName Name from department a left join company b on a.ccode = b.ccode"
                           + " where a.deptcode in ({0})";
                    break;
                case 2:////角色
                    strSql = "select rolename Name from role where roleid in ({0})";
                    break;
                case 3:////用户
                    strSql = "select CName,username Name from users a left join company b on a.ccode = b.ccode "
                          + " where a.userid in ({0})";
                    break;
            }
            strSql = string.Format(strSql, itemid);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (type.Equals(1) || type.Equals(3))
                {
                    returnName += string.Format("{0}--{1}", dt.Rows[i]["CName"].ToString(), dt.Rows[i]["Name"].ToString());
                }
                if (type.Equals(2))
                {
                    returnName += dt.Rows[i]["Name"].ToString();
                }
                if (i != dt.Rows.Count - 1) returnName += ",";
            }
            return returnName;
        }
        /// <summary>
        /// 根据项目编号返回最新的流程序号
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        private string getOrdernoByItem(string itemCode)
        {
            string orderno = string.Empty;
            string strSql = "select nvl(max(orderno),1) + 1 orderno from wf_instance where itemcode = '{0}'";
            strSql = string.Format(strSql, itemCode);
            return OracleHelper.ExecuteDataTable(strSql).Rows[0][0].ToString();
        }

        /// <summary>
        /// 获取流程创建人ID
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        private List<string> getBeginUserId(string itemCode)
        {
            List<string> userList = new List<string>();
            string strSql = "select userId from wf_instance where itemCode = '{0}' and perNode = 0";
            strSql = string.Format(strSql, itemCode);
            DataTable userdt = OracleHelper.ExecuteDataTable(strSql);
            userList.Add(userdt.Rows[0][0].ToString());
            return userList;
        }

        /// <summary>
        /// 获取流程历史处理人ID
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        private List<string> getHistoryUserId(string itemCode)
        {
            List<string> userList = new List<string>();

            string strSql = "select userId from wf_instance where itemCode = '{0}' and state = 1";
            strSql = string.Format(strSql, itemCode);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!userList.Contains(dt.Rows[i][0].ToString()))
                {
                    userList.Add(dt.Rows[i][0].ToString());
                }

            }
            return userList;
        }

        /// <summary>
        /// 获取流程
        /// </summary>
        /// <returns></returns>
        public DataTable GetWorkFlow(string cCode)
        {
            string strSql = "select * from wf_workflow where cCode = '{0}' order by Version desc";
            strSql = string.Format(strSql, cCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 获取环节
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public DataTable GetWfNode(string workflowId)
        {
            string strSql = "select * from wf_node where flowId = {0} order by NodeId";
            strSql = string.Format(strSql, workflowId);
            return OracleHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取环节对应的文档权限
        /// </summary>
        /// <param name="wffile"></param>
        /// <returns></returns>
        public IList<WfFile> GetWfFIle(WfFile wffile)
        {
            IList<WfFile> datalist = new List<WfFile>();
            string strWhere = string.Empty;

            if (string.IsNullOrEmpty(wffile.FileCode))
            {
                return datalist;
            }            
            strWhere += "FileCode In (" + wffile.FileCode + ") And ";

            string strSql = "select * from wf_file where {0} 1 = 1  order by OrderNo,FileCode";
            strSql = string.Format(strSql, strWhere);
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                WfFile fileEntity = new WfFile();
                fileEntity.FileCode = dr["FileCode"].ToString();
                fileEntity.FileName = dr["FileName"].ToString();
                fileEntity.FileType = dr["FileType"].ToString();
                datalist.Add(fileEntity);
            }
            dr.Close();
            return datalist;
        }

        /// <summary>
        /// 获取全部文档
        /// </summary>
        /// <returns></returns>
        public IList<WfFile> GetWfFIle()
        {
            IList<WfFile> datalist = new List<WfFile>();   
            string strSql = "select * from wf_file order by FileCode";
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                WfFile fileEntity = new WfFile();
                fileEntity.FileCode = dr["FileCode"].ToString();
                fileEntity.FileName = dr["FileName"].ToString();
                fileEntity.FileType = dr["FileType"].ToString();
                datalist.Add(fileEntity);
            }
            dr.Close();
            return datalist;
        }
        
        /// <summary>
        /// 保存环节信息
        /// </summary>
        /// <param name="wfNode"></param>
        /// <returns></returns>
        public bool SaveWfNodeInfo(WfNode wfNode)
        {
            string strSql = "update wf_node set nodedesc = '{2}',nodeLevel = '{3}', nodedepartcode = '{4}', noderoleid = '{5}', nodeuserid = '{6}'"
                + ", nodefilecode = '{7}', timeout = '{8}', notifytype = '{9}', notifybegintext = '{10}', notifynexttext = '{11}', notifyhistorytext = '{12}'"
                + "Where flowId = {0} and nodeId = {1}";
            strSql = string.Format(strSql, wfNode.FlowId, wfNode.NodeId, wfNode.NodeDesc, wfNode.NodeLevel, wfNode.NodeDepartCode,
                wfNode.NodeRoleId, wfNode.NodeUserId, wfNode.NodeFileCode, wfNode.TimeOut, wfNode.NotifyType, wfNode.BeginText,
                wfNode.NextText, wfNode.HistoryText);

            return OracleHelper.ExecuteCommand(strSql);
        }
    }
}
