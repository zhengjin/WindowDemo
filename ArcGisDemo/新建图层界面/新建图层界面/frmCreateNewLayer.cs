using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Carto;

namespace 新建图层界面
{
    public partial class frmCreateNewLayer : Form
    {
        string strFullPath = "";
        //string strLayerType = "";
        private IFeatureLayer m_pFeatureLayer = new FeatureLayerClass();
        public frmCreateNewLayer()
        {
            InitializeComponent();
            cbbLayerType.Items.Add("point");
            cbbLayerType.Items.Add("polyline");
            cbbLayerType.Items.Add("polygon");
            cbbCoordinateSystem.Items.Add("北京54坐标系");
            cbbCoordinateSystem.Items.Add("西安80坐标系");
            cbbfendai.Items.Add("三度带");
            cbbfendai.Items.Add("六度带");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Filter = "Shapefile(*.shp)|*.shp";
            saveFileDialog.Title = "输出图层位置";
            saveFileDialog.ShowDialog();
            saveFileDialog.Dispose();
            textBoxLocation.Text = saveFileDialog.FileName;
            strFullPath = saveFileDialog.FileName;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ////检查输入是否合法
            if (cbbfendai.Text == "三度带")
            {
                if (Convert.ToInt32(cbbnumber.Text) < 24 || Convert.ToInt32(cbbnumber.Text) > 45)
                {
                    MessageBox.Show("请输入正确带号");
                    return;
                }
            }
            else if (cbbfendai.Text=="六度带")
            {
                if (Convert.ToInt32(cbbnumber.Text) < 13 || Convert.ToInt32(cbbnumber.Text) > 23)
                {
                    MessageBox.Show("请输入正确带号");
                    return;
                }
            }
            ////检查输入是否合法
            ////获取文件名
            if (strFullPath.Length == 0) return;
            int index = strFullPath.LastIndexOf("\\");
            string strFolder = strFullPath.Substring(0, index);
            string strFileName = strFullPath.Substring(index + 1);
            string strShapeFieldName = "Shape";
            ////获取文件名
            //const string strFolder = "e:\\";
            //const string strFileName = "myshapefile";
            //const string strShapeFieldName = "SHAPE";

            IFeatureWorkspace pFWS;
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
            pFWS = pWorkspaceFactory.OpenFromFile(strFolder, 0) as IFeatureWorkspace;
            IFeatureClass pFeatClass = null;

            IFields pFields = new FieldsClass() as IFields;
            IFieldsEdit pFieldsEdit = pFields as IFieldsEdit;

            IField pField = new FieldClass() as IField;
            IFieldEdit pFieldEdit = pField as IFieldEdit;
            pFieldEdit.Name_2 = strShapeFieldName;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;

            IGeometryDef pGeomDef = new GeometryDefClass() as IGeometryDef;
            IGeometryDefEdit pGeomDefEdit = pGeomDef as IGeometryDefEdit;

            /////创建的图层类型
            if (cbbLayerType.Text == "point")
            {
                pGeomDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            }
            else if (cbbLayerType.Text == "polyline")
            {
                pGeomDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
            }
            else if (cbbLayerType.Text == "polygon")
            {
                pGeomDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            }
            /////创建的图层类型

            //SpatialReferenceEnvironment spatialReferenceEnvironment = new SpatialReferenceEnvironment();
            //IProjectedCoordinateSystem projectedCoordinateSystem = spatialReferenceEnvironment.CreateProjectedCoordinateSystem(21479);//(int)esriSRProjCSType.esriSRProjCS_Beijing1954GK_19N

            //pGeomDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();//ProjectedCoordinateSystemClass
            IProjectedCoordinateSystem projectedCoordinateSystem=GetProjectedCoordinateSystem();
            pGeomDefEdit.SpatialReference_2 = projectedCoordinateSystem;//采用的坐标系统
            pFieldEdit.GeometryDef_2 = pGeomDef;
            pFieldsEdit.AddField(pField);


            pField = new FieldClass();
            pFieldEdit = pField as IFieldEdit;
            pFieldEdit.Length_2 = 30;
            pFieldEdit.Name_2 = "Label";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            pFieldsEdit.AddField(pField);

            UID uid = new UIDClass() as UID;
            uid = null;
            UID uid2 = new UIDClass() as UID;
            uid2 = null;

            try
            {
                pFeatClass = pFWS.CreateFeatureClass(strFileName, pFields, uid, uid, esriFeatureType.esriFTSimple, strShapeFieldName, "") as IFeatureClass;
                m_pFeatureLayer.FeatureClass = pFeatClass;
                m_pFeatureLayer.Name = m_pFeatureLayer.FeatureClass.AliasName;
                MessageBox.Show("图层创建成功");
            }
            catch(Exception ex)
            {
                string strException = ex.Message;
                MessageBox.Show(strException);
            }
        }

        private IProjectedCoordinateSystem GetProjectedCoordinateSystem()
        {
            //以下结果均不加带号
            //if(北京54and三度带）2397+带号   4
            //if（北京54and六度带）21400+带号
            //if（西安80and三度带）2345+带号  4
            //if（西安80and六度带）2325+带号  4
            //创建投影坐标系
            try
            {
                SpatialReferenceEnvironment spatialReferenceEnvironment = new SpatialReferenceEnvironment();
                IProjectedCoordinateSystem projectedCoordinateSystem = new UnknownCoordinateSystemClass() as IProjectedCoordinateSystem;

                if (cbbCoordinateSystem.Text == "北京54坐标系" && cbbfendai.Text == "三度带")
                {
                    projectedCoordinateSystem = spatialReferenceEnvironment.CreateProjectedCoordinateSystem(2397 + Convert.ToInt32(cbbnumber.Text));
                }
                else if (cbbCoordinateSystem.Text == "北京54坐标系" && cbbfendai.Text == "六度带")
                {
                    projectedCoordinateSystem = spatialReferenceEnvironment.CreateProjectedCoordinateSystem(21400 + Convert.ToInt32(cbbnumber.Text));
                }
                else if (cbbCoordinateSystem.Text == "西安80坐标系" && cbbfendai.Text == "三度带")
                {
                    projectedCoordinateSystem = spatialReferenceEnvironment.CreateProjectedCoordinateSystem(2345 + Convert.ToInt32(cbbnumber.Text));
                }
                else if (cbbCoordinateSystem.Text == "西安80坐标系" && cbbfendai.Text == "六度带")
                {
                    projectedCoordinateSystem = spatialReferenceEnvironment.CreateProjectedCoordinateSystem(2325 + Convert.ToInt32(cbbnumber.Text));
                }

                return projectedCoordinateSystem;
            }
            catch (Exception ex)
            {
                string exception = ex.Message;
                MessageBox.Show(exception);
                return null;
            }
        }

        public IFeatureLayer GetFeatureLayer()
        {
            return m_pFeatureLayer;
        }

        private void cbbfendai_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbnumber.Items.Clear();
            if (cbbfendai.Text == "三度带")
            {
                for (int i = 24; i < 46; i++)
                {
                    cbbnumber.Items.Add(i);
                }
            }
            else if (cbbfendai.Text == "六度带")
            {
                for (int i = 13; i < 24; i++)
                {
                    cbbnumber.Items.Add(i);
                }
            }
        }
    }
}