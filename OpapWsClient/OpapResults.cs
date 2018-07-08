using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpapWsClient
{
    public class OpapResults
    {
        public Draw draw { get; set; }
    }

    public class Draw
    {
        public string drawTime { get; set; }
        public string drawNo { get; set; }
        public List<string> results { get; set; }
    }

    public class Draws
    {
        public List<Draw> draw { get; set; }
    }

    public class OpapDateResults
    {
        public Draws draws { get; set; }
    }
}
