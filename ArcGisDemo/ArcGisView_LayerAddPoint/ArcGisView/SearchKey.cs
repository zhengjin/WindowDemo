using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Geodatabase;

namespace ArcGisView
{
    public partial class SearchKey : Form
    {
        private AxMapControl axMapControl1;
        private IFeatureLayer pFeatureLayer;
        int LayerCount = 0;
        int DisplayFiledNum = 0;
        public SearchKey(AxMapControl axMapCtrol)
        {
            InitializeComponent();
            axMapControl1 = axMapCtrol;
            LayerCount = axMapControl1.LayerCount;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pFeatureLayer = this.axMapControl1.get_Layer(comboBox1.SelectedIndex) as IFeatureLayer;
            label4.Text = pFeatureLayer.DisplayField;
        }

        private void SearchKey_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                comboBox1.Items.Insert(i, axMapControl1.get_Layer(i).Name);//获得层的名字并添加到下拉框中

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (axMapControl1.LayerCount > LayerCount)
            {
                axMapControl1.DeleteLayer(axMapControl1.LayerCount);
            }
            //if (textBox1.Text != "")
            //{
                List<IFeature> pFList = new List<IFeature>();
                //QI到FeatureSelection
                IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;
                //创建过滤器
                IQueryFilter pQueryFilter = new QueryFilterClass();
                //设置过滤器对象的查询条件
                pQueryFilter.WhereClause = label4.Text + " like '%" + textBox1.Text + "%'";

                IFeatureCursor pFeatureCursor = pFeatureLayer.Search(pQueryFilter, false);
                IFeature pFeature = pFeatureCursor.NextFeature();
                while (pFeature != null)
                {
                    //获取要素对象
                    pFList.Add(pFeature);
                    pFeature = pFeatureCursor.NextFeature();
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);

                if (pFList.Count > 0)
                {
                    dataGridView1.RowCount = pFList.Count + 1;
                    //设置边界风格
                    dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
                    //设置列数
                    dataGridView1.ColumnCount = pFList[0].Fields.FieldCount;
                    //遍历第一个要素的字段用于给列头赋值（字段的名称）
                    for (int m = 0; m < pFList[0].Fields.FieldCount; m++)
                    {
                        dataGridView1.Columns[m].HeaderText = pFList[0].Fields.get_Field(m).AliasName;
                        if (pFList[0].Fields.get_Field(m).AliasName == label4.Text)
                        {
                            DisplayFiledNum = m;
                        }
                    }
                    //遍历要素
                    for (int i = 0; i < pFList.Count; i++)
                    {
                        pFeature = pFList[i];
                        for (int j = 0; j < pFeature.Fields.FieldCount; j++)
                        {
                            //填充字段值
                            dataGridView1[j, i].Value = pFeature.get_Value(j).ToString();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("没有任何数据");
                }
            //}
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;
            //创建过滤器
            IQueryFilter pQueryFilter = new QueryFilterClass();
            //设置过滤器对象的查询条件
            pQueryFilter.WhereClause = dataGridView1.Columns[0].HeaderText+ "=" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

            IFeatureCursor pFeatureCursor = pFeatureLayer.Search(pQueryFilter, false);
            IFeature pFeature = pFeatureCursor.NextFeature();

            SearchViewInfo frm = null;
            if (frm == null || frm.IsDisposed)
                frm = new SearchViewInfo(pFeature);

            frm.Show();
            frm.TopMost = true;
        }
    }
}