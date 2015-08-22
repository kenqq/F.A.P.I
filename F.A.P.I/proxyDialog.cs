using MyProg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace F.A.P.I
{
    public partial class proxyDialog : Form
    {
        public static string appdataFAPI = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\F.A.P.I.2";
        public static string configInI = Path.Combine(appdataFAPI, "config.ini");
        public static IniFile MyIni = new IniFile(configInI);

        public proxyDialog()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = comboBox1.FindStringExact(MyIni.Read("ProxyType"));
            textBox1.Text = MyIni.Read("ProxyAddress");
            textBox2.Text = MyIni.Read("ProxyPort");
            textBox3.Text = MyIni.Read("ProxyUserName");
            textBox4.Text = MyIni.Read("ProxyPassword");
            //MyIni.Read("FullProxyAddress");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyIni.Write("ProxyType", comboBox1.SelectedItem.ToString());
            MyIni.Write("ProxyAddress", textBox1.Text);
            MyIni.Write("ProxyPort", textBox2.Text);
            MyIni.Write("FullProxyAddress", textBox1.Text+":"+textBox2.Text);
            MyIni.Write("ProxyUserName", textBox3.Text);
            MyIni.Write("ProxyPassword", textBox4.Text);
            this.Close();
        }
    }
}
