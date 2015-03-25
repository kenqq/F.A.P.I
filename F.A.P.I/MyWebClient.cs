using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace F.A.P.I
{
    class MyWebClient : WebClient
    {
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
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
            request.Timeout = this.timeout;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            return request;
        }
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
