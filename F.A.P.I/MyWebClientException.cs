using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F.A.P.I
{
    class MyWebClientException : ApplicationException
    {
        public MyWebClientException(string message) : base(message)
        {
        }


        public override string Message
        {
            get
            {
                return (base.Message);
            }
        }
    }
}
