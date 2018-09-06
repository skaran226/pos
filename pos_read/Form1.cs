using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace pos_read
{
    public partial class Form1 : Form
    {
        public static Form1 fm;
        public Form1()
        {

            InitializeComponent();
            fm = this;
        }

        delegate void SetRichTextValue(object sender, SerialDataReceivedEventArgs e);
        SerialPort sp = null;
        private void button1_Click(object sender, EventArgs e)
        {
            sp = new SerialPort();
            sp.PortName = textBox1.Text;
            sp.BaudRate = 9600;
            sp.StopBits = StopBits.One;
            sp.Parity = Parity.None;
            sp.DataBits = 8;
            sp.DataReceived += new SerialDataReceivedEventHandler(pos_datarecived);
            sp.Open();
        }

        private void pos_datarecived(object sender, SerialDataReceivedEventArgs e)
        {
            int iBytesToRead = fm.sp.BytesToRead;
            byte[] comBuffer = new byte[iBytesToRead];
            int i;
            // byte[] buffer = new byte[readByte];

            fm.sp.Read(comBuffer, 0, iBytesToRead);


            foreach (byte ch in comBuffer)
            {
                if (fm.richTextBox1.InvokeRequired)
                {

                    SetRichTextValue st = new SetRichTextValue(pos_datarecived);
                    fm.Invoke(st, new object[] { sender, e });

                }
                else
                {
                    fm.richTextBox1.Text += (char)ch;
                }

            }
        }
    }
}
