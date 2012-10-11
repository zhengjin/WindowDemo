using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace ArcGisView
{
    public partial class SearchViewInfo : Form
    {
        IFeature pFeature;
        public SearchViewInfo(IFeature ft)
        {
            InitializeComponent();
            pFeature = ft;
        }

        private void SearchViewInfo_Load(object sender, EventArgs e)
        {
            ListView1.Columns.Add("字段", 80, HorizontalAlignment.Center);
            ListView1.Columns.Add("数值", 130, HorizontalAlignment.Left);
            //遍历第一个要素的字段用于给列头赋值（字段的名称）
            for (int m = 0; m < pFeature.Fields.FieldCount; m++)
            {
                ListViewItem lv = ListView1.Items.Add(pFeature.Fields.get_Field(m).AliasName);
                lv.SubItems.Add(pFeature.get_Value(m).ToString());
            }
            
        }


    }
}