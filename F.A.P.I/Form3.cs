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
using System.Reflection;

using NSoup.Nodes;
using Newtonsoft.Json;

namespace F.A.P.I
{
    public partial class Form3 : Form
    {
        public int day = (int)DateTime.Now.DayOfWeek;
        public int month = (int)DateTime.Now.Month;
        public int year = (int)DateTime.Now.Year;

        public Form1 mF_Form;
        public Form3(ComboBox a, ComboBox b, ComboBox c, Form1 mF_Form)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.mF_Form = mF_Form;

            //initcomboboxval(ref comboBox1, a);
            //initcomboboxval(ref comboBox4, a);

            initcomboboxval(ref comboBox2, b);
            initcomboboxval(ref comboBox5, b);

            //initcomboboxval(ref comboBox3, c);
            comboBox3.Items.Add(1);
            comboBox3.Items.Add(4);
            comboBox3.Items.Add(7);
            comboBox3.Items.Add(10);



            //initcomboboxval(ref comboBox6, c);
            comboBox6.Items.Add(1);
            comboBox6.Items.Add(4);
            comboBox6.Items.Add(7);
            comboBox6.Items.Add(10);

            initcombobox();
        }

        private void initcomboboxval(ref ComboBox cb1, ComboBox cb2)//初始化列表
        {
            for (var i = 0; i < cb2.Items.Count; ++i)
            {
                cb1.Items.Add(cb2.Items[i]);
            }
        }

        private void initcombobox()//初始化列表
        {
            //comboBox1.SelectedIndex = day;
            for (var i = 0; i < comboBox2.Items.Count; ++i)
            {
                if (comboBox2.Items[i].ToString() == year + "")
                {
                    comboBox2.SelectedIndex = i;
                }
            }
            comboBox3.SelectedIndex = month >= 10 ? 3 : month >= 7 ? 2 : month >= 4 ? 1 : month >= 1 ? 0 : 0;


            //comboBox4.SelectedIndex = day;
            for (var i = 0; i < comboBox5.Items.Count; ++i)
            {
                if (comboBox5.Items[i].ToString() == year + "")
                {
                    comboBox5.SelectedIndex = i;
                }
            }
            comboBox6.SelectedIndex = month >= 10 ? 3 : month >= 7 ? 2 : month >= 4 ? 1 : month >= 1 ? 0 : 0;
        }
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            readArchiveJson();

            List<JsonClass> j1 = readJson(Int32.Parse(comboBox3.SelectedItem.ToString()), comboBox2);
            List<JsonClass> j2 = readJson(Int32.Parse(comboBox6.SelectedItem.ToString()), comboBox5);
            List<JsonClass> j22 = new List<JsonClass>();

            foreach (JsonClass jc1 in j1)
            {
                foreach (JsonClass jc2 in j2)
                {
                    if (jc2.titleCN == jc1.titleCN)//&& jc1.isOrderRabbit == "1"
                    {
                        jc2.isOrderRabbit = jc1.isOrderRabbit;
                        jc2.longepisode = jc1.longepisode;
                        jc2.searchKeyword = jc1.searchKeyword;
                        jc2.fansub = jc1.fansub;
                        jc2.episode = jc1.episode;

                        jc2.lastDate = jc1.lastDate;
                    }
                }
                if ("1" == jc1.isOrderRabbit)//&& jc1.isOrderRabbit == "1"
                {
                    j22.Add(jc1);
                }
            }
            foreach (JsonClass j222 in j22)
            {
                j2.Add(j222);
            }

                String url = getJsonNameUrl(Int32.Parse(comboBox6.SelectedItem.ToString()), comboBox5);
            int adasd = "https://bgmlist.com/api/v1/bangumi/archive/".Length;
            String jsonName = url.Substring(adasd, url.Length - adasd);


            writeLocalJson(j2, jsonName);
            MessageBox.Show("Success!");
            mF_Form.getdataGridView1().Refresh();
            this.Close();
        }



        public string jsonName = "";
        public static string appdataFAPI = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\F.A.P.I.3";
        public static string archive = Path.Combine(appdataFAPI, "archive.json");
        public static string fansubtxt = Path.Combine(appdataFAPI, "fansub.txt");
        public static string downloadsofttxt = Path.Combine(appdataFAPI, "downloadsoft.txt");



        public List<archive> archiveList;
        public string bgmlist = "";//http://bgmlist.com/

        private void writeLocalJson(List<JsonClass> jsl, string jsonName)//更新动画信息json
        {
            string json = JsonConvert.SerializeObject(jsl);
            System.IO.File.WriteAllText(Path.Combine(appdataFAPI, @jsonName), json);
        }
        private void writeLocalJson(Newtonsoft.Json.Linq.JArray jsl, string jsonName)//更新动画信息json 第一次自定义更新结构
        {
            System.IO.File.WriteAllText(Path.Combine(appdataFAPI, @jsonName), jsl.ToString());
        }
        private List<JsonClass> readLocalJson(string jsonName)//读取动画详细信息json 本地
        {
            List<JsonClass> jsonList;
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
            return jsonList = JsonConvert.DeserializeObject<List<JsonClass>>(jsonText);
        }
        private void readArchiveJson() /* 读取json汇总的json */
        {
            MyWebClient webClient = new MyWebClient();
            //string url = "https://bgmlist.com/tempapi/archive.json";
            string url = "https://bgmlist.com/api/v1/bangumi/season";
            bool local = System.IO.File.Exists(archive);
            try
            {
                if (local)
                {
                    readLocalArchiveJson();
                    var maxMonth = Int32.Parse(archiveList.OrderByDescending(item => item.year).First().months.OrderByDescending(item => item.month).First().month);
                    var maxYear = Int32.Parse(archiveList.OrderByDescending(item => item.year).First().year);
                    if (year > maxYear)
                    {
                        local = false;
                    }
                    else
                    {
                        if (maxMonth + 3 <= month)
                        {
                            local = false;
                        }
                    }
                }

                if (local)
                {
                    readLocalArchiveJson();
                }
                else
                {
                    archiveList = new List<I.archive>();
                    byte[] b = webClient.DownloadData(url);
                    string jsonText = Encoding.UTF8.GetString(b, 0, b.Length);
                    Newtonsoft.Json.Linq.JObject aaaa = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(jsonText);
                    Newtonsoft.Json.Linq.JArray bb = (Newtonsoft.Json.Linq.JArray)aaaa.GetValue("items");
                    foreach (var item in bb)
                    {
                        months m = new months();
                        var q = item.ToString().Substring(4, 2);
                        if (q.Equals("q1"))
                        {
                            m.month = "1";
                        }
                        if (q.Equals("q2"))
                        {
                            m.month = "4";
                        }
                        if (q.Equals("q3"))
                        {
                            m.month = "7";
                        }
                        if (q.Equals("q4"))
                        {
                            m.month = "10";
                        }
                        m.json = "https://bgmlist.com/api/v1/bangumi/archive/" + item.ToString();

                        archive asd = null;

                        var qwe = (from a in archiveList
                                   where item.ToString().Substring(0, 4) == a.year
                                   select a);
                        if (qwe.Any())
                        {
                            asd = qwe.Single();
                            asd.months.Add(m);
                        }
                        else
                        {
                            asd = new archive();
                            asd.year = item.ToString().Substring(0, 4);
                            asd.months = new List<months>();
                            asd.months.Add(m);
                            archiveList.Add(asd);
                        }

                    }
                }
            }
            //else
            //{
            //    archiveList = new List<I.archive>();
            //    byte[] b = webClient.DownloadData(url);
            //    string jsonText = Encoding.UTF8.GetString(b, 0, b.Length);
            //    Newtonsoft.Json.Linq.JObject aaaa = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(jsonText);
            //    Newtonsoft.Json.Linq.JObject bb = (Newtonsoft.Json.Linq.JObject)aaaa.GetValue("data");
            //    foreach (var item in bb)
            //    {
            //        archive asd = new archive();
            //        asd.year = item.Key;
            //        asd.months = new List<months>();
            //        Newtonsoft.Json.Linq.JObject cc = (Newtonsoft.Json.Linq.JObject)item.Value;
            //        foreach (var item1 in cc)
            //        {
            //            months m = new months();
            //            m.month = item1.Key;
            //            m.json = ((Newtonsoft.Json.Linq.JObject)item1.Value).GetValue("path").ToString();
            //            asd.months.Add(m);
            //        }
            //        archiveList.Add(asd);
            //    }
            //    jsonText = JsonConvert.SerializeObject(archiveList);
            //    //archiveList = JsonConvert.DeserializeObject<List<archive>>(jsonText);
            //    System.IO.File.WriteAllText(archive, jsonText);
            //}

            catch (Exception ex)
            {
                if (System.IO.File.Exists(archive))
                {
                    readLocalArchiveJson();
                }
                if (archiveList == null || archiveList.Count < 1)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void readLocalArchiveJson() /* 读取json汇总 本地 */
        {
            try
            {
                StreamReader sr = new StreamReader(archive, Encoding.GetEncoding("UTF-8"));
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
            catch (Exception e)
            {
                MessageBox.Show("找不到文件:" + archive + "  ,请至少在联网环境中成功运行第一次哦");
            }
        }

        private string getJsonNameUrl(int month, ComboBox comboBox2)//读取动画详细信息json
        {
            string url = "";
            int t_month = month >= 10 ? 10 : month >= 7 ? 7 : month >= 4 ? 4 : month >= 1 ? 1 : 1;
            int t_year = comboBox2.SelectedIndex == -1 ? year : Int32.Parse(comboBox2.Items[comboBox2.SelectedIndex].ToString());
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
            return url;
        }
        private string getJsonName(String url)//读取动画详细信息json
        {
            int adasd = "https://bgmlist.com/tempapi/bangumi/".Length;
            return url.Substring(adasd, url.Length - adasd).Replace("/", "-"); 
        }


        private List<JsonClass> readJson(int month, ComboBox comboBox2)//读取动画详细信息json
        {
            Newtonsoft.Json.Linq.JArray jsonList1;
            String url = getJsonNameUrl(month, comboBox2);
            int adasd = "https://bgmlist.com/api/v1/bangumi/archive/".Length;
            String jsonName = url.Substring(adasd, url.Length - adasd);

            //**   本地json没有创建的话
            if (System.IO.File.Exists(Path.Combine(appdataFAPI, @jsonName)))
            {
                return readLocalJson(Path.Combine(appdataFAPI, @jsonName));
            }
            else
            {
                MyWebClient webClient = new MyWebClient();
                byte[] b = webClient.DownloadData(url);
                string jsonText = Encoding.UTF8.GetString(b, 0, b.Length);
                Newtonsoft.Json.Linq.JObject aaaa = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(jsonText);

                jsonList1 = new Newtonsoft.Json.Linq.JArray();
                foreach (var item in aaaa)
                {
                    jsonList1.Add(item.Value);
                    //Console.WriteLine(item.Key + item.Value);
                }
                foreach (Newtonsoft.Json.Linq.JObject a in jsonList1)
                {
                    a.Add("isOrderRabbit", "0");                                          /* 是否订阅 */
                    a.Add("episode", "01");                                               /* 集数 默认01 */


                    String sad = a["titleCN"].ToString().Replace("-", " ");
                    if (sad.Trim() == "")
                    {
                        sad = a["titleJP"].ToString().Replace("-", " ");
                        a["titleCN"] = sad;

                    }

                    a.Add("searchKeyword", sad);  /* 搜索用关键字 默认用json提供的 */
                    a.Add("fansub", " ");                                                 /* 字幕组 */
                    a.Add("longepisode", "0");                                            /* 长期连载 */
                    a.Add("lastDate", "");                                                /* 上一次完成时间 */
                }
                writeLocalJson(jsonList1, jsonName);
                return readLocalJson(Path.Combine(appdataFAPI, @jsonName));
            }
        }
    }
}
