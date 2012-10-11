using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArcGISAddInDemo02
{
    public partial class AlterForm : Form
    {
        public AlterForm(int x,int y)
        {
            InitializeComponent();
            label1.Text = "坐标：X：" + x.ToString()+"  Y："+y.ToString();
        }
    }
}
