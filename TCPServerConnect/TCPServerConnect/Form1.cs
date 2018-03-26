using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace TCPServerConnect
{
    public partial class Form1 : Form
    {
        private TcpListener server;
        private TcpClient client;

        public Form1()
        {
            InitializeComponent();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress ipadress = null;

            try
            {
                ipadress = IPAddress.Parse(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Incorrect IP adress format", "Error");
                textBox1.Text = string.Empty;
                return;
            }

            int port = System.Convert.ToInt16(numericUpDown1.Value);

            try
            {
                server = new TcpListener(ipadress, port);
                server.Start();
                client = server.AcceptTcpClient();
                IPEndPoint ip = (IPEndPoint)client.Client.RemoteEndPoint;
                listBox1.Items.Add("[" + ip.ToString() + "]: Connection established");
                button1.Enabled = false;
                button2.Enabled = true;
                client.Close();
                server.Stop();
            }
            catch (Exception ex)
            {
                listBox1.Items.Add("Server initialization error");
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            server.Stop();
            client.Close();
            listBox1.Items.Add("Server shut down");
            button1.Enabled = true;
            button2.Enabled = false;
        }
    }
}
