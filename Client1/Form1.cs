using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Client1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Socket sock;
        bool ok = false;
        private void SendMsgBTN_Click(object sender, EventArgs e)
        {
            byte[] bytes = new byte[1024];
            if (!ok)
            {
                IPAddress host = IPAddress.Parse("127.0.0.1");
                IPEndPoint hostep = new IPEndPoint(host, 9050);
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPTxt.Text = "127.0.0.1";
                sock.Connect(hostep);
                InfoTxt.Text += "Socket connected to " + sock.RemoteEndPoint.ToString() + "\n";
                SendMsgBTN.Text = "SendMessage";
                MsgTxt.ReadOnly = false;
                ok = true;
            }
            // Encode the data string into a byte array+
            byte[] msg = Encoding.ASCII.GetBytes(MsgTxt.Text);
            // Send the data through the socket.
            int bytesSent = sock.Send(msg);
            // Receive the response from the remote device.
            int bytesRec = sock.Receive(bytes);
            InfoTxt.Text += "Echoed test = " + Encoding.ASCII.GetString(bytes, 0, bytesRec) + "\n";
            if (MsgTxt.Text == "exit")
            {
                SendMsgBTN.Text = "Connect";
                MsgTxt.ReadOnly = true;
                sock.Close();
                ok = false;
            }
            MsgTxt.Clear();
        }
    }
}