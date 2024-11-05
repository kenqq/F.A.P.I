using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;
using System.Globalization;
using System.Diagnostics;
using System.Reflection;

using NSoup.Nodes;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using MyProg;
using Seringa.Engine.Implementations.Proxy;
using Seringa.Engine.Enums;
using OpenQA.Selenium;
using RestSharp;
using OpenQA.Selenium.Edge;

namespace F.A.P.I
{
    public partial class Form1 : Form
    {
        public List<JsonClass> jsonList;
        public List<archive> archiveList;
        public string bgmlist = "";//http://bgmlist.com/

        //public static string dmhyUrl = "https://share.dmhy.org/";
        public static string dmhyUrl = "https://share.dmhy.org/";//https://dmhy.anoneko.com/




        public static string dmhyBgmListUrl = dmhyUrl + "cms/page/name/programme.html";





        public static string fapiServerUrl = "http://127.0.0.1:8080";

        public int day = (int)DateTime.Now.DayOfWeek;
        public int month = (int)DateTime.Now.Month;
        public int year = (int)DateTime.Now.Year;
        public bool is_grid_load = false;
        public bool initflag = false;
        public string jsonName = "";
        public SynchronizationContext _syncContext = null;
        public string downloadsoftpath = "";
        public bool handling = false;
        public List<string> kwList = new List<string>();

        public List<string> filterList = new List<string>();//运行时去除结果    (单纯程序去除结果中的多余词
        public List<string> filterList2 = new List<string>();//运行时"排除"结果   (sql not 运算

        public static string appdataFAPI = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\F.A.P.I.3";
        public static string archive = Path.Combine(appdataFAPI, "archive.json");
        public static string fansubtxt = Path.Combine(appdataFAPI, "fansub.txt");
        public static string filterListtxt = Path.Combine(appdataFAPI, "filterList.txt");
        public static string downloadsofttxt = Path.Combine(appdataFAPI, "downloadsoft.txt");
        public static string configInI = Path.Combine(appdataFAPI, "config.ini");
        public static IniFile MyIni = new IniFile(configInI);
        public static ArrayList lad; /* 临时dmhy 预先加载10页 */
        public static ProxyDetails ProxyDetails = new ProxyDetails();

        public static string dmhycookies = "";

        public ContextMenu contextMenu1 = new ContextMenu();

        public Form1()
        {









            System.Net.ServicePointManager.DefaultConnectionLimit = 100;
            try
            {
                /*
                 * string sosososo = "bt.ktxp.com";
                 * bool qsdfg = Ping(sosososo); / * true; * /
                 * if (!qsdfg)
                 * {
                 *    MessageBox.Show(@"极影又香了");
                 * }
                 * sosososo = "share.dmhy.org";
                 * qsdfg = Ping(sosososo);
                 * if (!qsdfg)
                 * {
                 *    MessageBox.Show(@"花园又香了");
                 * }
                 * sosososo = "www.nyaa.se";
                 * qsdfg = Ping(sosososo);
                 * if (!qsdfg)
                 * {
                 *    MessageBox.Show(@"喵又香了");
                 * }
                 */
                //string[] bababab = "预告 預告 pv GB MP4 720p 1080p 576p 720P 1080P 576P 新番 BIG5 1月 2月 3月 4月 5月 6月 7月 8月 9月 10月 11月 12月 第 话 讨论 一月 二月 三月 四月 五月 六月 七月 八月 九月 十月 十一月 十二月".Split(' ');
                //filterList = bababab.ToList();

                AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
                {
                    string resourceName = new AssemblyName(args.Name).Name + ".dll";
                    string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));
                    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                    {
                        Byte[] assemblyData = new Byte[stream.Length];
                        stream.Read(assemblyData, 0, assemblyData.Length);
                        return (Assembly.Load(assemblyData));
                    }
                };
                InitializeComponent();

                /* 获取UI线程同步上下文 */
                _syncContext = SynchronizationContext.Current;
                if (!Directory.Exists(@appdataFAPI))                /* 判断文件夹是否已经存在 */
                {
                    Directory.CreateDirectory(@appdataFAPI);      /* 创建文件夹 */
                }
                //comboBox4.SelectedIndex = 0;
                label1.Text = @"
＿人人人人人人人人人人人人人人人人人人人人＿
＞　　すっごーい！君はＤＤoＳのフレンズなんだね！！　＜
￣^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^￣
";


                loadconfig();
                loadfansub();
                loadfilterList();
                loadfilterList2();
                readArchiveJson();
                readJson(month,false);
                initcombobox();
                //readVote();


                if (String.IsNullOrEmpty(MyIni.Read("new_version_flag_0_0_4")))
                {
                    MessageBox.Show(@"-------------------------------------------------------------------
○F.A.P.I 
　更新公告
　　　　　　　　　　　　　　　　　                       kenqq
　　　　　　　　　　　　　　　　　　　　　　　        2021/12/26
-------------------------------------------------------------------

■1．更新履歴
2024/11/14 ver 0.0.5 强制TLS
2024/04/14 ver 0.0.4 磁链长度超过255截取不要
2022/01/02 ver 0.0.2 新增按钮 更新bgmlist 当季最新清单,更新字符画
2021/12/26 ver 0.0.1 由于数据源格式发生变化，重新适配;dmhy镜像站移除。
2021/12/21 ver 1.2.3 把dmhy的主站与镜像站分开，主站加入获取cookies机制优化。
2021/04/03 ver 1.2.0 提供对https://nyaa.si 站的支持");
                    MyIni.Write("new_version_flag_0_0_4", "1");
                }




                /* Add menu items to context menu. */
                MenuItem MenuItem_1 = contextMenu1.MenuItems.Add("F.A.P.I.!");
                MenuItem MenuItem_2 = contextMenu1.MenuItems.Add("E&xit");

                MenuItem_1.Click += new EventHandler(button1_Click);
                MenuItem_2.Click += new EventHandler(item_Click2);

                this.notifyIcon1.Text = "F.A.P.I.";
                this.notifyIcon1.ContextMenu = contextMenu1;

                downloadsoftpath = MyIni.Read("downloadSoftPath");
                if (downloadsoftpath != null && downloadsoftpath != "")
                    Process.Start(@downloadsoftpath);

                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { };
                timer.Tick += new EventHandler(OnTimedEvent);
                int autoFapiInternal = 5;
                try
                {
                    autoFapiInternal = Int32.Parse(MyIni.Read("autoFapiInternal"));
                    if (autoFapiInternal < 1)
                    {
                        autoFapiInternal = 5;
                    }
                }
                catch (Exception ex)
                {
                    autoFapiInternal = 5;
                }

                timer.Interval = 1000 * 60 * autoFapiInternal;
                timer.Enabled = true;

                initflag = true;

                if (MyIni.Read("Order") == "1")
                {
                    checkBox1.Checked = !checkBox1.Checked;
                    checkBox1.Checked = !checkBox1.Checked;
                }
                if (MyIni.Read("StartFAPI") == "1")
                {
                    FAPI();
                }
                else
                {
                    MyIni.Write("StartFAPI", "0");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //private void readVote()
        //{
        //    fapiServerUrl = MyIni.Read("fapiServerUrl");
        //    if (fapiServerUrl != null && fapiServerUrl != "")
        //    {
        //        var client = new RestClient(fapiServerUrl);
        //        var request = new RestRequest("FAPI/rs/fapiService/list", Method.POST);
        //        request.RequestFormat = DataFormat.Json;
        //        IRestResponse response = client.Execute(request);
        //        var content = response.Content;
        //        Newtonsoft.Json.Linq.JObject aaaa = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(content);
        //        Newtonsoft.Json.Linq.JArray bbbb = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(aaaa.GetValue("message").ToString());



        //        foreach (JsonClass json in jsonList)
        //        {
        //            json.downvote = "";
        //            json.upvote = "";
        //        }



        //        foreach (Newtonsoft.Json.Linq.JObject aa in bbbb)
        //        {
        //            try
        //            {
        //                JsonClass aaa = (from a in jsonList
        //                                 where a.titleCN == aa.GetValue("t").ToString()
        //                                 select a).ToList()[0];
        //                aaa.downvote = aa.GetValue("d").ToString();
        //                aaa.upvote = aa.GetValue("u").ToString();
        //            }
        //            catch (Exception e)
        //            {

        //            }


        //        }


        //        writeLocalJson(jsonList, jsonName);


        //    }
        //}

        private string getDmhyCookies()
        {
            int i = 0;
            var s = "";
            Proxy proxy = new Proxy();
            var ProxyAddress = MyIni.Read("ProxyAddress");
            var ProxyPort = MyIni.Read("ProxyPort");
            if (ProxyAddress == null || ProxyAddress.Length == 0)
            {
                MessageBox.Show("请输入代理ip地址!");
                throw new Exception("请输入代理ip地址!");
            }
            if (ProxyPort == null || ProxyPort.Length == 0)
            {
                MessageBox.Show("请输入代理端口!");
                throw new Exception("请输入代理端口!");
            }
            var proxyUrl = "http://" + ProxyAddress + ":" + ProxyPort;
            proxy.HttpProxy = proxyUrl;
            proxy.SslProxy = proxyUrl;

            //var options = new ChromeOptions();
            //options.Proxy = proxy;
            ////options.AddArgument("ignore-certificate-errors");
            //options.AddArguments("start-maximized");
            //options.AddArguments("--disable-blink-features=AutomationControlled");
            //options.AddArguments("--ignore-certificate-errors");
            //options.AddArguments("--ignore-ssl-errors");


            /* 设置注册表 修改全局代理
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            if (myKey != null)
            {
                var aaaaa = myKey.GetValue("ProxyEnable");
                myKey.SetValue("ProxyEnable", "1", RegistryValueKind.DWord);
                myKey.Close();
            }
            */

            /*
            var options = new InternetExplorerOptions()
            {
                InitialBrowserUrl = dmhyUrl,
                Proxy = proxy
            };*/
            var options = new EdgeOptions();
            options.AddArgument("headless");
            options.Proxy = proxy;
            IWebDriver driver = null;
            while (true)
            {
                try
                {
                    //driver = new InternetExplorerDriver(options);
                    driver = new EdgeDriver(options);
                    //new ChromeDriver(options);
                    driver.Navigate().GoToUrl(dmhyUrl);
                    IWebElement text = null;
                    OpenQA.Selenium.Support.UI.WebDriverWait wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, new TimeSpan(0, 5, 0));
                    text = wait.Until<IWebElement>(drv =>
                    {
                        try
                        {
                            return drv.FindElement(By.Id("keyword"));
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                            Thread.Sleep(5000);
                            return null;
                        }
                    });
                    text.SendKeys("魔法少女小圆 01");
                    driver.FindElement(By.ClassName("formButton")).SendKeys(OpenQA.Selenium.Keys.Space);
                    Thread.Sleep(5000);
                    text = wait.Until<IWebElement>(drv =>
                    {
                        try
                        {
                            return drv.FindElement(By.Id("keyword"));
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                            Thread.Sleep(5000);
                            return null;
                        }
                    });
                    var _AllCookies = driver.Manage().Cookies.AllCookies;
                    if (_AllCookies.Count >= 2)
                    {
                        
                        foreach (var a in _AllCookies)
                        {
                            s += a + ";";
                        }
                        //MessageBox.Show(s);
                        break;
                    }
                    else
                    {
                        foreach (var a in _AllCookies)
                        {
                            s += a + ";";
                        }
                        //MessageBox.Show(s);
                        ++i;
                        if (i == 3)
                        {
                            MessageBox.Show("清理一下ie cookies!");
                            Application.Exit();
                        }
                    }
                }
                catch (Exception eeeee)
                {
                    MessageBox.Show(eeeee.ToString());
                    Application.Exit();
                }
                finally
                {
                    driver.Quit();
                }
            }

            //proxyUrl = "http://:";
            //var proxy2 = new Proxy();
            //proxy2.Kind = ProxyKind.Direct;

            //var options2 = new InternetExplorerOptions()
            //{
            //    //InitialBrowserUrl = "http://www.baidu.com",
            //    Proxy = proxy2
            //};
            //var driver2 = new InternetExplorerDriver(options2);
            //Thread.Sleep(5000);
            //driver2.Quit();

            /* 设置注册表 修改全局代理
myKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
if (myKey != null)
{
    var aaaaa = myKey.GetValue("ProxyEnable");
    myKey.SetValue("ProxyEnable", "0", RegistryValueKind.DWord);
    myKey.Close();
}
            */


return s;
}

private void OnTimedEvent(object sender, EventArgs e)
{
if (checkBox5.CheckState == CheckState.Checked)
    FAPI();
}


private void setconfig()
{
if (initflag)
{
    if (checkBox1.CheckState == CheckState.Checked)
    {
        MyIni.Write("Order", "1");
    }
    else
    {
        MyIni.Write("Order", "0");
    }
    if (checkBox3.CheckState == CheckState.Checked)
    {
        MyIni.Write("Nolimit", "1");
    }
    else
    {
        MyIni.Write("Nolimit", "0");
    }
    if (checkBox5.CheckState == CheckState.Checked)
    {
        MyIni.Write("autoF", "1");
    }
    else
    {
        MyIni.Write("autoF", "0");
    }
    if (checkBox4.CheckState == CheckState.Checked)
    {
        MyIni.Write("linkType", "1");
    }
    else
    {
        MyIni.Write("linkType", "0");
    }
    MyIni.Write("Ffrom", comboBox4.SelectedIndex + "");
    //MyIni.Write("autoTime", "300");
    //MyIni.Write("downloadSoftPath", "");
    //MyIni.Write("ProxyType", "ProxyType.Socks");
    //MyIni.Write("ProxyAddress", "127.0.0.1");
    //MyIni.Write("ProxyPort", "1081");
    //MyIni.Write("FullProxyAddress", "127.0.0.1:1081");
}
}

private void loadconfig()
{
if (System.IO.File.Exists(configInI))
{
    loadconfig_1();
}
else
{
    /* 第一行空格 */
            string fansubtmp = "";
                System.IO.File.WriteAllText(configInI, fansubtmp);
                MyIni.Write("Order", "0");
                MyIni.Write("Nolimit", "0");
                MyIni.Write("autoF", "0");
                MyIni.Write("autoTime", "300");
                MyIni.Write("linkType", "0");
                MyIni.Write("downloadSoftPath", "");
                MyIni.Write("Ffrom", "0");
                //Proxy http  Socks socks4,5 None 没代理
                MyIni.Write("ProxyType", "None");
                MyIni.Write("ProxyAddress", "127.0.0.1");
                MyIni.Write("ProxyPort", "1081");
                MyIni.Write("FullProxyAddress", "127.0.0.1:1081");
                MyIni.Write("ProxyUserName", "ProxyUserName");
                MyIni.Write("ProxyPassword", "ProxyPassword");

                MyIni.Write("StartFAPI", "0");
                MyIni.Write("StartMinimized", "0");
                MyIni.Write("torrentPath", "0");
                MyIni.Write("autoFapiInternal", "5");
                MyIni.Write("nyaQueryString", "https://nyaa.si/?f=0&c=1_3&q=");

                loadconfig_1();
            }
        }

        private void loadconfig_1()
        {
            if (MyIni.Read("Order") == "1")
            {
                checkBox1.Checked = true;
            }
            else if (MyIni.Read("Order") == "0")
            {
                checkBox1.Checked = false;
            }
            if (MyIni.Read("Nolimit") == "1")
            {
                checkBox3.Checked = true;
            }
            else if (MyIni.Read("Nolimit") == "0")
            {
                checkBox3.Checked = false;
            }
            if (MyIni.Read("autoF") == "1")
            {
                checkBox5.Checked = true;
            }
            else if (MyIni.Read("autoF") == "0")
            {
                checkBox5.Checked = false;
            }
            if (MyIni.Read("linkType") == "1")
            {
                checkBox4.Checked = true;
            }
            else if (MyIni.Read("linkType") == "0")
            {
                checkBox4.Checked = false;
            }
            if (MyIni.Read("StartMinimized") == "1")
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
            else
            {
                MyIni.Write("StartMinimized", "0");
            }
            comboBox4.SelectedIndex = Int32.Parse(MyIni.Read("Ffrom"));

            ProxyDetails.ProxyType = (ProxyType)System.Enum.Parse(typeof(ProxyType), MyIni.Read("ProxyType"));
            ProxyDetails.ProxyAddress = MyIni.Read("ProxyAddress");
            ProxyDetails.ProxyPort = Int32.Parse(MyIni.Read("ProxyPort"));
            ProxyDetails.FullProxyAddress = MyIni.Read("FullProxyAddress");

            ProxyDetails.ProxyUserName = MyIni.Read("ProxyUserName");
            ProxyDetails.ProxyPassword = MyIni.Read("ProxyPassword");
        }

        private void loadfilterList()
        {

            if (System.IO.File.Exists(@filterListtxt))
            {
                string[] bababab = readfilterList().Split(' '); ;
                filterList = bababab.ToList();
            }
            else
            {
                /* 第一行空格 */
                string fansubtmp = @"GB MP4 720p 1080p 576p 720P 1080P 576P 1280 720 新番 BIG5 1月 2月 3月 4月 5月 6月 7月 8月 9月 10月 11月 12月 第 话 讨论 一月 二月 三月 四月 五月 六月 七月 八月 九月 十月 十一月 十二月 01月 04月 07月 11月 10Bit BDRIP
预告 預告 pv 英语 延後 HEVC_P10 1080 Hi10P HEVC_Main10";
                System.IO.File.WriteAllText(@filterListtxt, fansubtmp);
                string[] bababab = readfilterList().Split(' '); ;
                filterList = bababab.ToList();
            }
            for (int i = filterList.Count - 1; i > -1; --i)
            {
                if (filterList[i].Length < 1)
                    filterList.Remove(filterList[i]);
            }
        }
        private void loadfilterList2()
        {

            if (System.IO.File.Exists(@filterListtxt))
            {
                string[] bababab = readfilterList2().Split(' '); ;
                filterList2 = bababab.ToList();
            }
            else
            {
                /* 第一行空格 */
                string fansubtmp = @"GB MP4 720p 1080p 576p 720P 1080P 576P 新番 BIG5 1月 2月 3月 4月 5月 6月 7月 8月 9月 10月 11月 12月 第 话 讨论 一月 二月 三月 四月 五月 六月 七月 八月 九月 十月 十一月 十二月
预告 預告 pv ";
                System.IO.File.WriteAllText(@filterListtxt, fansubtmp);
                string[] bababab = readfilterList2().Split(' '); ;
                filterList2 = bababab.ToList();
            }
            for (int i = filterList2.Count - 1; i > -1; --i)
            {
                if (filterList2[i].Length < 1)
                    filterList2.Remove(filterList2[i]);
            }
        }
        private String readfilterList()
        {
            StreamReader sr = new StreamReader(@filterListtxt, Encoding.GetEncoding("UTF-8"));
            StringBuilder sb = new StringBuilder();
            return (sr.ReadLine());
        }
        private String readfilterList2()
        {
            StreamReader sr = new StreamReader(@filterListtxt, Encoding.GetEncoding("UTF-8"));
            StringBuilder sb = new StringBuilder();
            sr.ReadLine();
            return (sr.ReadLine());
        }

        void item_Click2(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            Application.Exit();
            /* your code here */
        }





        private void initcombobox() /* 初始化列表 */
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


        private void loadfansub() /* 读取& 创建字幕组 并且加载 */
        {
            if (System.IO.File.Exists(@fansubtxt))
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
                /* 第一行空格 */
                string fansubtmp = @" 
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
                System.IO.File.WriteAllText(@fansubtxt, fansubtmp);
                ArrayList al = readFansub();
                ComboBox CB = new ComboBox();
                foreach (string a in al)
                {
                    CB.Items.Add(a);
                }
                ((DataGridViewComboBoxColumn)dataGridView1.Columns["fansub"]).DataSource = CB.Items;
            }
        }


        string getTorrentFromKtxp(string keywordURL, bool longepisode, JsonClass jc) /* 获取种子 */
        {
            string str_url = "nothing";
            keywordURL = keywordURL.Replace("\"", "");
            if ((from a in kwList
                 where a == keywordURL
                 select a).ToList().Count == 0)
            {
                /* kwList.Add(keywordURL); */
            }
            else
            {
                kwList.Add(keywordURL);
                return ("fail");
            }
            try
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                /*下载网页源代码 */
                MyWebClient webClient = new MyWebClient(dmhycookies);
                string ktxp = "http://bt.ktxp.com";
                string sort_addate = "&order=addate";
                string sort_seeders = "&order=seeders";
                string url = "http://bt.ktxp.com/search.php?keyword=" + keywordURL
                                  + "&sort_id=12";
                int param = 0;
                if (param == 0)
                {
                    url += sort_addate;
                }
                else if (param == 1)
                {
                    url += sort_addate;
                }
                string htmlString = Encoding.GetEncoding("utf-8").GetString(webClient.DownloadData(url));
                Document doc = NSoup.NSoupClient.Parse(htmlString);
                int countmax = 0;
                int trCount = doc.Select("tbody").First().Select("tr").Count; /* 找不到结果也会是1 */
                for (var i = 0; i < trCount; ++i)
                {
                    if (trCount == 1)
                    {
                        Element isnulldoc = doc.Select("tbody").First().Select("tr:eq(" + i + ")").First().Select("td").First();
                        if (isnulldoc.Text() == "没有可显示资源")
                            return ("nothing");
                    }

                    if (!longepisode)                                                                                                     /* 如果是长期连载的 无视开播时间 */
                    {
                        Element time_doc = doc.Select("tbody").First().Select("tr:eq(" + i + ")").First().Select("td:eq(0)").First();
                        DateTime dateVal = DateTime.ParseExact(time_doc.Attr("title"), "yyyy/MM/dd HH:mm", culture); /* 片时间 */
                        string monthtmp = comboBox2.Items[comboBox2.SelectedIndex].ToString().Length < 2 ? "0" + comboBox2.Items[comboBox2.SelectedIndex].ToString() : comboBox2.Items[comboBox2.SelectedIndex].ToString();
                        string asdasdsad = comboBox3.Items[comboBox3.SelectedIndex].ToString() + "/"
                                          + monthtmp + "/01 00:00";
                        DateTime currentSeason = DateTime.ParseExact(asdasdsad, "yyyy/MM/dd HH:mm", culture);                         /* 本季时间 */
                        if (DateTime.Compare(dateVal, currentSeason) < 0)                                                           /* 片要比本季时间大 */
                            return ("time");
                    }
                    bool filterflag = false;
                    Element td = doc.Select("tbody").First().Select("tr:eq(" + i + ")").First().Select("td:eq(2)").First();
                    filterflag = filter(jc, td);
                    if (filterflag)
                    {
                        continue;
                    }
                    Element countt = doc.Select("tbody").First().Select("tr:eq(" + i + ")").First().Select("td:eq(6)").First();
                    int count = 0;
                    try
                    {
                        count = Int32.Parse(countt.Text());
                    }
                    catch (Exception e)
                    {
                        count = 0;
                    }
                    if (count >= countmax)
                    {
                        Element torrent = doc.Select("tbody").First().Select("tr:eq(" + i + ")").First()
                                  .Select("td[class=ltext ttitle]").First().Select("a[class=quick-down cmbg]").First(); /* 种子地址 */
                        countmax = count;
                        str_url = ktxp + torrent.Attr("href");
                    }
                }
            }
            catch (System.Net.WebException e)
            {
                kwList.Add(keywordURL);
                return ("fail");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                kwList.Add(keywordURL);
                return ("fail");
            }
            return (str_url);
        }


        private bool filter(JsonClass jc, Element td)
        {
            bool filterflag = false;
            List<string> filterDate = new List<string>();
            string year = comboBox3.SelectedItem.ToString();
            string year2 = comboBox2.SelectedItem.ToString();
            string[] months = "1".Equals(comboBox2.SelectedItem.ToString()) == true ? "01,02,03".Split(',') :
                              "4".Equals(comboBox2.SelectedItem.ToString()) == true ? "04,05,06".Split(',') :
                              "7".Equals(comboBox2.SelectedItem.ToString()) == true ? "07,08,09".Split(',') :
                              "10".Equals(comboBox2.SelectedItem.ToString()) == true ? "10,11,12".Split(',') : null;

            for (int i = 0; i < 3; ++i)
            {
                for (int j = 1; j < 32; ++j)
                {
                    filterDate.Add(year + "-" + months[i] + "-" + (j < 10 ? "0" + j : j + ""));
                    filterDate.Add(year + "." + months[i] + "." + (j < 10 ? "0" + j : j + ""));
                }
            }
            string asd = "";


            /*
             * if (jc.episode.Equals(comboBox2.SelectedItem.ToString()))
             * {
             */
            NSoup.Select.Elements td_a = td.Select("a");
            /* NSoup.Select.Elements td_span = td.Select("span"); */
            foreach (Element a in td_a)
            {
                asd += a.Text();
            }


            /*
             * foreach (Element a in td_span)
             * {
             *    asd += a.Text();
             * }
             */
            foreach (string s in filterList)
            {
                asd = asd.Replace(s, " ");
            }
            foreach (string s in filterDate)
            {
                asd = asd.Replace(s, " ");
            }
            if (asd.IndexOf(jc.episode) == -1)
            {
                filterflag = true;
            }
            foreach (string s in filterList2)
            {
                if (asd.IndexOf(s) > -1)
                {
                    filterflag = true;
                }
            }
            /* } */
            return (filterflag);
        }


        string getTorrentFromNyaa(string keywordURL, bool longepisode) /* 获取种子 */
        {
            string str_url = null;
            keywordURL = keywordURL.Replace("\"", "");
            if ((from a in kwList
                 where a == keywordURL
                 select a).ToList().Count == 0)
            {
                /* kwList.Add(keywordURL); */
            }
            else
            {
                kwList.Add(keywordURL);
                return ("fail");
            }
            try
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                /*下载网页源代码 */
                MyWebClient webClient = new MyWebClient(dmhycookies);
                string url = "http://www.nyaa.se/?page=search&term=" + keywordURL
                                  + "&cats=1_0&minage=0&maxage=14";
                string htmlString = Encoding.GetEncoding("utf-8").GetString(webClient.DownloadData(url));
                Document doc = NSoup.NSoupClient.Parse(htmlString);
                if (htmlString.IndexOf("tlist") > -1)
                {
                    /* --------------------- */
                    int countmax = 0;
                    int trCount = doc.Select("table.tlist").First().Select("tr").Count; /* 找不到结果会是2 */
                    for (var i = 1; i < trCount; ++i)
                    {
                        if (trCount == 2)
                        {
                            Element isnulldoc = doc.Select("table.tlist").First().Select("tr:eq(" + i + ")").First().Select("td").First().Select("b").First();
                            if (isnulldoc.Text() == "No torrents found.")
                                return ("nothing");
                        }
                        if (!longepisode) /* 如果是长期连载的 无视开播时间 */
                        {
                        }
                        Element countt = doc.Select("table.tlist").First().Select("tr:eq(" + i + ")").First().Select("td:eq(6)").First();
                        int count = 0;
                        try
                        {
                            count = Int32.Parse(countt.Text());
                        }
                        catch (Exception e)
                        {
                            count = 0;
                        }
                        if (count >= countmax)
                        {
                            Element torrent = doc.Select("table.tlist").First().Select("tr:eq(" + i + ")").First()
                                      .Select("td[class=tlistdownload]").First().Select("a").First(); /* 种子地址 */
                            countmax = count;
                            str_url = torrent.Attr("href");
                        }
                    }
                    /* /---------- */
                }
                else
                {
                    Element asd = doc.Select("div[class=viewdownloadbutton]").First();
                    str_url = doc.Select("div[class=viewdownloadbutton] a[rel=nofollow]").First().Attr("href"); /* 找不到结果会是2 */
                }
            }
            catch (System.Net.WebException e)
            {
                kwList.Add(keywordURL);
                return ("fail");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                kwList.Add(keywordURL);
                return ("fail");
            }
            return (str_url);
        }


        string getTorrentFromDmhy(string keywordURL, bool longepisode, JsonClass jc) /* 获取种子 */
        {
            bool bb = jc.longepisode == "1" ? true : false;
            List<string> filterEpisode = new List<string>();
            if (!bb)
            {
                /*
                 * if (jc.episode.Equals(comboBox2.SelectedItem.ToString()))
                 * {
                 */

                int countttttt = 0;
                try
                {
                    countttttt = Int32.Parse(jc.episode);
                }
                catch (Exception e)
                {
                    countttttt = 0;
                }
                int episode_before = countttttt - 1;

                for (int q = episode_before; q > -1; --q)
                {
                    filterEpisode.Add((q < 10 ? "0" + q : q + ""));
                }
                /* } */
            }
            string str_url = "nothing";
            keywordURL = keywordURL.Replace("\"", "");

            if ((from a in kwList
                 where a == keywordURL
                 select a).ToList().Count == 0)
            {
                /* kwList.Add(keywordURL); */
            }
            else
            {
                kwList.Add(keywordURL);
                return ("fail");
            }
            try
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                /*下载网页源代码 */
                MyWebClient webClient = new MyWebClient(dmhycookies);

                string dmhy = dmhyUrl;

                string url = dmhyUrl + "topics/list?keyword=" + keywordURL + "&team_id=0&order=date-desc";

                /* string url = "dmhyUrl/"; */

                string htmlString = Encoding.GetEncoding("utf-8").GetString(webClient.DownloadData(url));

                Document doc = NSoup.NSoupClient.Parse(htmlString);

                int countmax = 0;
                NSoup.Select.Elements tables = doc.Select("table#topic_list");
                if (tables.Count == 0)
                {
                    return ("nothing");
                }
                Element table = tables.First();
                int trCount = table.Select("tr").Count - 1;       /* 找不到结果也会是1 */
                Element tr = null;
                for (var i = 0; i < trCount; ++i)                     /* 第一行是标题 */
                {
                    try
                    {
                        if (i == 0)
                        {
                            NSoup.Select.Elements trs = doc.Select("table#topic_list").First().Select("tr:eq(" + i + ")");
                            tr = doc.Select("table#topic_list").First().Select("tr:eq(" + i + ")").Eq(1).First();
                        }
                        else
                        {
                            tr = doc.Select("table#topic_list").First().Select("tr:eq(" + i + ")").First();
                        }
                        string type = tr.Select("td:eq(1)").First().Text();
                        if (!(type == "特攝" || type == "動畫"))
                        {
                            continue;
                        }
                        bool filterflag = false;
                        Element td = doc.Select("tbody").First().Select("tr:eq(" + i + ")").First().Select("td:eq(2)").First();
                        filterflag = filter(jc, td);
                        if (filterflag)
                        {
                            continue;
                        }
                        if (!longepisode)                                                                                             /* 如果是长期连载的 无视开播时间 */
                        {
                            Element time_doc = tr.Select("td:eq(0)").First().Select("span").First();
                            DateTime dateVal = DateTime.ParseExact(time_doc.Text(), "yyyy/MM/dd HH:mm", culture);  /* 片时间 */
                            string monthtmp = comboBox2.Items[comboBox2.SelectedIndex].ToString().Length < 2 ? "0" + comboBox2.Items[comboBox2.SelectedIndex].ToString() : comboBox2.Items[comboBox2.SelectedIndex].ToString();
                            string asdasdsad = comboBox3.Items[comboBox3.SelectedIndex].ToString() + "/"
                                              + monthtmp + "/01 00:00";
                            DateTime currentSeason = DateTime.ParseExact(asdasdsad, "yyyy/MM/dd HH:mm", culture);                 /* 本季时间 */
                            if (DateTime.Compare(dateVal, currentSeason) < 0)                                                   /* 片要比本季时间大 */
                                continue;
                            /* return ("time"); */
                        }
                        Element countt = tr.Select("td:eq(7)").First();


                        int count = 0;
                        try
                        {
                            count = Int32.Parse(countt.Text());
                        }
                        catch (Exception e)
                        {
                            count = 0;
                        }


                        if (count >= countmax)
                        {
                            /* Element torrent = tr.Select("td:eq(3)").First().Select("a[class=download-arrow arrow-torrent]").First(); / * 种子地址 * / */
                            Element magnet = tr.Select("td:eq(3)").First().Select("a[class=download-arrow arrow-magnet]").First();     /* 磁链地址 */
                            Element innerlink = tr.Select("td:eq(2)").First().Select("a").Last();                                         /* 内部链接地址 用于获取地址 */

                            countmax = count;
                            if (checkBox4.CheckState == CheckState.Checked)
                            {
                                /* str_url = dmhy + torrent.Attr("href"); */
                                string inner_url = dmhy + innerlink.Attr("href");
                                string innerhtmlString = Encoding.GetEncoding("utf-8").GetString(webClient.DownloadData(inner_url));
                                Document innerdoc = NSoup.NSoupClient.Parse(innerhtmlString);
                                Element torrent = innerdoc.GetElementById("tabs-1").Select("a").First();
                                str_url = "http:" + torrent.Attr("href");
                                /* str_url = magnet.Attr("href"); */
                            }
                            else
                            {
                                str_url = magnet.Attr("href");
                            }
                        }
                    }
                    catch (Exception eeeee)
                    {
                        MessageBox.Show(jc.searchKeyword + " " + jc.episode + " tmd出错了,请联系tmd kenqq");
                        MessageBox.Show(eeeee.ToString());
                        /* str_url = "asd"; */
                    }
                }
            }
            catch (System.Net.WebException e)
            {
                kwList.Add(keywordURL);
                return ("fail");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                kwList.Add(keywordURL);
                return ("fail");
            }
            return (str_url);
        }

        string getTorrentFromAcgRip(string keywordURL, bool longepisode, JsonClass jc) /* 获取种子 */
        {
            bool bb = jc.longepisode == "1" ? true : false;
            List<string> filterEpisode = new List<string>();
            if (!bb)
            {
                /*
                 * if (jc.episode.Equals(comboBox2.SelectedItem.ToString()))
                 * {
                 */

                int countttttt = 0;
                try
                {
                    countttttt = Int32.Parse(jc.episode);
                }
                catch (Exception e)
                {
                    countttttt = 0;
                }
                int episode_before = countttttt - 1;

                for (int q = episode_before; q > -1; --q)
                {
                    filterEpisode.Add((q < 10 ? "0" + q : q + ""));
                }
                /* } */
            }
            string str_url = "nothing";
            keywordURL = keywordURL.Replace("\"", "");

            if ((from a in kwList
                 where a == keywordURL
                 select a).ToList().Count == 0)
            {
                /* kwList.Add(keywordURL); */
            }
            else
            {
                kwList.Add(keywordURL);
                return ("fail");
            }
            try
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                /*下载网页源代码 */
                MyWebClient webClient = new MyWebClient(dmhycookies);
                string acg_rip = "https://acg.rip/";

                string url = "https://acg.rip/?term=" + keywordURL;

                string htmlString = Encoding.GetEncoding("utf-8").GetString(webClient.DownloadData(url));

                Document doc = NSoup.NSoupClient.Parse(htmlString);

                int countmax = 0;
                NSoup.Select.Elements tables = doc.Select("table[class=table table-hover table-condensed post-index]");

                if (tables.Count == 0)
                {
                    return ("nothing");
                }
                Element table = tables.First();
                int trCount = table.Select("tr").Count - 1;       /* 找不到结果也会是1 */
                Element tr = null;
                for (var i = 0; i < trCount; ++i)                     /* 第一行是标题 */
                {
                    try
                    {
                        if (i == 0)
                        {
                            NSoup.Select.Elements trs = doc.Select("table[class=table table-hover table-condensed post-index]").First().Select("tr:eq(" + i + ")");
                            tr = doc.Select("table[class=table table-hover table-condensed post-index]").First().Select("tr:eq(" + i + ")").Eq(1).First();
                        }
                        else
                        {
                            tr = doc.Select("table[class=table table-hover table-condensed post-index]").First().Select("tr:eq(" + i + ")").First();
                        }

                        bool filterflag = false;
                        Element td = tr.Select("td:eq(1)").First();
                        filterflag = filter(jc, td);
                        if (filterflag)
                        {
                            continue;
                        }

                        if (!longepisode)                                                                                             /* 如果是长期连载的 无视开播时间 */
                        {
                            string time_string = tr.Select("td:eq(0)").First().Select("div").Eq(1).First().Text();
                            if (time_string.IndexOf("分钟") > -1)
                            {

                            }
                            else if (time_string.IndexOf("小时") > -1)
                            {

                            }
                            else if (time_string.IndexOf("一天") > -1)
                            {

                            }
                            else if (time_string.IndexOf("年") > -1)
                            {
                                continue;
                            }
                            else if (time_string.IndexOf("月") > -1)
                            {
                                time_string = time_string.Replace("个月", "").Trim();
                                int months = Int16.Parse(time_string);
                                months *= -1;
                                DateTime dateVal = DateTime.Now.AddMonths(months);//DateTime.ParseExact(time_doc.Text(), "yyyy/MM/dd HH:mm", culture);  /* 片时间 */
                                string monthtmp = comboBox2.Items[comboBox2.SelectedIndex].ToString().Length < 2 ? "0" + comboBox2.Items[comboBox2.SelectedIndex].ToString() : comboBox2.Items[comboBox2.SelectedIndex].ToString();
                                string asdasdsad = comboBox3.Items[comboBox3.SelectedIndex].ToString() + "/"
                                                  + monthtmp + "/01 00:00";
                                DateTime currentSeason = DateTime.ParseExact(asdasdsad, "yyyy/MM/dd HH:mm", culture);                 /* 本季时间 */
                                if (DateTime.Compare(dateVal, currentSeason) < 0)                                                   /* 片要比本季时间大 */
                                    continue;
                            }
                            else if (time_string.IndexOf("天") > -1)
                            {
                                time_string = time_string.Replace("天", "").Trim();
                                int days = Int16.Parse(time_string);
                                days *= -1;
                                DateTime dateVal = DateTime.Now.AddDays(days); //DateTime.ParseExact(time_doc.Text(), "yyyy/MM/dd HH:mm", culture);  /* 片时间 */

                                string monthtmp = comboBox2.Items[comboBox2.SelectedIndex].ToString().Length < 2 ? "0" + comboBox2.Items[comboBox2.SelectedIndex].ToString() : comboBox2.Items[comboBox2.SelectedIndex].ToString();
                                string asdasdsad = comboBox3.Items[comboBox3.SelectedIndex].ToString() + "/"
                                                  + monthtmp + "/01 00:00";
                                DateTime currentSeason = DateTime.ParseExact(asdasdsad, "yyyy/MM/dd HH:mm", culture);                 /* 本季时间 */
                                if (DateTime.Compare(dateVal, currentSeason) < 0)                                                   /* 片要比本季时间大 */
                                    continue;
                            }
                        }

                        Element countt = tr.Select("td:eq(4)").First().Select("span").Eq(2).First();


                        int count = 0;
                        try
                        {
                            count = Int32.Parse(countt.Text());
                        }
                        catch (Exception e)
                        {
                            count = 0;
                        }


                        if (count >= countmax)
                        {
                            Element torrent = tr.Select("td:eq(2)").First().Select("a").First();     /* 磁链地址 */
                            str_url = acg_rip + torrent.Attr("href");
                            countmax = count;
                        }
                    }
                    catch (Exception eeeee)
                    {
                        MessageBox.Show(jc.searchKeyword + " " + jc.episode + " tmd出错了,请联系tmd kenqq");
                        MessageBox.Show(eeeee.ToString());
                        /* str_url = "asd"; */
                    }
                }
            }
            catch (System.Net.WebException e)
            {
                kwList.Add(keywordURL);
                return ("fail");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                kwList.Add(keywordURL);
                return ("fail");
            }
            return (str_url);
        }
        private ArrayList readFansub() /* 读取字幕组 可以手动维护 */
        {
            StreamReader sr = new StreamReader(@fansubtxt, Encoding.GetEncoding("UTF-8"));
            String line;
            StringBuilder sb = new StringBuilder();
            ArrayList al = new ArrayList();
            while ((line = sr.ReadLine()) != null)
            {
                al.Add(line);
            }
            sr.Close();
            return (al);
        }


        private void readArchiveJson() /* 读取json汇总的json */
        {
            MyWebClient webClient = new MyWebClient(dmhycookies);
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


        private void readJson(int month, bool bbb) /* 读取动画详细信息json */
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
            int adasd = "https://bgmlist.com/api/v1/bangumi/archive/".Length;
            jsonName = url.Substring(adasd, url.Length - adasd);

            /* **   本地json没有创建的话 */
            if (System.IO.File.Exists(Path.Combine(appdataFAPI, @jsonName))&&!bbb)
            {
                readLocalJson(Path.Combine(appdataFAPI, @jsonName));
            }
            else
            {
                try
                {
                    var pt = ProxyDetails.ProxyType;
                    //ProxyDetails.ProxyType = (ProxyType)System.Enum.Parse(typeof(ProxyType), "None");

                    MyWebClient webClient = new MyWebClient(dmhycookies);
                    byte[] b = webClient.DownloadData(url);
                    string jsonText = Encoding.UTF8.GetString(b, 0, b.Length);
                    Newtonsoft.Json.Linq.JObject aaaa = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(jsonText);
                    jsonList1 = (Newtonsoft.Json.Linq.JArray)aaaa.GetValue("items");

                    //jsonList1 = new Newtonsoft.Json.Linq.JArray();
                    //foreach (var item in aaaa)
                    //{
                    //    jsonList1.Add(item.Value);
                    //}
                    //jsonList1 = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(jsonText);
                    foreach (Newtonsoft.Json.Linq.JObject a in jsonList1)
                    {
                        a.Add("isOrderRabbit", "0");                                          /* 是否订阅 */
                        a.Add("episode", "01");                                               /* 集数 默认01 */


                        DateTime dt = Convert.ToDateTime(a["begin"].ToString());



                        a.Add("weekDayJP", (int)dt.DayOfWeek+"");  /* 搜索用关键字 默认用json提供的 */
                        a.Add("weekDayCN", (int)dt.DayOfWeek+"");  /* 搜索用关键字 默认用json提供的 */

                        a.Add("titleCN", a["title"]);  /* 搜索用关键字 默认用json提供的 */
                        try
                        {
                            a.Add("titleJP", a["titleTranslate"]["zh-Hans"].First());
                            a.Add("searchKeyword", a["titleTranslate"]["zh-Hans"].First());
                        }
                        catch(Exception eee)
                        {
                            try
                            {
                                //a.Add("titleJP", a["titleTranslate"]["zh-Hant"].First());
                                //a.Add("searchKeyword", a["titleTranslate"]["zh-Hant"].First());
                            }
                            catch (Exception eeee)
                            {
                                try
                                {
                                    //a.Add("titleJP", a["titleTranslate"]["en"].First());
                                    //a.Add("searchKeyword", a["titleTranslate"]["en"].First());
                                }
                                catch (Exception eeeee)
                                {
                                    //a.Add("titleJP", a["title"]);
                                    //a.Add("searchKeyword", a["title"]); 
                                }
                            }
                        }
                        a.Add("fansub", " ");                                                 /* 字幕组 */
                        a.Add("longepisode", "0");                                            /* 长期连载 */
                        a.Add("lastDate", "");                                                /* 上一次完成时间 */
                    }
                    //useDmhyKeyword(jsonList1);
                    if (bbb)
                    {
                        refresh(jsonList1);
                    }
                    else
                    {
                        writeLocalJson(jsonList1, jsonName);
                    }
                    readLocalJson(Path.Combine(appdataFAPI, @jsonName));
                    checkBox1.Checked = false;
                    ProxyDetails.ProxyType = pt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            dataGridView1.DataSource = (from a in jsonList
                                        where Convert.ToInt32(a.weekDayCN) == day
                                        select a).ToList(); ;
        }

        private void refresh(Newtonsoft.Json.Linq.JArray jsonList1)
        {
            foreach (JsonClass jc in jsonList)
            {
                for (int q = jsonList1.Count-1; q > -1; --q)
                {
                    if (jsonList1[q]["titleCN"].ToString().Equals(jc.titleCN))
                    {
                        jsonList1.RemoveAt(q);
                    }
                }
            }
            foreach (Newtonsoft.Json.Linq.JObject a in jsonList1)
            {
                JsonClass jc = new JsonClass();
                jc.titleCN = a["titleCN"].ToString();
                jc.titleJP = a["titleJP"].ToString();
                jc.weekDayCN = a["weekDayCN"].ToString();
                jc.weekDayJP = a["weekDayJP"].ToString();
                jc.isOrderRabbit = a["isOrderRabbit"].ToString();
                jc.episode = a["episode"].ToString();
                jc.searchKeyword = a["searchKeyword"].ToString();
                jc.longepisode = "0";
                jc.lastDate = "";
                jc.fansub = " ";
                jsonList.Add(jc);
            }
            writeLocalJson(jsonList, jsonName);

        }

        private void useDmhyKeyword(Newtonsoft.Json.Linq.JArray jsonList)
        {
            if (jsonList == null || jsonList.Count == 0)
            {
                return;
            }
            //MessageBox.Show(dmhycookies);

            MyWebClient webClient = new MyWebClient(dmhycookies);
            byte[] contentBytes = webClient.DownloadData(dmhyBgmListUrl);
            string content = Encoding.UTF8.GetString(contentBytes, 0, contentBytes.Length);
            int startIndex = content.IndexOf("sunarray.push(['");
            List<string[]> bgmList = new List<string[]>();
            while (startIndex != -1)
            {
                int endIndex = content.IndexOf("','", startIndex);
                if (endIndex == -1)
                {
                    break;
                }
                startIndex = endIndex + 3;
                endIndex = content.IndexOf("','", startIndex);
                if (endIndex == -1)
                {
                    break;
                }
                string bgmName = content.Substring(startIndex, endIndex - startIndex);
                startIndex = endIndex + 3;
                endIndex = content.IndexOf("','", startIndex);
                string bgmKeyword = content.Substring(startIndex, endIndex - startIndex);
                bgmList.Add(new string[] { bgmName, bgmKeyword });
                startIndex = content.IndexOf("array.push(['", endIndex);
            }
            if (bgmList.Count > 0)
            {
                foreach (Newtonsoft.Json.Linq.JObject json in jsonList)
                {
                    foreach (string[] bgm in bgmList)
                    {
                        if (isSameAnimate(json["titleCN"].ToString(), bgm[0]))
                        {
                            json.Remove("searchKeyword");
                            json.Add("searchKeyword", System.Web.HttpUtility.UrlDecode(bgm[1]));
                            bgmList.Remove(bgm);
                            break;
                        }
                    }
                }
            }
        }

        private bool isSameAnimate(string name1, string name2)
        {
            int lcs = GetLCS(name1, name2);
            string shortName = name1.Length > name2.Length ? name2 : name1;
            if ((double)lcs / shortName.Length >= 0.7)
            {
                return true;
            }
            return false;
        }

        private int GetLCS(string str1, string str2)
        {
            int[,] table;
            return GetLCSInternal(str1, str2, out table);
        }

        private int GetLCSInternal(string str1, string str2, out int[,] matrix)
        {
            matrix = null;

            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                return 0;
            }

            int[,] table = new int[str1.Length + 1, str2.Length + 1];
            for (int i = 0; i < table.GetLength(0); i++)
            {
                table[i, 0] = 0;
            }
            for (int j = 0; j < table.GetLength(1); j++)
            {
                table[0, j] = 0;
            }

            for (int i = 1; i < table.GetLength(0); i++)
            {
                for (int j = 1; j < table.GetLength(1); j++)
                {
                    if (str1[i - 1] == str2[j - 1])
                        table[i, j] = table[i - 1, j - 1] + 1;
                    else
                    {
                        if (table[i, j - 1] > table[i - 1, j])
                            table[i, j] = table[i, j - 1];
                        else
                            table[i, j] = table[i - 1, j];
                    }
                }
            }

            matrix = table;
            return table[str1.Length, str2.Length];
        }

        private void readLocalJson(string jsonName) /* 读取动画详细信息json 本地 */
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


        private void writeLocalJson(List<JsonClass> jsl, string jsonName)             /* 更新动画信息json */
        {
            string json = JsonConvert.SerializeObject(jsl);
            System.IO.File.WriteAllText(Path.Combine(appdataFAPI, @jsonName), json);
        }


        private void writeLocalJson(Newtonsoft.Json.Linq.JArray jsl, string jsonName) /* 更新动画信息json 第一次自定义更新结构 */
        {
            System.IO.File.WriteAllText(Path.Combine(appdataFAPI, @jsonName), jsl.ToString());
        }


        private void threadMethod(object a)
        {
            _syncContext.Post(SetLabelText, a.ToString());        /* 子线程中通过UI线程上下文更新UI */
        }


        private void threadMethod1(object a)
        {
            _syncContext.Post(btnTotorrent, null);                /* 子线程中通过UI线程上下文更新UI */
        }


        private void SetLabelText(object text)
        {
            this.label1.Text = text.ToString();
            this.label1.Refresh();
        }


        private void btnTotorrent(object text) /* 处理按钮 */
        {
            kwList = new List<string>();
            handling = true;
            string keyword = null;
            string torrentList = null;
            int Rabbits = -1;
            IFormatProvider culture = new CultureInfo("en-US", true);
            //DateTime dateVal;
            DateTime now = DateTime.Now;
            //TimeSpan ts;
            int days = 0;

            List<JsonClass> oklist = new List<JsonClass>();        /* 已经处理列表(抓取到种子的) */
            List<JsonClass> handllist = new List<JsonClass>();        /* 已经处理列表 (包括成功的,找不到的)*/
            List<string> errorlist = new List<string>();           /*有问题列表 */
            List<string> strCmdText = new List<string>();

            try
            {
                while (Rabbits != 0)
                {
                    Rabbits = 0;
                    foreach (JsonClass jc in jsonList)
                    {
                        if (jc.lastDate == null || jc.lastDate.Length < 1)
                        {
                        }
                        else
                        {
                            if (checkBox3.CheckState == CheckState.Checked)
                            {
                            }
                            else
                            {
                                //dateVal = DateTime.ParseExact(jc.lastDate, "yyyy-MM-dd HH:mm", culture);
                                //ts = dateVal - now;
                                //days = (int)Math.Round(ts.TotalDays); /* 相差天数 ; */
                                days = getdaybystirng(jc.lastDate);
                                if (!(days <= 0))
                                {
                                    continue;
                                }
                            }
                        }


                        if (jc.isOrderRabbit == "1")
                        {
                            ++Rabbits;
                            if (jc.maxepisode != null)
                            {
                                if (jc.maxepisode == jc.episode)
                                {
                                    handllist.Add(jc);
                                    jc.isOrderRabbit = "0";
                                    break;
                                }
                            }
                            bool bb = jc.longepisode == "1" ? true : false;
                            string filterEpisode = "";
                            if (!bb)
                            {
                                if (jc.episode.Equals(comboBox2.SelectedItem.ToString()))
                                {
                                    int episode_before = Int32.Parse(jc.episode) - 1;
                                    for (int q = episode_before; q > -1; --q)
                                    {
                                        filterEpisode += " -" + (q < 10 ? "0" + q : q + "");
                                    }
                                }
                            }


                            if (comboBox4.SelectedItem.ToString().IndexOf("ktxp") > -1)
                            {
                                keyword = jc.searchKeyword + " " + jc.episode + " " + jc.fansub + filterEpisode;
                                keyword = keyword.Replace("\u3000", " ");
                                keyword = getTorrentFromKtxp(keyword, bb, jc);
                            }
                            else if (comboBox4.SelectedItem.ToString().IndexOf("dmhy(anoneko)") > -1)
                            {
                                keyword = jc.searchKeyword + " " + jc.episode + " " + jc.fansub;
                                keyword = keyword.Replace("\u3000", " ");
                                dmhyUrl = "https://share.dmhy.org/";
                                keyword = getTorrentFromDmhy(keyword, bb, jc);
                            }
                            else if (comboBox4.SelectedItem.ToString().IndexOf("dmhy(带cookies)") > -1)
                            {
                                keyword = jc.searchKeyword + " " + jc.episode + " " + jc.fansub;
                                keyword = keyword.Replace("\u3000", " ");
                                dmhyUrl = "https://share.dmhy.org/";
                                if (dmhycookies.Length==0)
                                {
                                    dmhycookies = getDmhyCookies();
                                }
                                keyword = getTorrentFromDmhy(keyword, bb, jc);
                            }
                            else if (comboBox4.SelectedItem.ToString().IndexOf("nya") > -1)
                            {
                                keyword = jc.searchKeyword + " " + jc.episode + " " + jc.fansub + filterEpisode;
                                keyword = keyword.Replace("\u3000", " ");
                                //keyword = getTorrentFromNyaa(keyword, bb);
                                keyword = getTorrentFromNyaaNew(keyword, bb, jc);
                            }
                            else if (comboBox4.SelectedItem.ToString().IndexOf("临时版") > -1)
                            {
                                keyword = getTorrentFromDmhy_1(jc.searchKeyword, jc.episode, jc.fansub, filterEpisode);
                            }
                            else if (comboBox4.SelectedItem.ToString().IndexOf("acg.rip") > -1)
                            {
                                keyword = jc.searchKeyword + " " + jc.episode + " " + jc.fansub;
                                keyword = keyword.Replace("\u3000", " ");
                                keyword = getTorrentFromAcgRip(keyword, bb, jc);
                            }


                            if (keyword != null && keyword != "nothing" && keyword != "fail" && keyword != "time")
                            {
                                if (comboBox4.SelectedItem.ToString().IndexOf("dmhy") > -1 || comboBox4.SelectedItem.ToString().IndexOf("nya") > -1)
                                {
                                    if (checkBox4.CheckState != CheckState.Checked)
                                    {
                                        torrentList += keyword + "\n";
                                        strCmdText.Add(keyword);
                                    }
                                    else
                                    {
                                        torrentList += keyword + "\n";
                                        //strCmdText.Add(keyword);
                                    }
                                }
                                else
                                {
                                    torrentList += keyword + "\n";
                                }
                                int iii = Int32.Parse(jc.episode) + 1;
                                int jc_episode_length = jc.episode.Length;
                                if (jc_episode_length==2)
                                {
                                    jc.episode = iii < 10 ? "0" + iii : iii + "";
                                }
                                else
                                {
                                    if (iii < 10)
                                    {
                                        jc.episode = "00" + iii;
                                    }else if (iii >= 10 && iii < 100)
                                    {
                                        jc.episode = "0" + iii;
                                    }
                                    else
                                    {
                                        jc.episode = iii + "";
                                    }
                                    
                                }
                                oklist.Add(jc);
                            }
                            else if (keyword.IndexOf("nothing") != -1)
                            {
                                handllist.Add(jc);
                                jc.isOrderRabbit = "0";
                            }
                            else if (keyword.IndexOf("fail") != -1)
                            {
                                handllist.Add(jc);
                                errorlist.Add(jc.titleCN + "(未知原因错误)");
                                jc.isOrderRabbit = "0";
                            }
                            else if (keyword.IndexOf("time") != -1)
                            {
                                handllist.Add(jc);
                                errorlist.Add(jc.titleCN + "(当集出片时间不是当前季度,请勾选年翻)");
                                jc.isOrderRabbit = "0";
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            foreach (JsonClass jc in handllist)
            {
                JsonClass aaa = (from a in jsonList
                                 where a.titleCN == jc.titleCN
                                 select a).ToList()[0];
                jc.isOrderRabbit = "1";
            }

            try
            {
                foreach (JsonClass jc in oklist)
                {
                    JsonClass aaa = (from a in jsonList
                                     where a.titleCN == jc.titleCN
                                     select a).ToList()[0];
                    if (aaa.weekDayJP == null)
                    {
                        jc.lastDate = getlastDate(aaa.weekDayCN, now);
                    }
                    else
                    {
                        jc.lastDate = getlastDate(aaa.weekDayJP, now);
                    }

                    /* now.ToString("yyyy-MM-dd HH:mm"); */
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }



            if (strCmdText.Count > 0 && MyIni.Read("downloadSoftPath")!=null)
            {
                foreach (string s in strCmdText)
                {
                    if (s.Length>255)
                    {
                        System.Diagnostics.Process.Start(MyIni.Read("downloadSoftPath"), s.Substring(0,255));
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(MyIni.Read("downloadSoftPath"), s);
                    }
                    
                }
            }
            else
            {
                if (torrentList != null)
                {
                    Clipboard.SetDataObject(torrentList);
                }
                totxt(torrentList);
            }

            this.dataGridView1.Refresh();

           
            if (kwList.Count != 0 || errorlist.Count != 0)
            {
                string sisisi = "";
                foreach (string asd in kwList)
                {
                    sisisi += asd + " ";
                }

                foreach (string jc in errorlist)
                {
                    sisisi += jc + " ";
                }


                this.label1.Text = "成功,but:" + sisisi;
            }
            else
            {
                this.label1.Text = @"
＿人人人人人人人人人人人人＿
＞　　はいはい、ゆっくりゆっくりｗ　＜
￣^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ^Ｙ￣
";
            }

            handling = false;
            writeLocalJson(jsonList, jsonName);
            this.notifyIcon1.ShowBalloonTip(1000, "Mission complete", @"朝だぞ。人間が朝の6時に、起きれるか！", ToolTipIcon.Info);//
            //changWindowsSize();
            if (handllist.Count == 1)
            {
                if (checkBox5.CheckState != CheckState.Checked)
                    MessageBox.Show("完成鸟");
            }
            dmhycookies = "";
        }

        private string getTorrentFromNyaaNew(string keywordURL, bool longepisode, JsonClass jc) /* 获取种子 */
        {
            bool bb = jc.longepisode == "1" ? true : false;
            List<string> filterEpisode = new List<string>();
            if (!bb)
            {
                /*
                 * if (jc.episode.Equals(comboBox2.SelectedItem.ToString()))
                 * {
                 */

                int countttttt = 0;
                try
                {
                    countttttt = Int32.Parse(jc.episode);
                }
                catch (Exception e)
                {
                    countttttt = 0;
                }
                int episode_before = countttttt - 1;

                for (int q = episode_before; q > -1; --q)
                {
                    filterEpisode.Add((q < 10 ? "0" + q : q + ""));
                }
                /* } */
            }
            string str_url = "nothing";
            keywordURL = keywordURL.Replace("\"", "");

            if ((from a in kwList
                 where a == keywordURL
                 select a).ToList().Count == 0)
            {
                /* kwList.Add(keywordURL); */
            }
            else
            {
                kwList.Add(keywordURL);
                return ("fail");
            }
            try
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                /*下载网页源代码 */
                MyWebClient webClient = new MyWebClient(dmhycookies);

                string dmhy = dmhyUrl;

                string url = MyIni.Read("nyaQueryString") + keywordURL;


                /* string url = "dmhyUrl/"; */

                string htmlString = Encoding.GetEncoding("utf-8").GetString(webClient.DownloadData(url));

                Document doc = NSoup.NSoupClient.Parse(htmlString);

                NSoup.Select.Elements tables = doc.Select("tbody");

                //table table-bordered table-hover table-striped torrent-list

                if (tables.Count == 0)
                {
                    return ("nothing");
                }
                Element table = tables.First();
                int trCount = table.Select("tr").Count;       /* 找不到结果也会是1 */
                Element tr = null;
                for (var i = 0; i < trCount; ++i)                     /* 第一行是标题 */
                {
                    try
                    {

                        tr = table.Select("tr:eq(" + i + ")").First();

                        bool filterflag = false;
                        Element td = tr.Select("td:eq(1)").First();

                        filterflag = filter(jc, td);
                        if (filterflag)
                        {
                            continue;
                        }

                        if (!longepisode)                                                                                             // 如果是长期连载的 无视开播时间 
                        {
                            Element time_doc = tr.Select("td:eq(4)").First();
                            DateTime dateVal = DateTime.ParseExact(time_doc.Text(), "yyyy-MM-dd HH:mm", culture);  // 片时间 
                            string monthtmp = comboBox2.Items[comboBox2.SelectedIndex].ToString().Length < 2 ? "0" + comboBox2.Items[comboBox2.SelectedIndex].ToString() : comboBox2.Items[comboBox2.SelectedIndex].ToString();
                            string asdasdsad = comboBox3.Items[comboBox3.SelectedIndex].ToString() + "-"
                                              + monthtmp + "-01 00:00";
                            DateTime currentSeason = DateTime.ParseExact(asdasdsad, "yyyy-MM-dd HH:mm", culture);                 // 本季时间 
                            if (DateTime.Compare(dateVal, currentSeason) < 0)                                                   // 片要比本季时间大 
                                continue;
                        }




                        /* Element torrent = tr.Select("td:eq(3)").First().Select("a[class=download-arrow arrow-torrent]").First(); / * 种子地址 * / */
                        Element torrent = tr.Select("td:eq(2)").First().Select("a:eq(0)").First();     /* 磁链地址 */
                        Element magnet = tr.Select("td:eq(2)").First().Select("a:eq(1)").First();                           /* 内部链接地址 用于获取地址 */


                        if (checkBox4.CheckState == CheckState.Checked)
                        {
                            str_url = "https://nyaa.si" + torrent.Attr("href");
                        }
                        else
                        {
                            str_url = magnet.Attr("href");
                        }

                    }
                    catch (Exception eeeee)
                    {
                        MessageBox.Show(jc.searchKeyword + " " + jc.episode + " tmd出错了,请联系tmd kenqq");
                        MessageBox.Show(eeeee.ToString());
                        /* str_url = "asd"; */
                    }
                }
            }
            catch (System.Net.WebException e)
            {
                kwList.Add(keywordURL);
                return ("fail");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                kwList.Add(keywordURL);
                return ("fail");
            }
            return (str_url);
        }

        private void totxt(string torrentList)
        {
            if (torrentList == null || torrentList.Length < 1)
            {
                return;
            }
            string a = appdataFAPI + "/torrent/";
            if (!Directory.Exists(a))         /* 判断文件夹是否已经存在 */
            {
                Directory.CreateDirectory(a);      /* 创建文件夹 */
            }
            string torrentsTxt = Path.Combine(a, DateTime.Now.ToString("yyyyMMddHHmmssffffff") + ".txt");
            System.IO.File.WriteAllText(torrentsTxt, torrentList);
            if (MyIni.Read("torrentPath") != null && !MyIni.Read("torrentPath").Equals(""))
            {
                string torrentsTxtDown = "";
                if (ProxyDetails.FullProxyAddress != null)
                {
                    torrentsTxtDown += " -e \"http_proxy=http://" + ProxyDetails.FullProxyAddress + "\"   ";
                    torrentsTxtDown += " -e \"https_proxy=http://" + ProxyDetails.FullProxyAddress + "\"   ";
                }
                torrentsTxtDown +=
                  " -d --header=\"User-Agent: Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:49.0) Gecko/20100101 Firefox/49.0\"  --header=\"Accept-Encoding: gzip, deflate, br\"";
                torrentsTxtDown += " -c -w 6 -t 0 -T 30 -i ";
                torrentsTxtDown += torrentsTxt + @" -P " + MyIni.Read("torrentPath");

                //torrentsTxtDown += " --load-cookies cookies.txt"; // firefox   Export Cookies



                torrentsTxtDown += " --no-check-certificate";



                Process.Start("wget.exe", torrentsTxtDown);
                //-c 断点续传 -w 等待时间  -t 重试次数 -T timeout秒数 -i 读取文件 -P 保存路径
            }
        }


        private string getlastDate(string p, DateTime now)
        {
            int i = Convert.ToInt32(now.DayOfWeek);
            TimeSpan ts = new TimeSpan(0, 0, 0);
            now = now.Date + ts;

            if (i.ToString().Equals(p))
            {
                now = now.AddDays(7);
                /* return now.ToString("yyyy-MM-dd HH:mm"); */
            }
            else
            {
                while (!i.ToString().Equals(p))
                {
                    now = now.AddDays(1);
                    i = Convert.ToInt32(now.DayOfWeek);
                }
            }
            /* now = now.AddDays(7); */
            return (now.ToString("yyyy-MM-dd HH:mm"));
        }


        private string getTorrentFromDmhy_1(string searchKeyword, string episode, string fansub, string filterEpisode)
        {
            string str_url = "nothing";
            string keywordURL = searchKeyword + " " + episode + " " + fansub + filterEpisode;
            keywordURL = keywordURL.Replace("\"", "");

            if ((from a in kwList
                 where a == keywordURL
                 select a).ToList().Count == 0)
            {
                /* kwList.Add(keywordURL); */
            }
            else
            {
                kwList.Add(keywordURL);
                return ("fail");
            }


            string dmhy = dmhyUrl;

            ArrayList lada = DmhyFiveDoces();

            foreach (Document doc in lada)
            {
                int countmax = 0;
                int trCount = doc.Select("table#topic_list").First().Select("tr").Count;        /* 找不到结果也会是1 */
                for (var i = 1; i < trCount - 1; ++i)                                                         /* 第一行是标题 */
                {
                    try
                    {
                        Element title = doc.Select("table#topic_list").First().Select("tr:eq(" + i + ")").First()
                                .Select("td:eq(2)").First().Select("a").Eq(1).First();
                        string asd = title.Text();
                        if (asd.IndexOf(searchKeyword) > -1)
                        {
                            if (asd.IndexOf(episode) > -1)
                            {
                                Element countt = doc.Select("table#topic_list").First().Select("tr:eq(" + i + ")").First().Select("td:eq(7)").First();
                                int count = 0;
                                try
                                {
                                    count = Int32.Parse(countt.Text());
                                }
                                catch (Exception e)
                                {
                                    count = 0;
                                }
                                if (count >= countmax)
                                {
                                    /* Element torrent = doc.Select("table#topic_list").First().Select("tr:eq(" + i + ")").First().Select("td:eq(3)").First().Select("a[class=download-arrow arrow-torrent]").First(); / * 种子地址 * / */

                                    Element magnet = doc.Select("table#topic_list").First().Select("tr:eq(" + i + ")").First()
                                             .Select("td:eq(3)").First().Select("a[class=download-arrow arrow-magnet]").First(); /* 磁链地址 */
                                    countmax = count;
                                    if (checkBox4.CheckState == CheckState.Checked)
                                    {
                                        /* str_url = dmhy + torrent.Attr("href"); */
                                        str_url = magnet.Attr("href");
                                    }
                                    else
                                    {
                                        str_url = magnet.Attr("href");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        /* str_url = "asd"; */
                    }
                }
            }
            return (str_url);
        }


        private static ArrayList DmhyFiveDoces()
        {
            try
            {
                if (lad == null)
                {
                    lad = new ArrayList();
                    IFormatProvider culture = new CultureInfo("en-US", true);
                    /*下载网页源代码 */
                    MyWebClient webClient = new MyWebClient(dmhycookies);
                    string url;

                    for (int i = 1; i < 4 + 1; ++i)
                    {
                        url = dmhyUrl + "topics/list/sort_id/2/page/" + i;
                        lad.Add(NSoup.NSoupClient.Parse(Encoding.GetEncoding("utf-8").GetString(webClient.DownloadData(url))));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }


            return (lad);
        }


        private void button1_Click(object sender, System.EventArgs e)
        {
            FAPI();
            changWindowsSize();
        }

        private void FAPI()
        {
            Thread demoThread = new Thread(new ParameterizedThreadStart(threadMethod));
            demoThread.IsBackground = true;
            demoThread.Start("处理中");      /* 启动线程 */

            demoThread = new Thread(new ParameterizedThreadStart(threadMethod1));
            demoThread.IsBackground = true;
            demoThread.Start();             /* 启动线程 */
        }


        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!handling)
            {
                try
                {
                    string asdasd = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    JsonClass aaa = (from a in jsonList
                                     where a.titleCN == asdasd
                                     select a).ToList()[0];
                    /* aaa.lastDate = ""; */
                    writeLocalJson(jsonList, jsonName);
                    dataGridView1.Refresh();
                }
                catch (Exception ee)
                {

                }



            }
        }


        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) /* 更新列表 */
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
            setconfig();
        }


        private void comboBox1comboBox2_SelectedIndexChanged()
        {
            if (initflag)
            {
                readJson(Int32.Parse(comboBox2.SelectedItem.ToString()),false);
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
                this.Width = 1200;
                comboBox1comboBox2_SelectedIndexChanged1();
            }
        }


        private static string readdownloadsoftpath() /* 更新下载软件路径 */
        {
            if (System.IO.File.Exists(downloadsofttxt))
            {
                StreamReader sr = new StreamReader(downloadsofttxt, Encoding.GetEncoding("UTF-8"));
                String line;
                StringBuilder sb = new StringBuilder();
                line = sr.ReadLine();
                sr.Close();
                return (line);
            }
            else
            {
                System.IO.File.WriteAllText(downloadsofttxt, "");
                return ("");
            }
        }


        private void button2_Click(object sender, EventArgs e) /* 打开浏览文件选取下载软件 */
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            string fileToOpen = "";
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileToOpen = FD.FileName;
                MyIni.Write("downloadSoftPath", fileToOpen);
                downloadsoftpath = MyIni.Read("downloadSoftPath");
                //System.IO.File.WriteAllText(downloadsofttxt, fileToOpen);
                //downloadsoftpath = readdownloadsoftpath();
            }
        }


        private void button3_Click(object sender, EventArgs e)        /* 帮助 */
        {
            string asd = @"1.  本软件原理,把你订阅的片的种子复制到粘贴板上,然后调用下载软件来下载
2.  勾选需要订阅的片,根据需要编辑该片集数，字幕组和修正后的关键字(请自行去该站点搜索一次)(非必要)
3.  点击 设置下载软件 设置你的下载软件位置(必须)
4.  设置好后点击左上角按钮(FAPI!)，种子文件连接会添加到剪切板
5.  如果是年番,请勾选longepisode,这样就可以无视 片时间>本季时间 (eg:8月22号发布的片>7月1号 )
6.  右上角的无视时间限制,是用来取消 下次执行时间 限制的
7.  add 添加一个新的片 del 删除 可多选
8.  转移 用于换季的时候将年翻进度转移到新一季
";
            MessageBox.Show(asd);
        }


        private void button4_Click(object sender, EventArgs e)        /* 新增 */
        {
            string name = prompt.ShowDialog("input your title", "我爱冷泉麻子");
            if (name != "")
            {
                string[] lines = name.Split('\n');
                for (int i = 0; i < lines.Length; i += 1)
                    lines[i] = lines[i].Trim();


                for (int i = 0; i < lines.Length; i += 1)
                {
                    JsonClass jc = new JsonClass();
                    jc.titleCN = lines[i];
                    jc.weekDayCN = comboBox1.SelectedIndex + "";
                    jc.weekDayJP = comboBox1.SelectedIndex + "";
                    jc.isOrderRabbit = "0";
                    jc.episode = "01";
                    jc.searchKeyword = lines[i];
                    jc.fansub = " ";
                    jsonList.Add(jc);
                    writeLocalJson(jsonList, jsonName);
                    comboBox1comboBox2_SelectedIndexChanged();
                }
            }
        }


        private void button5_Click(object sender, EventArgs e) /*删除 */
        {
            /* del */
            if (MessageBox.Show("不看该片了嘛?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                /* delete */
                foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                {
                    jsonList.RemoveAll(a => a.titleCN == dataGridView1.Rows[item.Index].Cells[1].Value.ToString());
                    /* dataGridView1.Rows.RemoveAt(item.Index); */
                }
                writeLocalJson(jsonList, jsonName);
                comboBox1comboBox2_SelectedIndexChanged();
            }
        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string q;
            if (checkBox2.Checked == true)
            {
                q = "1";
            }
            else
            {
                q = "0";
            }
            List<JsonClass> handllist = (from a in jsonList
                                         where Convert.ToInt32(a.weekDayCN) == comboBox1.SelectedIndex
                                         select a).ToList();
            foreach (JsonClass jc in handllist)
            {
                jc.isOrderRabbit = q;
            }
            writeLocalJson(jsonList, jsonName);
            this.dataGridView1.Refresh();
        }


        private void label1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();

            Process.Start(appdataFAPI);
        }


        private void button6_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(comboBox1, comboBox3, comboBox2, this);
            f3.ShowDialog();
        }


        public bool Ping(string ip)
        {
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000; /* Timeout 时间，单位：毫秒 */
            try
            {
                System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
                if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    return (true);
                else
                    return (false);
            }
            catch (PingException e)
            {
                MessageBox.Show(@"ping 异常:找不到" + ip + "哦");
            }
            return (false);
        }


        private void button7_Click(object sender, EventArgs e)
        {
            Process.Start(fansubtxt);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            /* get bilibili html for bangumi */

            /*

             try
             {
                 MyWebClient webClient = new MyWebClient(dmhycookies);
                 string urlaaaaaaaaa = "http://www.bilibili.com/list/b--a2-2015-t-0--0-d---3.html";
                 string htmlString = "";
                 string bilibili = "http://www.bilibili.com";

                 htmlString = Encoding.GetEncoding("utf-8").GetString(webClient.DownloadData(urlaaaaaaaaa));

                 Document doc = NSoup.NSoupClient.Parse(htmlString);
                 Document doctmp;

                 NSoup.Select.Elements table = doc.Select("ul[class=v_ul]").First().Select("li");


                 ArrayList al = new ArrayList();
                 ArrayList a2 = new ArrayList();
                 foreach (Element a in table)`
                 {
                     string href = bilibili + a.Select("div[class=t]").First().Select("a").First().Attr("href");
                     string title = a.Select("div[class=t]").First().Select("a").First().Attr("title");

                     htmlString = Encoding.GetEncoding("utf-8").GetString(webClient.DownloadData(href));
                     doctmp = NSoup.NSoupClient.Parse(htmlString);
                     string w = doctmp.Html();
                     int ewqeqwwqewe = w.IndexOf("spid =") + "spid =".Length;
                     int ewqeqwwqewe1 = w.IndexOf("var is");
                     if (ewqeqwwqewe1 == -1)
                     {
                         a2.Add(href + "," + title);
                         continue;
                     }

                     string sid = w.Substring(ewqeqwwqewe, ewqeqwwqewe1 - ewqeqwwqewe);
                     sid = System.Text.RegularExpressions.Regex.Replace(sid, @"[^\d]", "");
                     //vidbox zt
                     al.Add("http://www.bilibili.com/sppage/bangumi-" + sid + "-1.html," + title);
                 }
                 string sum = "";
                 foreach (string a in al)
                 {
                     //string bfr = a.Split(',')[0];
                     //htmlString = Encoding.GetEncoding("utf-8").GetString(webClient.DownloadData(bfr));
                     //doctmp = NSoup.NSoupClient.Parse(htmlString);
                     //NSoup.Select.Elements ulul = doctmp.Select("ul[class=vidbox zt]").First().Select("li");
                     //foreach (Element ty in ulul)
                     //{
                     //    string href = bilibili + ty.Select("a").First().Attr("href");
                     //    string title = ty.Select("a").First().Text();
                     //}
                     sum += a + "\n";
                 }
                 foreach (string a in a2)
                 {
                     sum += a + "\n";
                 }

                 sum += "";
             }
             catch (Exception dasdasdasd)
             {
                 string asd222 = "";
             }

              */


            /* get pokemon pkm */


            /*
             * try
             * {
             *  MyWebClient webClient = new MyWebClient(dmhycookies);
             *  string urlaaaaaaaaa = "http://www.pokedit.com/download/default-pkm/pokemon-black/";
             *  string htmlString = "";
             *
             *
             *  htmlString = Encoding.GetEncoding("utf-8").GetString(webClient.DownloadData(urlaaaaaaaaa));
             *
             *  Document doc = NSoup.NSoupClient.Parse(htmlString);
             *
             *  NSoup.Select.Elements table = doc.Select("table.download-main-table").First().Select("tr.dl-row");
             *  ArrayList al = new ArrayList();
             *  foreach (Element a in table)
             *  {
             *      al.Add("http://www.pokedit.com" + a.Select("td.dl-cel-dl").First().Select("a[class=dl-cell-link]").First().Attr("href"));
             *  }
             *  Document tmp;
             *  Element dlform;
             *  System.Collections.Specialized.NameValueCollection reqparm;
             *  string topic_id;
             *  string s;
             *  string n;
             *  string g;
             *  int i = 1;
             *
             *  System.Net.ServicePointManager.Expect100Continue = false;
             *  int aaa = System.IO.Directory.GetFiles("d:/pkm", "*.pkm", SearchOption.AllDirectories).Length;
             *  foreach (string a in al)
             *  {
             *      if(i<aaa){
             * ++i;
             *          continue;
             *      }
             *
             *      htmlString = Encoding.GetEncoding("utf-8").GetString(webClient.DownloadData(a));
             *      tmp = NSoup.NSoupClient.Parse(htmlString);
             *      dlform = tmp.Select("#dlform").First();
             *
             *      topic_id = dlform.Select("input[name=topic_id]").First().Attr("value");
             *      s = dlform.Select("input[name=s]").First().Attr("value");
             *      n = dlform.Select("input[name=n]").First().Attr("value");
             *      g = dlform.Select("input[name=g]").First().Attr("value");
             *
             *      reqparm = new System.Collections.Specialized.NameValueCollection();
             *      reqparm.Add("topic_id", topic_id);
             *      reqparm.Add("s", s);
             *      reqparm.Add("n", n);
             *      reqparm.Add("g", g);
             *
             *      byte[] responsebytes = webClient.UploadValues("http://www.pokedit.com/pkmhelper/dfpkmgener.php", "POST", reqparm);
             *      ByteArrayToFile("d:/pkm/" + i + ".pkm", responsebytes);
             * ++i;
             *  }
             * }
             * catch (Exception dasdasdasd)
             * {
             *  string asd222 = "";
             * }
             */
        }


        public bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                /* Open file for reading */
                System.IO.FileStream _FileStream =
                    new System.IO.FileStream(_FileName, System.IO.FileMode.Create,
                                  System.IO.FileAccess.Write);


                /*
                 * Writes a block of bytes to this stream using data from
                 * a byte array.
                 */
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                /* close file stream */
                _FileStream.Close();

                return (true);
            }
            catch (Exception _Exception)
            {
                /* Error */
                Console.WriteLine("Exception caught in process: {0}",
                           _Exception.ToString());
            }

            /* error occured, return false */
            return (false);
        }


        private void label1_Click_1(object sender, EventArgs e)
        {
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["clearTime"].Index && e.RowIndex >= 0)
            {
                string asdasd = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                JsonClass aaa = (from a in jsonList
                                 where a.titleCN == asdasd
                                 select a).ToList()[0];
                aaa.lastDate = "";
                writeLocalJson(jsonList, jsonName);
                dataGridView1.Refresh();
            }
    //        if (e.ColumnIndex == dataGridView1.Columns["upvote"].Index && e.RowIndex >= 0)
    //        {
    //            var macAddr =
    //(
    //    from nic in NetworkInterface.GetAllNetworkInterfaces()
    //    where nic.OperationalStatus == OperationalStatus.Up
    //    select nic.GetPhysicalAddress().ToString()
    //).FirstOrDefault();

    //            string asdasd = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
    //            var client = new RestClient(fapiServerUrl);
    //            var request = new RestRequest("FAPI/rs/fapiService/vote", Method.POST);
    //            request.RequestFormat = DataFormat.Json;
    //            request.AddBody(new VoteDTO
    //            {
    //                title = asdasd,
    //                macAddress = macAddr,
    //                voteType = 0
    //            });
    //            IRestResponse response = client.Execute(request);
    //            var content = response.Content;
    //            Newtonsoft.Json.Linq.JObject aaaa = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(content);
    //            string[] aaaaaaa = aaaa.GetValue("message").ToString().Split(new string[] { "," }, StringSplitOptions.None);

    //            JsonClass aaa = (from a in jsonList
    //                             where a.titleCN == asdasd
    //                             select a).ToList()[0];
    //            aaa.upvote = aaaaaaa[0];
    //            aaa.downvote = aaaaaaa[1];
    //            writeLocalJson(jsonList, jsonName);
    //            dataGridView1.Refresh();
    //        }
    //        if (e.ColumnIndex == dataGridView1.Columns["downvote"].Index && e.RowIndex >= 0)
    //        {
    //            var macAddr =
    //(
    //    from nic in NetworkInterface.GetAllNetworkInterfaces()
    //    where nic.OperationalStatus == OperationalStatus.Up
    //    select nic.GetPhysicalAddress().ToString()
    //).FirstOrDefault();

    //            string asdasd = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
    //            var client = new RestClient(fapiServerUrl);
    //            var request = new RestRequest("FAPI/rs/fapiService/vote", Method.POST);
    //            request.RequestFormat = DataFormat.Json;
    //            request.AddBody(new VoteDTO
    //            {
    //                title = asdasd,
    //                macAddress = macAddr,
    //                voteType = 1
    //            });
    //            IRestResponse response = client.Execute(request);
    //            var content = response.Content;
    //            Newtonsoft.Json.Linq.JObject aaaa = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(content);
    //            string[] aaaaaaa = aaaa.GetValue("message").ToString().Split(new string[] { "," }, StringSplitOptions.None);

    //            JsonClass aaa = (from a in jsonList
    //                             where a.titleCN == asdasd
    //                             select a).ToList()[0];
    //            aaa.upvote = aaaaaaa[0];
    //            aaa.downvote = aaaaaaa[1];
    //            writeLocalJson(jsonList, jsonName);
    //            dataGridView1.Refresh();
    //        }
        }


        public DataGridView getdataGridView1()
        {
            this.dataGridView1.Refresh();
            return (this.dataGridView1);
        }


        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }


        private void Form1_Resize(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Process.Start(filterListtxt);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            setconfig();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            setconfig();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            setconfig();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            setconfig();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            proxyDialog a = new proxyDialog();
            a.ShowDialog();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            changWindowsSize();
        }

        private void changWindowsSize()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = true;
                this.ShowInTaskbar = true;                         /* 显示在系统任务栏 */
                this.WindowState = FormWindowState.Normal;       /* 还原窗体 */
                /* this.notifyIcon1.Visible = false;  //托盘图标隐藏 */
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
            comboBox1comboBox2_SelectedIndexChanged1();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (initflag)
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.Hide();
                    this.notifyIcon1.Visible = true;
                }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Sure?", "确定更新bgmlist本季新添加的片吗?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                readJson(Int32.Parse(comboBox2.SelectedItem.ToString()), true);
                dataGridView1.Refresh();
                MessageBox.Show(@"更新完成");
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            bool b = true;
            if (b)
            {
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Sure?", "确定提交关键词吗?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (JsonClass json in jsonList)
                {
                    var client = new RestClient(fapiServerUrl);
                    var request = new RestRequest("FAPI/rs/fapiService/keywordfix", Method.POST);
                    request.RequestFormat = DataFormat.Json;
                    request.AddBody(new KeywordDTO
                    {
                        title = json.titleCN,
                        keyword = json.searchKeyword
                    });
                    IRestResponse response = client.Execute(request);
                }
                dataGridView1.Refresh();
                MessageBox.Show(@"提交完成");
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

        }
        private void comboBox1comboBox2_SelectedIndexChanged1()
        {

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                try
                {
                    string asdasd = "";
                    if (dataGridView1.Rows[i].Cells[15] != null && dataGridView1.Rows[i].Cells[15].Value!=null)
                    {
                        asdasd = dataGridView1.Rows[i].Cells[15].Value.ToString();
                    }
                    if (asdasd.Equals(""))
                    {
                        dataGridView1.Rows[i].Cells[15].Style.BackColor = Color.Red;
                        continue;
                    }


                    int days = getdaybystirng(asdasd);

                    if (days < 0)
                    {
                        dataGridView1.Rows[i].Cells[15].Style.BackColor = Color.Red;
                    }
                    else if (days == 0)
                    {
                        dataGridView1.Rows[i].Cells[15].Style.BackColor = Color.Yellow;
                    }


                }
                catch (Exception ee)
                {

                }
            }

        }

        private static int getdaybystirng(string asdasd)
        {
            if (asdasd == null || asdasd.Equals(""))
            {
                return -1;
            }

            var newDate = DateTime.ParseExact(asdasd,
                          "yyyy-MM-dd HH:mm",
                           CultureInfo.InvariantCulture);


            DateTime now = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd",
                                CultureInfo.InvariantCulture),
                          "yyyy-MM-dd",
                           CultureInfo.InvariantCulture);



            TimeSpan diff = newDate - now;
            double asd = diff.TotalDays;
            int days = (int)Math.Round(asd);
            return days;
        }
    }
}
