using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;

namespace ArcGisView
{
    public partial class updateFeature : Form
    {
        private IFeature pFeature;
        //private string OIdName;
        private IFeatureLayer pFeatureLayer;
        public updateFeature(IFeatureLayer ifl, IFeature ifeat)
        {
            InitializeComponent();
            pFeature = ifeat;
            pFeatureLayer = ifl;
        }

        private void updateFeature_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> FeatInfoTable = new Dictionary<string, string>();
            for (int i = 0; i < pFeature.Fields.FieldCount; i++)
            {
                //几何图形
                IGeometry shape = pFeature.Shape;
                string fieldValue = string.Empty;
                if (pFeature.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                {
                    fieldValue = shape.GeometryType.ToString().Substring(12);
                }
                else
                {
                    fieldValue = pFeature.get_Value(i).ToString();
                }
                FeatInfoTable.Add(pFeature.Fields.get_Field(i).Name, fieldValue);
            }

            
            foreach (KeyValuePair<string, string> objDE in FeatInfoTable)
            {

                    string[] PropRow = new string[] { objDE.Key.ToLower(), objDE.Value };

                    dataGridView1.Rows.Add(PropRow);

            }

            //设置边界风格
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> FieldTable = new Dictionary<string, string>();
            
            foreach (DataGridViewRow PropRow in this.dataGridView1.Rows)
            {
                string PropKey = PropRow.Cells[0].Value.ToString();
                string PropValue = "";
                if (PropRow.Cells[1].Value != null)
                {
                    PropValue = PropRow.Cells[1].Value.ToString();
                }
                else
                {
                    PropValue = "";
                }

                FieldTable.Add(PropKey, PropValue);
            }

            ArcGisPublic agp = new ArcGisPublic();
            agp.UpdateFeatureValue(FieldTable, pFeatureLayer as IFeatureLayer);
        }
    }
}