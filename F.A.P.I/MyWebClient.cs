using Seringa.Engine.Enums;
using Seringa.Engine.Interfaces;
using Seringa.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Seringa.Engine.Implementations.Proxy;

namespace F.A.P.I
{
    class MyWebClient : WebClient
    {
        public IProxyDetails ProxyDetails { get; set; }
        public string UserAgent { get; set; }


        //time in milliseconds
        private int timeout;
        private string cookies; 

        

        public int Timeout
        {
            get
            {
                return timeout;
            }
            set
            {
                timeout = value;
            }
        }

        public MyWebClient()
        {
            this.timeout = 60000;
        }

        public MyWebClient(int timeout)
        {
            this.timeout = timeout;
        }

        public MyWebClient(string cookies)
        {
            this.cookies = cookies;
            this.timeout = 60000;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest result = null;
            ProxyDetails = Form1.ProxyDetails;
            //ProxyDetails.ProxyType = ProxyType.Socks;
            //ProxyDetails.ProxyAddress = "127.0.0.1";
            //ProxyDetails.ProxyPort = 1081;
            //ProxyDetails.FullProxyAddress = "127.0.0.1:1081";

            if (ProxyDetails != null)
            {
                if (ProxyDetails.ProxyType == ProxyType.Proxy)
                {
                    result = (HttpWebRequest)WebRequest.Create(address);
                    HttpWebRequest request = (HttpWebRequest)result;
                    request.Proxy = new WebProxy(ProxyDetails.FullProxyAddress);
                    request.Timeout = this.timeout;
                    request.AllowAutoRedirect = true;  //这里不允许再继续跳转.否则取不到了
                    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    if (!string.IsNullOrEmpty(UserAgent))
                    {
                        request.UserAgent = UserAgent;
                    }
                    if (address.ToString().IndexOf("dmhy")>-1)
                    {
                        addDmhyHeader(result);
                    }
                }
                else if (ProxyDetails.ProxyType == ProxyType.Socks)
                {
                    result = SocksHttpWebRequest.Create(address);
                    SocksHttpWebRequest request = (SocksHttpWebRequest)result;
                    request.Proxy = new WebProxy(ProxyDetails.FullProxyAddress);
                    //request.AllowAutoRedirect = true;  //这里不允许再继续跳转.否则取不到了
                    //((HttpWebRequest)result).AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    //TODO: implement user and password

                    if (address.ToString().IndexOf("dmhy") > -1)
                    {
                        addSocksDmhyHeader(result);
                    }
                }
                else if (ProxyDetails.ProxyType == ProxyType.None)
                {
                    result = (HttpWebRequest)WebRequest.Create(address);
                    HttpWebRequest request = (HttpWebRequest)result;
                    request.Timeout = this.timeout;
                    request.AllowAutoRedirect = true;  //这里不允许再继续跳转.否则取不到了
                    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    if (!string.IsNullOrEmpty(UserAgent))
                    {
                        request.UserAgent = UserAgent;
                    }
                    if (address.ToString().IndexOf("dmhy") > -1)
                    {
                        addDmhyHeader(result);
                    }
                    
                }
            }
            else
            {
 
            }

            return result;
        }

        private void addSocksDmhyHeader(WebRequest result)
        {
            result.Headers.Add(HttpRequestHeader.Accept, "text/html, application/xhtml+xml, */*");
            result.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN");
            result.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko");
            //result.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            result.Headers.Add(HttpRequestHeader.Connection, "Keep-Alive");
            result.Headers.Add(HttpRequestHeader.Host, "share.dmhy.org");
            result.Headers.Add(HttpRequestHeader.Cookie, cookies);
        }

        private void addDmhyHeader(WebRequest result)
        {
            Version currentVersion = Environment.OSVersion.Version;
            Version compareToVersion = new Version("6.2");
            if (currentVersion.CompareTo(compareToVersion) >= 0)
            {//win8及其以上版本的系统  
                Console.WriteLine("当前系统是WIN8及以上版本系统。");
                ((HttpWebRequest)result).Accept = "text/html, application/xhtml+xml, image/jxr, */*";
                result.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-Hans-CN,zh-Hans;q=0.8,ja;q=0.6,en-US;q=0.4,en;q=0.2");
                ((HttpWebRequest)result).UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
            }
            else
            {
                Console.WriteLine("当前系统不是WIN8及以上版本系统。");
                ((HttpWebRequest)result).Accept = "text/html, application/xhtml+xml, */*";
                result.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN");
                ((HttpWebRequest)result).UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
            }  

            result.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            ((HttpWebRequest)result).Headers.GetType().InvokeMember("ChangeInternal",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.InvokeMethod, null,
                ((HttpWebRequest)result).Headers, new object[] { "Host", "share.dmhy.org" }
            );
            ((HttpWebRequest)result).KeepAlive = true;
            result.Headers.Add(HttpRequestHeader.Cookie, cookies);
        }

        //protected override WebRequest GetWebRequest(Uri address)
        //{
        //    HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
        //    request.Timeout = this.timeout;
        //    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        //    return request;
        //}
        public virtual byte[] DownloadData(string address)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            int tryCount = 0;
            byte[] result = null;
            while (true)
            {
                try
                {
                    if (1 < tryCount)
                    {
                        throw new MyWebClientException("地址:" + address + " 连接失败!请检测网络!");
                    }
                    result = base.DownloadData(address);
                    break;
                }
                catch (System.Net.WebException e)
                {
                    tryCount++;
                    //线程休眼10秒后在试
                    Thread.Sleep(100);
                }
                catch (NotSupportedException e)
                {
                    tryCount++;
                    //线程休眼10秒后在试
                    Thread.Sleep(100);
                }
            }
            return result;
        }

    }
}
