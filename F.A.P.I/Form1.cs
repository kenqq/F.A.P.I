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
using System.Collections;
using System.Threading;
using System.Globalization;
using System.Diagnostics;

using NSoup.Nodes;
using Newtonsoft.Json;

namespace F.A.P.I
{
    public partial class Form1 : Form
    {
        public List<JsonClass> jsonList;
        public List<archive> archiveList;
        public string bgmlist = "http://bgmlist.com/";
        public int day = (int)DateTime.Now.DayOfWeek;
        public int month = (int)DateTime.Now.Month;
        public int year = (int)DateTime.Now.Year;
        public bool is_grid_load = false;
        public bool initflag = false;
        public string jsonName = "";
        public SynchronizationContext _syncContext = null;
        public string downloadsoftpath = "";

        public Form1()
        {
            InitializeComponent();
            //获取UI线程同步上下文  
            _syncContext = SynchronizationContext.Current;
            loadfansub();
            readArchiveJson();
            readJson(month);
            initcombobox();
            initflag = true;
            comboBox1comboBox2_SelectedIndexChanged();
            downloadsoftpath = readdownloadsoftpath();
            if (downloadsoftpath!=null&& downloadsoftpath != "")
            Process.Start(@downloadsoftpath);
        }
        private void initcombobox()//初始化列表
        {
            comboBox1.SelectedIndex = day;
            comboBox3.Items.Clear();
            foreach (archive a in archiveList)
            {
                comboBox3.Items.Add(a.year);
            }
            for (var i = 0; i < comboBox3.Items.Count; ++i)
            {
                if (comboBox3.Items[i].ToString() == year + "")
                {
                    comboBox3.SelectedIndex = i;
                }
            }
            comboBox2.Items.Clear();
            foreach (months m in (from a in archiveList
                                  where a.year == year + ""
                                  select a).ToList()[0].months)
            {
                comboBox2.Items.Add(m.month);
            }
            comboBox2.SelectedIndex = month >= 10 ? 3 : month >= 7 ? 2 : month >= 4 ? 1 : month >= 1 ? 0 : 0;
        }
        private void loadfansub()//读取& 创建字幕组 并且加载
        {
            if (System.IO.File.Exists(@"fansub.txt"))
            {
                ArrayList al = readFansub();
                ComboBox CB = new ComboBox();
                foreach (string a in al)
                {
                    CB.Items.Add(a);
                }
                ((DataGridViewComboBoxColumn)dataGridView1.Columns["fansub"]).DataSource = CB.Items;
            }
            else
            {
                //第一行空格
                string fansub = @" 
极影
HKG
梦幻恋樱
千夏
DHR動研
动漫国
轻之国度
幻樱
雪飘工作室
SOSG
WOLF
澄空学园
华盟
恶魔岛
异域
◆漫游FREEWIND工作室
猪猪
SGS曙光社
X2
琵琶行
动音漫影
悠哈C9
动漫先锋
KPDM
流雲
光荣";
                System.IO.File.WriteAllText(@"fansub.txt", fansub);
                ArrayList al = readFansub();
                ComboBox CB = new ComboBox();
                foreach (string a in al)
                {
                    CB.Items.Add(a);
                }
                ((DataGridViewComboBoxColumn)dataGridView1.Columns["fansub"]).DataSource = CB.Items;
            }
        }
        string getTorrent(string keywordURL)//
        {
            string str_url = null;
            keywordURL = keywordURL.Replace("-", "");
            keywordURL = keywordURL.Replace("\"", "");

            try
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                //下载网页源代码
                WebClient webClient = new WebClient();
                string ktxp = "http://bt.ktxp.com";
                string url = "http://bt.ktxp.com/search.php?keyword=" + keywordURL
                        + "&order=addate";
                string htmlString = Encoding.GetEncoding("utf-8").GetString(webClient.DownloadData(url));
                Document doc = NSoup.NSoupClient.Parse(htmlString);

                int countmax = 0;
                int trCount = doc.Select("tbody").First().Select("tr").Count;
                for (var i = 0; i < trCount; ++i)
                {
                    if (trCount == 1)
                    {
                        Element isnulldoc = doc.Select("tbody").First().Select("tr:eq(" + i + ")").First().Select("td").First();
                        if (isnulldoc.Text() == "没有可显示资源")
                            return "nothing";
                    }
                    Element type_doc = doc.Select("tbody").First().Select("tr:eq(" + i + ")").First().Select("td:eq(1) a").First();
                    string type = type_doc.Text();//类型
                    if (type != "新番连载")
                        continue;

                    Element time_doc = doc.Select("tbody").First().Select("tr:eq(" + i + ")").First().Select("td:eq(0)").First();
                    DateTime dateVal = DateTime.ParseExact(time_doc.Attr("title"), "yyyy/MM/dd HH:mm", culture);//片时间
                    string monthtmp = comboBox2.Items[comboBox2.SelectedIndex].ToString().Length < 2 ? "0" + comboBox2.Items[comboBox2.SelectedIndex].ToString() : comboBox2.Items[comboBox2.SelectedIndex].ToString();
                    string asdasdsad = comboBox3.Items[comboBox3.SelectedIndex].ToString() + "/"
                        + monthtmp + "/01 00:00";
                    DateTime currentSeason = DateTime.ParseExact(asdasdsad, "yyyy/MM/dd HH:mm", culture);//本季时间
                    if (DateTime.Compare(dateVal, currentSeason) < 0)//片要比本季时间大
                        continue;

                    Element countt = doc.Select("tbody").First().Select("tr:eq(" + i + ")").First().Select("td:eq(6)").First();
                    int count = Int32.Parse(countt.Text());
                    if (count >= countmax)
                    {
                        Element torrent = doc.Select("tbody").First().Select("tr:eq(" + i + ")").First()
                                 .Select("td[class=ltext ttitle]").First().Select("a[class=quick-down cmbg]").First();//种子地址
                        countmax = count;
                        str_url = ktxp + torrent.Attr("href");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return str_url;
        }
        private ArrayList readFansub()
        {
            StreamReader sr = new StreamReader("fansub.txt", Encoding.GetEncoding("UTF-8"));
            String line;
            StringBuilder sb = new StringBuilder();
            ArrayList al = new ArrayList();
            while ((line = sr.ReadLine()) != null)
            {
                al.Add(line);
            }
            sr.Close();
            return al;
        }
        private void readArchiveJson()
        {
            if (System.IO.File.Exists(@"archive.json"))
            {
                readLocalArchiveJson();
            }
            else
            {
                WebClient webClient = new WebClient();
                string url = "http://bgmlist.com/json/archive.json";
                byte[] b = webClient.DownloadData(url);
                string jsonText = Encoding.UTF8.GetString(b, 0, b.Length);
                archiveList = JsonConvert.DeserializeObject<List<archive>>(jsonText);
                System.IO.File.WriteAllText(@"archive.json", jsonText);
            }
        }
        private void readLocalArchiveJson()
        {
            StreamReader sr = new StreamReader("archive.json", Encoding.GetEncoding("UTF-8"));
            String line;
            StringBuilder sb = new StringBuilder();
            string jsonText;
            while ((line = sr.ReadLine()) != null)
            {
                sb.Append(line);
            }
            sr.Close();
            jsonText = sb.ToString();
            archiveList = JsonConvert.DeserializeObject<List<archive>>(jsonText);
        }
        private void readJson(int month)
        {
            Newtonsoft.Json.Linq.JArray jsonList1;
            string url = "";
            int t_month = month >= 10 ? 10 : month >= 7 ? 7 : month >= 4 ? 4 : month >= 1 ? 1 : 1;
            int t_year = comboBox3.SelectedIndex == -1 ? year : Int32.Parse(comboBox3.Items[comboBox3.SelectedIndex].ToString());
            foreach (archive a in archiveList)
            {
                if (a.year == t_year + "")
                {
                    foreach (months m in a.months)
                    {
                        if (m.month == t_month + "")
                        {
                            url = bgmlist + m.json;
                        }
                    }
                }
            }
            int adasd = "http://bgmlist.com/json/".Length;
            jsonName = url.Substring(adasd, url.Length - adasd);

            //**   本地json没有创建的话
            if (System.IO.File.Exists(@jsonName))
            {
                readLocalJson(@jsonName);
            }
            else
            {
                WebClient webClient = new WebClient();
                byte[] b = webClient.DownloadData(url);
                string jsonText = Encoding.UTF8.GetString(b, 0, b.Length);
                jsonList1 = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(jsonText);
                foreach (Newtonsoft.Json.Linq.JObject a in jsonList1)
                {
                    a.Add("isOrderRabbit", "0");//是否订阅
                    a.Add("episode", "01");//集数 默认01
                    a.Add("searchKeyword", a["titleCN"].ToString());//搜索用关键字 默认用json提供的
                    a.Add("fansub", " ");//字幕组
                }
                writeLocalJson(jsonList1, jsonName);
                readLocalJson(@jsonName);
                checkBox1.Checked = false;
            }
            dataGridView1.DataSource = (from a in jsonList
                                        where Convert.ToInt32(a.weekDayCN) == day
                                        select a).ToList(); ;
        }
        private void readLocalJson(string jsonName)
        {
            StreamReader sr = new StreamReader(jsonName, Encoding.GetEncoding("UTF-8"));
            String line;
            StringBuilder sb = new StringBuilder();
            string jsonText;
            while ((line = sr.ReadLine()) != null)
            {
                sb.Append(line);
            }
            sr.Close();
            jsonText = sb.ToString();
            jsonList = JsonConvert.DeserializeObject<List<JsonClass>>(jsonText);
        }
        private void writeLocalJson(List<JsonClass> jsl, string jsonName)
        {
            string json = JsonConvert.SerializeObject(jsl);
            System.IO.File.WriteAllText(@jsonName, json);
        }
        private void writeLocalJson(Newtonsoft.Json.Linq.JArray jsl, string jsonName)
        {
            System.IO.File.WriteAllText(@jsonName, jsl.ToString());
        }
        private void threadMethod(object a)
        {
            _syncContext.Post(SetLabelText, a.ToString());//子线程中通过UI线程上下文更新UI  
        }
        private void threadMethod1(object a)
        {
            _syncContext.Post(btnTotorrent, null);//子线程中通过UI线程上下文更新UI  
        }
        private void SetLabelText(object text)
        {
            this.label1.Text = text.ToString();
            this.label1.Refresh();
        }
        private void btnTotorrent(object text)
        {
            string keyword = null;
            string torrentList = null;

            int a = 0;
            int b = 0;
            string faillist = "";
            foreach (JsonClass jc in jsonList)
            {
                if (jc.isOrderRabbit == "1")
                {
                    keyword = jc.searchKeyword + " " + jc.episode + " " + jc.fansub;
                    keyword = getTorrent(keyword);
                    if (keyword != null && keyword != "nothing")
                    {
                        torrentList += keyword + "\n";
                        int iii = Int32.Parse(jc.episode) + 1;
                        jc.episode = iii < 10 ? "0" + iii : iii + "";
                        ++a;
                    }
                    else if (keyword == "nothing")
                    {
                        faillist += jc.searchKeyword + " ";
                        ++b;
                    }
                }
            }
            if (torrentList != null)
            {
                writeLocalJson(jsonList, jsonName);
                Clipboard.SetDataObject(torrentList);
            }
            this.dataGridView1.Refresh();
            this.label1.Text = "成功:" + a + "未处理：" + b + " 详细:" + faillist;
        }
        private void button1_Click(object sender, System.EventArgs e)
        {
            Thread demoThread = new Thread(new ParameterizedThreadStart(threadMethod));
            demoThread.IsBackground = true;
            demoThread.Start("处理中");//启动线程  

            demoThread = new Thread(new ParameterizedThreadStart(threadMethod1));
            demoThread.IsBackground = true;
            demoThread.Start();//启动线程 
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            writeLocalJson(jsonList, jsonName);
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            foreach (months m in (from a in archiveList
                                  where a.year == comboBox3.Items[comboBox3.SelectedIndex].ToString()
                                  select a).ToList()[0].months)
            {
                comboBox2.Items.Add(m.month);
            }
            comboBox2.SelectedIndex = 0;
            comboBox1comboBox2_SelectedIndexChanged();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1comboBox2_SelectedIndexChanged();

        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1comboBox2_SelectedIndexChanged();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1comboBox2_SelectedIndexChanged();
        }
        private void comboBox1comboBox2_SelectedIndexChanged()
        {
            if (initflag)
            {
                readJson(Int32.Parse(comboBox2.SelectedItem.ToString()));
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    dataGridView1.DataSource = (from a in jsonList
                                                where Convert.ToInt32(a.weekDayCN) == comboBox1.SelectedIndex
                                                && a.isOrderRabbit == "1"
                                                select a).ToList();
                }
                else
                {
                    dataGridView1.DataSource = (from a in jsonList
                                                where Convert.ToInt32(a.weekDayCN) == comboBox1.SelectedIndex
                                                select a).ToList();
                }
                var height = 120;
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    height += dr.Height;
                }
                this.Height = height;
                this.Width = 1100;
            }
        }
       
        private static string readdownloadsoftpath()
        {
            if (System.IO.File.Exists(@"downloadsoft.txt"))
            {
                StreamReader sr = new StreamReader(@"downloadsoft.txt", Encoding.GetEncoding("UTF-8"));
                String line;
                StringBuilder sb = new StringBuilder();
                line = sr.ReadLine();
                sr.Close();
                return line;
            }
            else
            {
                System.IO.File.WriteAllText(@"downloadsoft.txt", "");
                return "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            string fileToOpen = "";
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileToOpen = FD.FileName;
                System.IO.File.WriteAllText(@"downloadsoft.txt", fileToOpen);
                downloadsoftpath = readdownloadsoftpath();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string asd = @"1.  点击年份月份星期几
2.  勾选需要订阅的片,根据需要编辑本集集数，字幕组和修正后的关键字
2.5.点击donwoloadsoft设置你的下载软件位置(非必须)
3.  设置好后点击左上角按钮，种子文件连接会添加到剪切板";
            MessageBox.Show(asd);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string name = prompt.ShowDialog( "input your title","我爱冷泉麻子");
            if (name!="")
            {
                JsonClass jc = new JsonClass();
                jc.titleCN = name;
                jc.weekDayCN = comboBox1.SelectedIndex + "";
                jc.isOrderRabbit = "0";
                jc.episode = "01";
                jc.searchKeyword = name;
                jc.fansub = " ";
                jsonList.Add(jc);
                writeLocalJson(jsonList, jsonName);
                comboBox1comboBox2_SelectedIndexChanged();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //del
            if (MessageBox.Show("Delete this user?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //delete
                foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                {
                    jsonList.RemoveAll(a => a.titleCN == dataGridView1.Rows[item.Index].Cells[0].Value.ToString());
                    //dataGridView1.Rows.RemoveAt(item.Index);
                }
                writeLocalJson(jsonList, jsonName);
                comboBox1comboBox2_SelectedIndexChanged();
            }
        }
    }
}
