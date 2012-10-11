using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.SysManage;
using HHSoft.FieldProtect.Business.SysManage;
using System.Data;
using HHSoft.FieldProtect.Framework.Utility;

namespace HHSoft.FieldProtect.Business.Common
{
    public class TreeViewOperation
    {
        private TreeViewNode BuildNode(string id, string value, string display, bool IsExpand, bool hasChildren)
        {
            return new TreeViewNode() { id = id, value = value, text = display, isexpand = IsExpand, hasChildren = hasChildren, showcheck = true };
        }

        private void AddNode(TreeViewNode parent, TreeViewNode child)
        {
            if (child != null)
            {
                parent.ChildNodes.Add(child);
            }
        }

        public List<TreeViewNode> BuildUserTreeViewData(string queryProvinceShortCode, string queryCityShortCode, string queryDistrictShortCode)
        {
            BusiCompanyService company = new BusiCompanyService();
            BusiDepartMent department = new BusiDepartMent();

            List<TreeViewNode> result = new List<TreeViewNode>();

            //省。
            DataTable dtProvince = company.GetCompanyInfo(queryProvinceShortCode, 2);
            foreach (DataRow drProvince in dtProvince.Rows)
            {
                string provinceName = drProvince["CNAME"].ToString();
                string provinceCode = drProvince["CCODE"].ToString();
                TreeViewNode province = BuildNode(provinceCode, string.Empty, provinceName, true, true);
                DataTable dtProvinceDepartment = department.GetDeptMentInfo(provinceCode);
                AddDepartmentAndUser(province, dtProvinceDepartment);
                result.Add(province);
            }

            List<TreeViewNode> cityNodes = new List<TreeViewNode>();
            DataTable dtCity = company.GetCompanyInfo(queryCityShortCode, 4);

            DataTable dtDistrict = company.GetCompanyInfo(queryDistrictShortCode, 6);

            //市。
            foreach (DataRow drCity in dtCity.Rows)
            {
                string cityName = drCity["CNAME"].ToString();
                string cityCode = drCity["CCODE"].ToString();
                TreeViewNode city = BuildNode(cityCode, string.Empty, cityName, false, true);
                DataTable dtCityDepartment = department.GetDeptMentInfo(cityCode);
                AddDepartmentAndUser(city, dtCityDepartment);

                //区。
                foreach (DataRow drDistrict in dtDistrict.Select("ccode like '" + drCity["SHORTCCODE"].ToString() + "%'"))
                {
                    string districtName = drDistrict["CNAME"].ToString();
                    string districtCode = drDistrict["CCODE"].ToString();
                    TreeViewNode district = BuildNode(districtCode, string.Empty, districtName, false, true);
                    DataTable dtDistrictDepartment = department.GetDeptMentInfo(districtCode);
                    AddDepartmentAndUser(district, dtDistrictDepartment);
                    AddNode(city, district);
                }
                cityNodes.Add(city);
            }

            bool haveProvince = true;
            if (result.Count == 0)
            {
                haveProvince = false;
            }
            foreach (var cityNode in cityNodes)
            {
                if (haveProvince)
                {
                    AddNode(result[0], cityNode);
                }
                else
                {
                    result.Add(cityNode);
                }
            }

            return result;
        }

        private void AddDepartmentAndUser(TreeViewNode company, DataTable dt)
        {
            BusiDepartMent department = new BusiDepartMent();
            foreach (DataRow drDepartment in dt.Rows)
            {
                string departmentCode = drDepartment["DEPTCODE"].ToString();
                string departmentName = drDepartment["DEPTNAME"].ToString();
                TreeViewNode departmentNode = BuildNode(departmentCode, string.Empty, departmentName, false, true);
                AddUserNode(departmentNode, departmentCode);
                AddNode(company, departmentNode);
            }
        }

        private void AddUserNode(TreeViewNode departmentNode, string departmentCode)
        {
            TreeViewOperation treeViewOperation = new TreeViewOperation();
            BusiUserService user = new BusiUserService();

            DataTable dtUser = user.GetUserByDeptCode(departmentCode);
            for (int j = 0; j < dtUser.Rows.Count; j++)
            {
                string provinceUserCompanyCode = dtUser.Rows[j]["CCODE"].ToString();
                string provinceUserId = dtUser.Rows[j]["USERID"].ToString();
                string provinceUserName = dtUser.Rows[j]["REALNAME"].ToString();
                TreeViewNode userNode = treeViewOperation.BuildNode(Guid.NewGuid().ToString(), provinceUserId, provinceUserName, false, false);
                userNode.ServerXzdm = CommonHelper.GetSHICode(provinceUserCompanyCode);
                treeViewOperation.AddNode(departmentNode, userNode);
            }
        }
    }
}
