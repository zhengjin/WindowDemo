using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace ArcGisView
{
    public partial class attribute : Form
    {
        private IFeatureLayer pFeatureLayer;
        public attribute(IFeatureLayer ftl)
        {
            InitializeComponent();
            pFeatureLayer = ftl;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;
            //创建过滤器
            IQueryFilter pQueryFilter = new QueryFilterClass();
            //设置过滤器对象的查询条件
            pQueryFilter.WhereClause = dataGridView1.Columns[0].HeaderText + "=" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

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