using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

using NSoup.Nodes;
using Newtonsoft.Json;


namespace F.A.P.I
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string aa = a(textBox1.Text);
            listBox1.Items.Add(aa);

        }





        static string a(string keywordURL)
        {
            string str_url = null;
            try
            {
                //下载网页源代码

                WebClient webClient = new WebClient();
                //string keywordURL = "魔法科高校的劣等生";

                string ktxp = "http://bt.ktxp.com";
                string url = "http://bt.ktxp.com/search.php?keyword=" + keywordURL
                        + "&order=completed";

                string htmlString = Encoding.GetEncoding("utf-8").GetString(webClient.DownloadData(url));

                Document doc = NSoup.NSoupClient.Parse(htmlString);


                Element torrent = doc.Select("td[class=ltext ttitle]").First()
           .Select("a[class=quick-down cmbg]").First();
                str_url = ktxp + torrent.Attr("href");

            }
            catch (Exception e)
            {
                Console.Write(e);

            }
            return str_url;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();
            string url = "http://bgmlist.com/json/bangumi-1407.json";
            byte[] b = webClient.DownloadData(url);
            string jsonText = Encoding.UTF8.GetString(b, 0, b.Length);

            jsonText = jsonText.Replace("\n", "");
            jsonText = jsonText.Replace("\"[", "[");

            JsonReader reader = new JsonTextReader(new StringReader(jsonText));
            url = url + "";

        }


        public string Utf8ToGB2312(string utf8String)
        {
            Encoding fromEncoding = Encoding.UTF8;
            Encoding toEncoding = Encoding.GetEncoding("gb2312");
            return EncodingConvert(utf8String, fromEncoding, toEncoding);
        }

        public string EncodingConvert(string fromString, Encoding fromEncoding, Encoding toEncoding)
        {
            byte[] fromBytes = fromEncoding.GetBytes(fromString);
            byte[] toBytes = Encoding.Convert(fromEncoding, toEncoding, fromBytes);

            string toString = toEncoding.GetString(toBytes);
            return toString;
        }
        public string GB2312ToUtf8(string gb2312String)
        {
            Encoding fromEncoding = Encoding.GetEncoding("gb2312");
            Encoding toEncoding = Encoding.UTF8;
            return EncodingConvert(gb2312String, fromEncoding, toEncoding);
        }
    }
}
