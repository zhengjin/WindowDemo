using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.Net.Sockets;
using System.Net;
using XL.ServiceAPI;
using System.Collections.ObjectModel;

namespace XL.Client
{
    public partial class MainForm : Form
    {
        public List<BaseForm> FormHistory;
        public MainForm()
        {
            var loginForm = new LoginForm();
            var result = loginForm.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK)
            {
                System.Environment.Exit(0);
            }
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            FormHistory = new List<BaseForm>();
            if (!Utils.IsInDesignMode())
            {
                InitMenu();
            }
        }
    }
}
