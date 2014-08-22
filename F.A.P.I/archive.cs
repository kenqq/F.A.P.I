using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F.A.P.I
{
    public class archive
    {
        public string year { get; set; }
        public List<months> months { get; set; }
    }

    public class months
    {
        public string month { get; set; }
        public string json { get; set; }
        public string done { get; set; }
    }
}
