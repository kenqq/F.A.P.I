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
                    result.Proxy = new WebProxy(ProxyDetails.FullProxyAddress);
                    result.Timeout = this.timeout;
                    ((HttpWebRequest)result).AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    if (!string.IsNullOrEmpty(UserAgent))
                        ((HttpWebRequest)result).UserAgent = UserAgent;
                }
                else if (ProxyDetails.ProxyType == ProxyType.Socks)
                {
                    result = SocksHttpWebRequest.Create(address);
                    result.Proxy = new WebProxy(ProxyDetails.FullProxyAddress);
                    //((HttpWebRequest)result).AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    //TODO: implement user and password

                }
                else if (ProxyDetails.ProxyType == ProxyType.None)
                {
                    result = (HttpWebRequest)WebRequest.Create(address);
                    result.Timeout = this.timeout;
                    ((HttpWebRequest)result).AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    if (!string.IsNullOrEmpty(UserAgent))
                        ((HttpWebRequest)result).UserAgent = UserAgent;
                }
            }
            else
            {
                result = (HttpWebRequest)WebRequest.Create(address);
                result.Timeout = this.timeout;
                ((HttpWebRequest)result).AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                if (!string.IsNullOrEmpty(UserAgent))
                    ((HttpWebRequest)result).UserAgent = UserAgent;
            }
            
            return result;
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
            int tryCount = 0;
            byte[] result=null;
            while (true)
            {
                try
                {
                    if (3 < tryCount)
                    {
                        throw new MyWebClientException("地址:" + address +" 连接失败!请检测网络!");
                    }
                      result = base.DownloadData(address);
                      break;
                }   
                catch (System.Net.WebException  e)
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
