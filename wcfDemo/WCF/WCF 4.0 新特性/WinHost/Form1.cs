using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.ServiceModel;
using System.ServiceModel.Description;

namespace WinHost
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Load += new EventHandler(Form1_Load);
        }

        void Form1_Load(object sender, EventArgs e)
        {
            ServiceHost host = new ServiceHost(typeof(ServiceLib.Demo));

            // 强制为每个 baseAddresses 生成默认 endpoint（即使显示地指定 endpoint 也会生成默认的 endpoint）
            // host.AddDefaultEndpoints();

            if (host.State != CommunicationState.Opening)
                host.Open();

            txtMsg.Text += string.Format("endpoint 的数量: {0}", host.Description.Endpoints.Count);
            txtMsg.Text += "\r\n";

            foreach (ServiceEndpoint se in host.Description.Endpoints)
            {
                txtMsg.Text += string.Format("A: {0}\r\nB: {1}\r\nC: {2}",
                    se.Address, se.Binding.Name, se.Contract.Name);
            }
        }
    }
}
