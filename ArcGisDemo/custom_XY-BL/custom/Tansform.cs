using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace custom
{
    public partial class Tansform : UserControl
    {
        private TextBox _x = new TextBox();
        private TextBox _y = new TextBox();
        private TextBox _lat = new TextBox();
        private TextBox _lon = new TextBox();
        public Tansform()
        {
            InitializeComponent();
            
        }
        public Tansform(TextBox Lat,TextBox Lon,TextBox X,TextBox Y)
        {
            InitializeComponent();
            _x = X;
            _y = Y;
            _lat = Lat;
            _lon = Lon;
        }

        private void btnBLtoXY_Click(object sender, EventArgs e)
        {
            TextPass();
            #region BLtoXY
            String csStr = cmbCoordinateSystem.Text;
            string numberNameStr = cmbNumberName.Text;
            object fromCSType = null;
            object toCSType = null;


            double b = Utility.DMSToDegree(_lon.Text);
            double l = Utility.DMSToDegree(_lat.Text);

            if (csStr != null && numberNameStr != null)
            {
                if (b == -1 || l == -1)
                {
                    MessageBox.Show("BL坐标不符合规范,请重新填写！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }

                if (csStr == "北京54坐标系")
                {
                    fromCSType = CSType.GCS_Beijing_1954;
                    if (numberNameStr == "3度分带")
                    {
                        if (double.Parse(_lon.Text.Trim().Substring(0, 3)) < 103.5 || double.Parse(_lon.Text.Trim().Substring(0, 3)) > 112.5)
                        {
                            MessageBox.Show("请确保经度在103.5与112.5之间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            return;
                        }
                        else if (double.Parse(_lon.Text.Trim().Substring(0, 3)) >= 103.5 && double.Parse(_lon.Text.Trim().Substring(0, 3)) < 106.5)
                        {
                            toCSType = CSType.Beijing_1954_3_Degree_GK_Zone_35;
                        }
                        else if (double.Parse(_lon.Text.Trim().Substring(0, 3)) >= 106.5 && double.Parse(_lon.Text.Trim().Substring(0, 3)) < 109.5)
                        {
                            toCSType = CSType.Beijing_1954_3_Degree_GK_Zone_36;
                        }
                        else
                        {
                            toCSType = CSType.Beijing_1954_3_Degree_GK_Zone_37;
                        }
                    }
                    else if (numberNameStr == "6度分带")
                    {
                        if (double.Parse(_lon.Text.Trim().Substring(0, 3)) < 102 || double.Parse(_lon.Text.Trim().Substring(0, 3)) > 114)
                        {
                            MessageBox.Show("请确保经度在102与114之间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            return;
                        }
                        else if (double.Parse(_lon.Text.Trim().Substring(0, 3)) >= 102 && double.Parse(_lon.Text.Trim().Substring(0, 3)) <= 108)
                        {
                            toCSType = CSType.Beijing_1954_GK_Zone_18;
                        }
                        else
                        {
                            toCSType = CSType.Beijing_1954_GK_Zone_19;
                        }
                    }
                }
                else if (csStr == "西安80坐标系")
                {
                    fromCSType = CSType.GCS_Xian_1980;
                    if (numberNameStr == "3度分带")
                    {
                        if (double.Parse(_lon.Text.Trim().Substring(0, 3)) < 103.5 || double.Parse(_lon.Text.Trim().Substring(0, 3)) > 112.5)
                        {
                            MessageBox.Show("请确保经度在103.5与112.5之间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            return;
                        }
                        else if (double.Parse(_lon.Text.Trim().Substring(0, 3)) >= 103.5 && double.Parse(_lon.Text.Trim().Substring(0, 3)) < 106.5)
                        {
                            toCSType = CSType.Xian_1980_3_Degree_GK_Zone_35;
                        }
                        else if (double.Parse(_lon.Text.Trim().Substring(0, 3)) >= 106.5 && double.Parse(_lon.Text.Trim().Substring(0, 3)) < 109.5)
                        {
                            toCSType = CSType.Xian_1980_3_Degree_GK_Zone_36;
                        }
                        else
                        {
                            toCSType = CSType.Xian_1980_3_Degree_GK_Zone_37;
                        }
                    }
                    else if (numberNameStr == "6度分带")
                    {
                        if (double.Parse(_lon.Text.Trim().Substring(0, 3)) < 102 || double.Parse(_lon.Text.Trim().Substring(0, 3)) > 114)
                        {
                            MessageBox.Show("请确保经度在102与114之间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            return;
                        }
                        else if (double.Parse(_lon.Text.Trim().Substring(0, 3)) >= 102 && double.Parse(_lon.Text.Trim().Substring(0, 3)) < 108)
                        {
                            toCSType = CSType.Xian_1980_GK_Zone_18;
                        }
                        else
                        {
                            toCSType = CSType.Xian_1980_GK_Zone_19;
                        }
                    }
                }

                double[] point = new double[2];

                if (fromCSType != null && toCSType != null)
                {
                    point = Utility.TransForm(l, b, (CSType)fromCSType, (CSType)toCSType);
                    if (point != null)
                    {
                        _x.Text = point[1].ToString("#0.00");
                        _y.Text = point[0].ToString("#0.00");
                    }
                }

            }
            else
            {
                MessageBox.Show("请填写坐标相关信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            #endregion
        }

        private void btnXYtoBL_Click(object sender, EventArgs e)
        {
            TextPass();
            #region  XYtoBL
            string csStr = cmbCoordinateSystem.Text;
            string numberNameStr = cmbNumberName.Text;
            object fromCSType = null;
            object toCSType = null;

            double x = Utility.GetValue(_x.Text);
            double y = Utility.GetValue(_y.Text);

            if (csStr != "" && numberNameStr != "")
            {
                if (x == -1 || y == -1)
                {
                    MessageBox.Show("存在不是数字类型XY坐标，请重新填写！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }
                else if (_y.Text.Length < 2)
                {
                    MessageBox.Show("y坐标输入格式不正确，请重新填写！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }
                if (csStr == "北京54坐标系")
                {
                    toCSType = CSType.GCS_Beijing_1954;
                    if (numberNameStr == "3度分带")
                    {
                        if (_y.Text.Substring(0, 2) != "35" && _y.Text.Substring(0, 2) != "36" && _y.Text.Substring(0, 2) != "37")
                        {
                            MessageBox.Show("y坐标输入格式不正确，请从新填写", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }
                        else if (_y.Text.Substring(0, 2) == "35")
                        {
                            fromCSType = CSType.Beijing_1954_3_Degree_GK_Zone_35;
                        }
                        else if (_y.Text.Substring(0, 2) == "36")
                        {
                            fromCSType = CSType.Beijing_1954_3_Degree_GK_Zone_36;
                        }
                        else
                        {
                            fromCSType = CSType.Beijing_1954_3_Degree_GK_Zone_37;
                        }
                    }
                    else if (numberNameStr == "6度分带")
                    {
                        if (_y.Text.Substring(0, 2) != "18" && _y.Text.Substring(0, 2) != "19")
                        {
                            MessageBox.Show("y坐标输入格式不正确，请从新填写", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }
                        else if (_y.Text.Substring(0, 2) == "18")
                        {
                            fromCSType = CSType.Beijing_1954_GK_Zone_18;
                        }
                        else
                        {
                            fromCSType = CSType.Beijing_1954_GK_Zone_19;
                        }
                    }
                }
                else if (csStr == "西安80坐标系")
                {
                    toCSType = CSType.GCS_Xian_1980;
                    if (numberNameStr == "3度分带")
                    {
                        if (_y.Text.Substring(0, 2) != "35" && _y.Text.Substring(0, 2) != "36" && _y.Text.Substring(0, 2) != "37")
                        {
                            MessageBox.Show("y坐标输入格式不正确，请从新填写", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }
                        else if (_y.Text.Substring(0, 2) == "35")
                        {
                            fromCSType = CSType.Xian_1980_3_Degree_GK_Zone_35;
                        }
                        else if (_y.Text.Substring(0, 2) == "36")
                        {
                            fromCSType = CSType.Xian_1980_3_Degree_GK_Zone_36;
                        }
                        else
                        {
                            fromCSType = CSType.Xian_1980_3_Degree_GK_Zone_37;
                        }
                    }
                    else if (numberNameStr == "6度分带")
                    {
                        if (_y.Text.Substring(0, 2) != "18" && _y.Text.Substring(0, 2) != "19")
                        {
                            MessageBox.Show("y坐标输入格式不正确，请从新填写", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }
                        else if (_y.Text.Substring(0, 2) == "18")
                        {
                            fromCSType = CSType.Xian_1980_GK_Zone_18;
                        }
                        else
                        {
                            fromCSType = CSType.Xian_1980_GK_Zone_19;
                        }
                    }
                }

                double[] point = new double[2];
                if (fromCSType != null && toCSType != null)
                {
                    point = Utility.TransForm(x, y, (CSType)fromCSType, (CSType)toCSType);
                    if (point != null)
                    {
                        _lon.Text = Utility.DegreeToDMS(point[0]);
                        _lat.Text = Utility.DegreeToDMS(point[1]);
                    }
                }
            }
            else
            {
                MessageBox.Show("请填写坐标相关信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            #endregion
        }

        private void TextPass()
        {
            _lat = (TextBox)this.Parent.Controls["LatTBox"];
            _lon = (TextBox)this.Parent.Controls["LonTBox"];
            _x = (TextBox)this.Parent.Controls["X"];
            _y = (TextBox)this.Parent.Controls["Y"];
        }
    }
}
