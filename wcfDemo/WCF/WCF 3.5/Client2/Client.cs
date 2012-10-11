using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.ServiceModel;

namespace Client2
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void btnSayHello_Click(object sender, EventArgs e)
        {
            new Binding.Hello().SayHello(txtName.Text);
        }

        private void btnWithOneWay_Click(object sender, EventArgs e)
        {
            new Message.OneWay().HelloWithOneWay();
        }

        private void btnWithoutOneWay_Click(object sender, EventArgs e)
        {
            new Message.OneWay().HelloWithoutOneWay();
        }

        private void btnDuplex_Click(object sender, EventArgs e)
        {
            new Message.Duplex().HelloDulex(txtName.Text);
        }

        private void btnHelloStreamed_Click(object sender, EventArgs e)
        {
            new Message.Streamed().HelloStreamed(txtSourcePath.Text, txtDestination.Text);
        }

        private void btnDuplexReentrant_Click(object sender, EventArgs e)
        {
            new Message.DuplexReentrant().HelloDulexReentrant(txtName.Text);
        }

        private void btnMSMQ_Click(object sender, EventArgs e)
        {
            new Message.MSMQ().HelloMSMQ(txtName.Text);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            txtSourcePath.Text = openFileDialog1.FileName;
        }

        private void btnOpenDialog_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
    }
}
